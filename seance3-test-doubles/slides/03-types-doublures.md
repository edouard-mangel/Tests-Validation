# Les types de doublures de test

<br>

Il existe **5 types** de doublures (test doubles), chacune avec un rôle précis :

<v-clicks>

| Type | Rôle | Analogie |
|------|------|----------|
| **Dummy** | Remplit un paramètre, jamais utilisé | Un figurant dans un film |
| **Stub** | Retourne des réponses prédéfinies | Un répondeur automatique |
| **Fake** | Implémentation simplifiée mais fonctionnelle | Un avion en papier |
| **Mock** | Vérifie qu'une interaction a eu lieu | Une caméra de surveillance |
| **Spy** | Enregistre les appels pour assertion ultérieure | Un magnétophone |

</v-clicks>

<!--
Ces termes viennent du livre "xUnit Test Patterns" de Gerard Meszaros.
En pratique, on utilise surtout des stubs et des fakes. Les mocks sont à utiliser avec parcimonie.
-->

---

# Dummy

<br>

Un **dummy** est un objet qui remplit un paramètre mais **n'est jamais utilisé**.

```csharp
[Fact]
public void CreateOrder_WithoutDiscount_CalculatesTotal()
{
    // Le logger est requis par le constructeur mais pas utilisé dans ce test
    var dummyLogger = new DummyLogger(); // Ne fait rien

    var service = new OrderService(dummyLogger);
    var order = service.CreateOrder(items: new[] { 10m, 20m });

    Assert.Equal(30m, order.Total);
}

public class DummyLogger : ILogger
{
    public void Log(string message) { } // Ne fait rien
}
```

<v-click>

Utilisé quand une dépendance est requise par la signature mais **pas impliquée** dans le test.

</v-click>

<!--
Le cas le plus simple. On a besoin de passer quelque chose au constructeur, mais ce n'est pas ce qu'on teste.
-->

---

# Stub

<br>

Un **stub** retourne des **réponses prédéfinies**, sans logique.

```csharp
[Fact]
public void GetGreeting_MorningHour_ReturnsBonjourMessage()
{
    // Le stub retourne toujours 8h du matin
    var stubClock = new StubClock(hour: 8);
    var service = new GreetingService(stubClock);

    var greeting = service.GetGreeting();

    Assert.Equal("Bonjour !", greeting);
}

public class StubClock : IClock
{
    private readonly int _hour;
    public StubClock(int hour) => _hour = hour;
    public DateTime Now => new DateTime(2025, 1, 1, _hour, 0, 0);
}
```

<v-click>

Le stub **contrôle l'environnement** : on décide ce que la dépendance retourne.

</v-click>

<!--
C'est la doublure la plus courante. On veut tester le comportement de notre code quand la dépendance retourne une valeur précise.
On ne vérifie pas que la dépendance a été appelée — on contrôle ce qu'elle retourne.
-->

---

# Fake

<br>

Un **fake** est une **implémentation simplifiée** mais fonctionnelle.

```csharp
public class FakeUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public void Add(User user) => _users.Add(user);
    public User? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);
    public List<User> GetAll() => _users.ToList();
}
```

<v-click>

```csharp
[Fact]
public void GetById_ExistingUser_ReturnsUser()
{
    var fakeRepo = new FakeUserRepository();
    fakeRepo.Add(new User { Id = 1, Name = "Alice" });

    var user = fakeRepo.GetById(1);

    Assert.Equal("Alice", user?.Name);
}
```

</v-click>

<v-click>

Le fake **fonctionne** (stockage en mémoire) mais sans infrastructure réelle (pas de BDD).

</v-click>

<!--
Les fakes sont très utiles pour les repositories. On remplace la base de données par une List<T>.
C'est rapide, isolé, et on peut pré-remplir les données du test dans le Arrange.
-->

---

# Mock

<br>

Un **mock** vérifie qu'une **interaction a bien eu lieu**.

```csharp
[Fact]
public void NotifyUser_ExistingUser_SendsEmailWithCorrectSubject()
{
    var fakeRepo = new FakeUserRepository();
    fakeRepo.Add(new User { Id = 1, Email = "alice@test.com" });

    var mockSender = new MockEmailSender();
    var service = new NotificationService(fakeRepo, mockSender);

    service.NotifyUser(1, "Hello");

    // Le mock VÉRIFIE qu'une méthode a été appelée avec les bons arguments
    mockSender.VerifyWasCalled("alice@test.com", "Notification", "Hello");
}
```

<v-click>

<br>

### ⚠️ Attention à l'over-mocking

Les mocks testent les **interactions** (comment le code appelle ses dépendances), pas les **résultats**.

Risque : des tests **couplés à l'implémentation** qui cassent au moindre refactoring.

</v-click>

<!--
Les mocks sont puissants mais dangereux. Un test qui vérifie "la méthode X a été appelée avec les paramètres Y" est couplé au code.
Si demain on refactore l'intérieur du service, le test casse même si le comportement est correct.
Préférer les stubs et les fakes quand c'est possible.
-->

---

# Spy

<br>

Un **spy** enregistre les appels pour une **assertion ultérieure**.

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

```csharp
[Fact]
public void NotifyUser_SendsOneEmail()
{
    var spy = new SpyEmailSender();
    // ... setup ...

    service.NotifyUser(1, "Hello");

    Assert.Single(spy.SentEmails);
    Assert.Equal("alice@test.com", spy.SentEmails[0].To);
}
```

<v-click>

Le spy est un **enregistreur** : il capture les appels, on vérifie après coup.

</v-click>

<!--
La différence avec le mock : le spy ne vérifie rien lui-même. C'est le test qui fait les assertions.
En pratique, spy et mock sont souvent confondus. L'important est de comprendre la nuance.
-->

---

# Récapitulatif des doublures

<br>

| Type | Comportement | Vérification | Quand l'utiliser |
|------|-------------|--------------|------------------|
| **Dummy** | Rien | Rien | Paramètre obligatoire mais inutilisé |
| **Stub** | Retourne des valeurs fixes | Rien | Contrôler l'environnement du test |
| **Fake** | Implémentation simplifiée | Rien | Repositories, caches, files d'attente |
| **Mock** | Programmable | Vérifie les appels | Interactions critiques (avec parcimonie) |
| **Spy** | Enregistre les appels | Assertions sur les appels | Quand on veut vérifier QUOI a été envoyé |

<v-click>

<br>

> **Règle d'or :** préférer les stubs et les fakes. N'utiliser les mocks que quand l'interaction elle-même est le comportement à vérifier.

</v-click>

<!--
La question à se poser : "Est-ce que je teste un RÉSULTAT ou une INTERACTION ?"
Si résultat → stub ou fake.
Si interaction → mock ou spy (mais est-ce vraiment nécessaire ?).
-->

---

# L'angle IA — Tester du code généré

<br>

<v-clicks>

Le code généré par l'IA a souvent un défaut : le **couplage fort**.

```csharp
// Code typique généré par IA : tout est câblé en dur
public class OrderService
{
    public void ProcessOrder(Order order)
    {
        var db = new SqlConnection("...");     // ❌ Couplage
        var emailClient = new SmtpClient();     // ❌ Couplage
        var logger = new FileLogger("app.log"); // ❌ Couplage
        // ...
    }
}
```

Savoir **isoler les dépendances** et **injecter des doublures** est la compétence clé pour :
- Tester du code que vous n'avez pas écrit
- Refactorer du code généré pour le rendre testable
- Vérifier que le code fait **ce qu'il doit faire**, peu importe qui l'a écrit

</v-clicks>

<!--
Ce n'est pas une critique de l'IA. C'est une réalité : du code couplé est difficile à tester, qu'il soit écrit par un humain ou une IA.
La compétence : savoir identifier les dépendances et les extraire derrière des interfaces.
-->
