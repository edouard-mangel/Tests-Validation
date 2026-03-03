# TP3 - Doublures de test et isolation

## Objectifs

- Ecrire des **doublures manuelles** (fake, spy, stub) pour isoler un service
- Comprendre pourquoi les doublures manuelles sont **preferables** aux frameworks de mocking
- Constater les **problemes** des mocks (couplage a l'implementation, fragilite au refactoring)
- **Refactorer du code couple** pour le rendre testable
- Utiliser le pattern **Test Data Builder** pour simplifier le setup
- Partager le setup avec **IClassFixture**

## Mise en place

Creez un nouveau projet de test xUnit dans votre solution :

```bash
dotnet new xunit -n TestingTP3.Tests
dotnet add TestingTP3.Tests package NSubstitute
```


<div class="page" />


## Exercice 1 - Tester NotificationService avec des doublures (55 min)

### Contexte

Votre equipe a developpe un `NotificationService` plus complet que celui vu en cours. Il gere l'envoi d'emails et de SMS, avec un systeme d'opt-out pour les utilisateurs.

Voici les interfaces et les classes du domaine :

```csharp
namespace TestingTP3;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public bool HasOptedOut { get; set; }
}

public interface IUserRepository
{
    User? GetById(int id);
    List<User> GetAll();
}

public interface IEmailSender
{
    void Send(string to, string subject, string body);
}

public interface ISmsSender
{
    void Send(string phoneNumber, string message);
}

public interface IClock
{
    DateTime Now { get; }
}
```

Et voici l'implementation du service :

```csharp
namespace TestingTP3;

public class NotificationService
{
    private readonly IUserRepository _users;
    private readonly IEmailSender _emailSender;
    private readonly ISmsSender _smsSender;
    private readonly IClock _clock;

    public NotificationService(
        IUserRepository users,
        IEmailSender emailSender,
        ISmsSender smsSender,
        IClock clock)
    {
        _users = users;
        _emailSender = emailSender;
        _smsSender = smsSender;
        _clock = clock;
    }

    public void NotifyUser(int userId, string message)
    {
        var user = _users.GetById(userId);
        if (user is null) return;
        if (user.HasOptedOut) return;

        _emailSender.Send(
            user.Email,
            $"Notification du {_clock.Now:dd/MM/yyyy}",
            message);
    }

    public void SendUrgentNotification(int userId, string message)
    {
        var user = _users.GetById(userId);
        if (user is null) return;

        // Les notifications urgentes ignorent l'opt-out
        _emailSender.Send(user.Email, "URGENT", message);
        _smsSender.Send(user.Phone, $"URGENT: {message}");
    }

    public void NotifyAllUsers(string message)
    {
        var users = _users.GetAll();
        foreach (var user in users)
        {
            if (!user.HasOptedOut)
            {
                _emailSender.Send(
                    user.Email,
                    $"Notification du {_clock.Now:dd/MM/yyyy}",
                    message);
            }
        }
    }
}
```

### Etape 1 - Ecrire un FakeUserRepository (10 min)

Un **fake** est une implementation simplifiee mais fonctionnelle. Creez un `FakeUserRepository` qui stocke les utilisateurs en memoire :

```csharp
using TestingTP3;

namespace TestingTP3.Tests;

public class FakeUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public void Add(User user) => _users.Add(user);

    public User? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);

    public List<User> GetAll() => _users.ToList();
}
```

Ce fake remplace une vraie base de donnees. Il est rapide, isole, et on controle entierement son contenu.

### Etape 2 - Ecrire un SpyEmailSender et tester NotifyUser (10 min)

Un **spy** enregistre les appels pour qu'on puisse verifier ce qui s'est passe. Creez un `SpyEmailSender` :

```csharp
public class SpyEmailSender : IEmailSender
{
    public List<(string To, string Subject, string Body)> SentEmails { get; } = new();

    public void Send(string to, string subject, string body)
    {
        SentEmails.Add((to, subject, body));
    }
}
```

Ecrivez maintenant votre premier test. Il verifie que `NotifyUser` envoie un email a un utilisateur existant :

```csharp
using TestingTP3;

namespace TestingTP3.Tests;

public class NotificationServiceTests
{
    [Fact]
    public void NotifyUser_ExistingUser_SendsEmail()
    {
        // Arrange
        var repo = new FakeUserRepository();
        repo.Add(new User { Id = 1, Email = "alice@test.com", Phone = "0600000001" });

        var emailSpy = new SpyEmailSender();
        var smsSpy = new SpySmsSender();
        var clock = new StubClock(new DateTime(2025, 6, 15));

        var service = new NotificationService(repo, emailSpy, smsSpy, clock);

        // Act
        service.NotifyUser(1, "Bienvenue !");

        // Assert
        Assert.Single(emailSpy.SentEmails);
        Assert.Equal("alice@test.com", emailSpy.SentEmails[0].To);
        Assert.Contains("Bienvenue !", emailSpy.SentEmails[0].Body);
    }
}
```

Il vous manque `SpySmsSender` et `StubClock`. Ecrivez-les sur le meme modele :

```csharp
public class SpySmsSender : ISmsSender
{
    public List<(string PhoneNumber, string Message)> SentMessages { get; } = new();

    public void Send(string phoneNumber, string message)
    {
        SentMessages.Add((phoneNumber, message));
    }
}

public class StubClock : IClock
{
    private readonly DateTime _now;
    public StubClock(DateTime now) => _now = now;
    public DateTime Now => _now;
}
```

Lancez le test. Il doit passer au vert.

### Etape 3 - Tester les autres cas (10 min)

Ecrivez les tests suivants. Les signatures sont fournies, le corps est a completer :

```csharp
[Fact]
public void NotifyUser_UserNotFound_DoesNotSendEmail()
{
    // Arrange — repo vide, pas d'utilisateur avec l'id 999
    // Act — NotifyUser(999, ...)
    // Assert — emailSpy.SentEmails doit etre vide
}

[Fact]
public void NotifyUser_UserOptedOut_DoesNotSendEmail()
{
    // Arrange — ajouter un utilisateur avec HasOptedOut = true
    // Act — NotifyUser(...)
    // Assert — emailSpy.SentEmails doit etre vide
}

[Fact]
public void SendUrgentNotification_ExistingUser_SendsEmailAndSms()
{
    // Arrange — ajouter un utilisateur
    // Act — SendUrgentNotification(...)
    // Assert — emailSpy ET smsSpy doivent avoir recu un message
}

[Fact]
public void SendUrgentNotification_UserOptedOut_SendsAnyway()
{
    // Arrange — ajouter un utilisateur avec HasOptedOut = true
    // Act — SendUrgentNotification(...)
    // Assert — les notifications urgentes ignorent l'opt-out
}
```

### Etape 4 - Tester NotifyAllUsers avec le spy (10 min)

Ecrivez un test pour `NotifyAllUsers` en utilisant vos doublures manuelles.

Ajoutez 3 utilisateurs au `FakeUserRepository` : 2 actifs et 1 avec opt-out. Appelez `NotifyAllUsers`, puis verifiez avec le spy :

```csharp
[Fact]
public void NotifyAllUsers_SendsEmailOnlyToActiveUsers()
{
    // Arrange — 3 utilisateurs : Alice (active), Bob (active), Charlie (opted out)
    // Act — NotifyAllUsers("Mise a jour")
    // Assert — emailSpy.SentEmails contient 2 emails (Alice et Bob, pas Charlie)
}
```

Remarquez comme le spy rend l'assertion naturelle : on inspecte les emails **reellement captures** et on verifie leur contenu.

### Etape 5 - Le meme test avec NSubstitute : comparer les approches (15 min)

Reecrivez maintenant **les memes tests** avec NSubstitute. Voici les operations cles :

| Operation | Syntaxe NSubstitute |
|-----------|-------------------|
| Creer une doublure | `Substitute.For<IEmailSender>()` |
| Configurer un retour | `repo.GetById(1).Returns(new User { ... })` |
| Verifier un appel | `emailSender.Received(1).Send("alice@test.com", ...)` |
| Verifier l'absence d'appel | `emailSender.DidNotReceive().Send(...)` |
| Accepter n'importe quel argument | `Arg.Any<string>()` |

Creez une nouvelle classe `NotificationServiceNSubTests` et reecrivez ces deux tests avec NSubstitute :

1. `NotifyUser_ExistingUser_SendsEmail` — verifiez avec `.Received(1).Send(...)` au lieu du spy
2. `NotifyAllUsers_SendsEmailOnlyToActiveUsers` — configurez `GetAll().Returns(...)`, puis verifiez avec `.Received()` et `.DidNotReceive()` pour chaque utilisateur

Les deux versions (spy et NSubstitute) doivent etre au vert avant de continuer.

#### 5a - Refactoring : changer le format du sujet

Un collegue modifie le format du sujet des emails. Dans `NotificationService.NotifyUser`, changez :

```csharp
// Avant
$"Notification du {_clock.Now:dd/MM/yyyy}"

// Apres
$"[{_clock.Now:dd/MM/yyyy}] Notification"
```

Lancez **tous** les tests. Constatez :

- Quels tests de la version **spy** cassent ? Pourquoi ?
- Quels tests de la version **NSubstitute** cassent ? Pourquoi ?

Le spy capture des **donnees** qu'on peut inspecter librement — vos assertions portaient sur le **body**, pas sur le subject. `.Received()` verifie des **appels exacts** — si vous avez verifie le sujet, le test casse pour un changement cosmétique qui ne change pas le comportement.

Corrigez les tests NSubstitute qui ont casse, puis gardez le nouveau format — c'est un refactoring legitime.

#### 5b - Evolution : ajouter une formule de politesse

Le product owner demande que les emails de notification soient plus professionnels. Modifiez `NotifyUser` pour formater le body avec une salutation :

```csharp
var body = $"Bonjour {user.Name},\n\nCordialement,\nL'equipe";
_emailSender.Send(user.Email, subject, body);
```

C'est une evolution raisonnable. Lancez **tous** les tests (spy et NSubstitute).

Un des deux suites detecte un probleme. Lequel, et pourquoi l'autre ne le detecte pas ?

Corrigez le body pour inclure le `message` dans la formule de politesse, puis repassez tous les tests au vert.


<div class="page" />

---

## Exercice 2 - Refactorer du code couple (40 min)

### Contexte

Vous heritez d'un `ReportGenerator` ecrit par un collegue presse. Le code fonctionne, mais il est **impossible a tester** car toutes les dependances sont creees en interne.

```csharp
namespace TestingTP3;

public class ReportGenerator
{
    public void GenerateAndSend(string reportName, string recipientEmail)
    {
        // Dependance 1 : acces direct a la base de donnees
        var connection = new SqlConnection("Server=prod;Database=reports;...");
        connection.Open();
        var data = connection.Query($"SELECT * FROM ReportData WHERE Name = '{reportName}'");
        connection.Close();

        // Dependance 2 : horloge systeme
        var timestamp = DateTime.Now;

        // Dependance 3 : envoi d'email
        var smtp = new SmtpClient("smtp.company.com");
        var body = $"Rapport '{reportName}' genere le {timestamp:dd/MM/yyyy HH:mm}\n\n"
                 + $"Donnees : {data.Count()} lignes";
        smtp.Send(new MailMessage("reports@company.com", recipientEmail, reportName, body));
    }
}
```

### Etape 1 - Identifier les dependances (5 min)

Lisez le code et repondez a ces questions :

1. Combien de dependances externes ce code a-t-il ?
2. Lesquelles empechent les tests unitaires ?
3. Quels sont les risques de ce code en production ? (indice : regardez la requete SQL)

### Etape 2 - Extraire les interfaces et refactorer (10 min)

Definissez les trois interfaces suivantes :

```csharp
public interface IReportRepository
{
    ReportData GetByName(string reportName);
}

public interface IReportSender
{
    void Send(string to, string subject, string body);
}

public interface IClock
{
    DateTime Now { get; }
}

public class ReportData
{
    public string Name { get; set; } = "";
    public int RowCount { get; set; }
}
```

Refactorez `ReportGenerator` pour qu'il recoive ses dependances par le constructeur. L'implementation doit :
- Appeler `_repository.GetByName(reportName)` pour obtenir les donnees
- Utiliser `_clock.Now` pour l'horodatage
- Appeler `_sender.Send(recipientEmail, reportName, body)` pour envoyer le rapport

### Etape 3 - Ecrire les tests avec des doublures manuelles (15 min)

Comme pour l'exercice 1, creez des doublures manuelles pour vos interfaces :

```csharp
public class FakeReportRepository : IReportRepository
{
    private readonly Dictionary<string, ReportData> _reports = new();

    public void Add(string name, ReportData report) => _reports[name] = report;

    public ReportData GetByName(string reportName)
        => _reports.TryGetValue(reportName, out var report)
            ? report
            : throw new InvalidOperationException($"Report '{reportName}' not found");
}

public class SpyReportSender : IReportSender
{
    public List<(string To, string Subject, string Body)> SentReports { get; } = new();

    public void Send(string to, string subject, string body)
    {
        SentReports.Add((to, subject, body));
    }
}
```

Ecrivez les tests suivants. Les noms sont fournis, le corps est a ecrire :

```csharp
public class ReportGeneratorTests
{
    [Fact]
    public void GenerateAndSend_ValidReport_SendsEmailToRecipient()
    {
        // Verifier que le rapport est envoye au bon destinataire
    }

    [Fact]
    public void GenerateAndSend_ValidReport_IncludesTimestampInBody()
    {
        // Verifier que le corps du mail contient la date formatee
    }

    [Fact]
    public void GenerateAndSend_ValidReport_IncludesRowCountInBody()
    {
        // Verifier que le corps du mail contient le nombre de lignes
    }
}
```

Pour chaque test, utilisez vos doublures manuelles : `FakeReportRepository` (avec des donnees pre-remplies), `SpyReportSender` (pour verifier ce qui a ete envoye), et `StubClock` (pour controler la date).

### Etape 4 - Tester le cas d'erreur (10 min)

**Objectif** : tester ce qui se passe quand le repository leve une exception.

Le `FakeReportRepository` leve deja une `InvalidOperationException` quand le rapport n'existe pas. Ecrivez un test qui appelle `GenerateAndSend` avec un nom de rapport inexistant et verifiez le comportement :

```csharp
[Fact]
public void GenerateAndSend_UnknownReport_ThrowsException()
{
    // Arrange — un FakeReportRepository VIDE (pas de rapport ajoute)
    // Act + Assert — Assert.Throws<InvalidOperationException>(...)
}
```

Aucun besoin de framework de mocking : le fake gere naturellement le cas d'erreur.


<div class="page" />

---

## Exercice 3 - Test Data Builders (15 min)

### Contexte

Regardez les blocs `Arrange` de vos tests des exercices 1 et 2. Vous allez remarquer beaucoup de repetition dans la construction des objets `User` et `ReportData`.

Le pattern **Test Data Builder** encapsule cette construction avec des valeurs par defaut et une API fluente.

### Etape 1 - Observer le pattern Builder (5 min)

Voici un `UserBuilder` qui simplifie la creation d'utilisateurs de test :

```csharp
public class UserBuilder
{
    private int _id = 1;
    private string _name = "Alice";
    private string _email = "alice@test.com";
    private string _phone = "0600000001";
    private bool _hasOptedOut = false;

    public UserBuilder WithId(int id) { _id = id; return this; }
    public UserBuilder WithName(string name) { _name = name; return this; }
    public UserBuilder WithEmail(string email) { _email = email; return this; }
    public UserBuilder WithPhone(string phone) { _phone = phone; return this; }
    public UserBuilder AsOptedOut() { _hasOptedOut = true; return this; }

    public User Build() => new User
    {
        Id = _id,
        Name = _name,
        Email = _email,
        Phone = _phone,
        HasOptedOut = _hasOptedOut
    };
}
```

Comparez ces deux blocs Arrange :

```csharp
// Sans builder
var user = new User { Id = 1, Name = "Alice", Email = "alice@test.com",
                      Phone = "0600000001", HasOptedOut = false };

// Avec builder — seul ce qui est pertinent pour le test est visible
var user = new UserBuilder().AsOptedOut().Build();
```

Le builder met en avant **ce qui compte** pour le test et masque le bruit.

### Etape 2 - Implementer un ReportDataBuilder (5 min)

Implementez un builder pour `ReportData` avec cette API fluente :

```csharp
public class ReportDataBuilder
{
    // Valeurs par defaut
    // Methodes fluentes : WithName(string), WithRowCount(int)
    // Methode Build() qui retourne un ReportData
}

// Utilisation cible :
var report = new ReportDataBuilder().WithName("Ventes Q1").WithRowCount(150).Build();
```

### Etape 3 - Refactorer vos tests existants (5 min)

Choisissez 3 tests de l'exercice 1 ou 2 et remplacez la construction manuelle des objets par les builders.

Verifiez que tous les tests passent toujours.


<div class="page" />

---

## Exercice 4 - IClassFixture : partager le setup (15 min)

### Contexte

Dans vos tests de l'exercice 1, chaque methode de test recree les memes doublures et le meme service. Si on ajoute une dependance au `NotificationService`, il faudra modifier **chaque test**.

### Etape 1 - Identifier le setup repete (3 min)

Regardez vos tests de l'exercice 1. Quel code est duplique dans chaque methode de test ?

Listez les lignes qui apparaissent dans au moins 3 tests.

### Etape 2 - Creer une fixture partagee (7 min)

Creez une classe `NotificationTestFixture` qui contient le setup commun :

```csharp
public class NotificationTestFixture
{
    public FakeUserRepository UserRepo { get; }
    public SpyEmailSender EmailSpy { get; }
    public SpySmsSender SmsSpy { get; }
    public StubClock Clock { get; }
    public NotificationService Service { get; }

    public NotificationTestFixture()
    {
        UserRepo = new FakeUserRepository();
        EmailSpy = new SpyEmailSender();
        SmsSpy = new SpySmsSender();
        Clock = new StubClock(new DateTime(2025, 6, 15));
        Service = new NotificationService(UserRepo, EmailSpy, SmsSpy, Clock);
    }
}
```

Modifiez votre classe de test pour utiliser `IClassFixture<NotificationTestFixture>` :

```csharp
public class NotificationServiceTests : IClassFixture<NotificationTestFixture>
{
    private readonly NotificationTestFixture _fixture;

    public NotificationServiceTests(NotificationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void NotifyUser_ExistingUser_SendsEmail()
    {
        // Arrange — utiliser _fixture.UserRepo, _fixture.EmailSpy, etc.
        // ...
    }
}
```

**Attention** : avec `IClassFixture`, la fixture est partagee entre tous les tests. Vos tests doivent-ils etre adaptes pour rester isoles ? Reflechissez-y.

### Etape 3 - Verifier que tout fonctionne (5 min)

Lancez tous vos tests. Ils doivent tous passer au vert.

Si certains tests echouent, c'est probablement un probleme d'isolation : un test modifie l'etat de la fixture et affecte un autre test. Identifiez le probleme et corrigez-le.


<div class="page" />

---

## Recapitulatif

| Concept | Ou l'avez-vous pratique ? |
|---------|--------------------------|
| Fake (implementation simplifiee) | Exercice 1 etape 1, Exercice 2 etape 3 |
| Spy (enregistre les appels) | Exercice 1 etapes 2-4, Exercice 2 etape 3 |
| Stub (retourne des valeurs fixes) | Exercice 1 etape 2, Exercice 2 etape 3 |
| NSubstitute et ses limites | Exercice 1, etape 5a (refactoring qui casse les mocks) |
| `Arg.Any<>()` masque les regressions | Exercice 1, etape 5b (evolution qui revele la faiblesse) |
| Refactoring pour testabilite | Exercice 2, etapes 1-2 |
| Tester un cas d'erreur (exception) | Exercice 2, etape 4 |
| Test Data Builder | Exercice 3 |
| `IClassFixture<T>` | Exercice 4 |
