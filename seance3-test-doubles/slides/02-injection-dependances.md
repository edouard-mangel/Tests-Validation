# L'injection de dépendances (DI)

<br>

Le principe : les dépendances sont **fournies de l'extérieur**, pas créées en interne.

<v-click>

```csharp
// 1. Définir des interfaces pour chaque dépendance
public interface IUserRepository
{
    User? GetById(int id);
}

public interface IEmailSender
{
    void Send(string to, string subject, string body);
}

public interface IClock
{
    DateTime Now { get; }
}
```

</v-click>

<!--
Les interfaces sont le contrat. Elles décrivent ce qu'on peut faire, pas comment c'est fait.
En production, on injecte les vraies implémentations. En test, on injecte des doublures.
-->

---

# Injection par constructeur

<br>

````md magic-move
```csharp
// ❌ AVANT : dépendances codées en dur
public class NotificationService
{
    public void NotifyUser(int userId, string message)
    {
        var db = new SqlConnection("...");
        var user = db.Query<User>("...");
        var smtp = new SmtpClient("smtp.gmail.com");
        smtp.Send(new MailMessage(...));
    }
}
```

```csharp
// ✅ APRÈS : dépendances injectées via le constructeur
public class NotificationService
{
    private readonly IUserRepository _users;
    private readonly IEmailSender _emailSender;
    private readonly IClock _clock;

    public NotificationService(
        IUserRepository users,
        IEmailSender emailSender,
        IClock clock)
    {
        _users = users;
        _emailSender = emailSender;
        _clock = clock;
    }

    public void NotifyUser(int userId, string message)
    {
        var user = _users.GetById(userId);
        if (user is null) return;

        _emailSender.Send(user.Email, "Notification", message);
    }
}
```
````

<!--
L'injection par constructeur est le pattern le plus courant en C#/.NET.
Le framework de DI d'ASP.NET Core utilise exactement ce pattern.
Avantage clé : à la construction, on VOIT toutes les dépendances du service.
-->

---

# Pourquoi c'est testable maintenant

<br>

```csharp
[Fact]
public void NotifyUser_ExistingUser_SendsEmail()
{
    // Arrange — On injecte des DOUBLURES à la place des vrais services
    var fakeRepo = new FakeUserRepository();  // Pas de vraie BDD
    fakeRepo.Add(new User { Id = 1, Email = "alice@test.com" });

    var spySender = new SpyEmailSender();     // Pas de vrai SMTP
    var fakeClock = new FakeClock(new DateTime(2025, 1, 1));

    var service = new NotificationService(fakeRepo, spySender, fakeClock);

    // Act
    service.NotifyUser(1, "Hello");

    // Assert — On vérifie le comportement sans effet de bord
    Assert.Single(spySender.SentEmails);
    Assert.Equal("alice@test.com", spySender.SentEmails[0].To);
}
```

<v-click>

Pas de base de données. Pas d'email envoyé. Pas de dépendance à l'heure.

**Rapide, isolé, répétable, auto-validant.** ✅

</v-click>

<!--
C'est la puissance de l'injection de dépendances : on contrôle entièrement l'environnement du test.
FakeUserRepository, SpyEmailSender, FakeClock sont des doublures qu'on écrit nous-mêmes.
On va maintenant voir les différents types de doublures.
-->
