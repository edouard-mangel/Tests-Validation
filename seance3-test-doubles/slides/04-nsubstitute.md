# NSubstitute — Framework de mocking pour .NET

<br>

<v-clicks>

Écrire des doublures manuellement fonctionne, mais ça devient **verbeux** avec beaucoup de dépendances.

**NSubstitute** génère des doublures automatiquement à partir des interfaces.

```bash
dotnet add package NSubstitute
```

</v-clicks>

<!--
NSubstitute est une alternative populaire à Moq. Sa syntaxe est plus naturelle et lisible.
On pourrait aussi utiliser Moq ou FakeItEasy — les concepts sont les mêmes.
-->

---

# Créer un stub avec NSubstitute

<br>

````md magic-move
```csharp
// Sans NSubstitute : doublure manuelle
public class StubClock : IClock
{
    private readonly DateTime _now;
    public StubClock(DateTime now) => _now = now;
    public DateTime Now => _now;
}

// Utilisation
var clock = new StubClock(new DateTime(2025, 1, 1, 8, 0, 0));
```

```csharp
// Avec NSubstitute : une ligne suffit
using NSubstitute;

var clock = Substitute.For<IClock>();
clock.Now.Returns(new DateTime(2025, 1, 1, 8, 0, 0));
```
````

<v-click>

<br>

`Substitute.For<T>()` crée une doublure qui implémente l'interface `T`.

`.Returns(...)` configure la valeur retournée (c'est un **stub**).

</v-click>

<!--
NSubstitute génère dynamiquement une classe qui implémente l'interface.
Par défaut, toutes les méthodes retournent des valeurs par défaut (0, null, false...).
On configure les retours avec .Returns().
-->

---

# Un test complet avec NSubstitute

<br>

```csharp
[Fact]
public void NotifyUser_ExistingUser_SendsEmail()
{
    // Arrange — NSubstitute crée les doublures
    var userRepo = Substitute.For<IUserRepository>();
    userRepo.GetById(1).Returns(new User { Id = 1, Email = "alice@test.com" });

    var emailSender = Substitute.For<IEmailSender>();
    var clock = Substitute.For<IClock>();
    clock.Now.Returns(new DateTime(2025, 6, 15));

    var service = new NotificationService(userRepo, emailSender, clock);

    // Act
    service.NotifyUser(1, "Bienvenue !");

    // Assert — Vérifier que l'email a été envoyé
    emailSender.Received(1).Send(
        "alice@test.com",
        "Notification",
        "Bienvenue !"
    );
}
```

<!--
.Received(1) vérifie que la méthode a été appelée exactement 1 fois avec ces arguments.
C'est un mock au sens strict : on vérifie une interaction.
On pourrait aussi utiliser .DidNotReceive() pour vérifier qu'un appel n'a PAS eu lieu.
-->

---

# NSubstitute — Les essentiels

<br>

<v-clicks>

| Opération | Syntaxe |
|-----------|---------|
| Créer une doublure | `Substitute.For<IService>()` |
| Configurer un retour | `service.Method(arg).Returns(value)` |
| Retour conditionnel | `service.Method(Arg.Any<int>()).Returns(value)` |
| Vérifier un appel | `service.Received(1).Method(arg)` |
| Vérifier l'absence d'appel | `service.DidNotReceive().Method(arg)` |
| Lever une exception | `service.Method(arg).Throws(new Exception())` |

</v-clicks>

<v-click>

<br>

### La règle reste la même

Préférer les **stubs** (`.Returns(...)`) aux **mocks** (`.Received(...)`).

Ne vérifier les interactions que quand c'est le **comportement** qu'on teste.

</v-click>

<!--
En pratique, 80% de l'utilisation de NSubstitute, c'est Substitute.For + .Returns.
Les .Received() sont pour les cas où l'interaction EST le comportement (envoi d'email, publication d'événement, etc.).
-->

---

# Tester le comportement, pas l'implémentation

<br>

````md magic-move
```csharp
// ❌ Teste l'implémentation : couplé au nombre d'appels et à l'ordre
[Fact]
public void ProcessOrder_CallsRepositoryThenSendsEmail()
{
    var repo = Substitute.For<IOrderRepository>();
    var sender = Substitute.For<IEmailSender>();
    var service = new OrderService(repo, sender);

    service.Process(new Order { Id = 1 });

    // On vérifie COMMENT le code fonctionne
    repo.Received(1).Save(Arg.Any<Order>());       // ← couplage
    sender.Received(1).Send(Arg.Any<string>(),      // ← couplage
                            Arg.Any<string>(),
                            Arg.Any<string>());
}
```

```csharp
// ✅ Teste le comportement : vérifie le résultat observable
[Fact]
public void ProcessOrder_SavesOrderAndNotifiesCustomer()
{
    var repo = new FakeOrderRepository();
    var spy = new SpyEmailSender();
    var service = new OrderService(repo, spy);

    service.Process(new Order { Id = 1, CustomerEmail = "bob@test.com" });

    // On vérifie CE QUE le code fait, pas COMMENT
    Assert.Single(repo.GetAll());                   // ← résultat
    Assert.Contains(spy.SentEmails,
        e => e.To == "bob@test.com");               // ← résultat
}
```
````

<v-click>

> *"Testez-vous le comportement ou l'implémentation ? Comment le sauriez-vous ?"*

</v-click>

<!--
C'est LA question à se poser à chaque test.
Test de gauche : si on change l'ordre des appels internes, le test casse. Mauvais signe.
Test de droite : tant que la commande est sauvée et le client notifié, le test passe. Bon signe.
-->

---
layout: center
class: text-center
---

# TP — Doublures de test

<br>

**Exercice 1** — Tester `NotificationService` avec des stubs et un spy

**Exercice 2** — Refactorer du code couplé, puis le tester

**Exercice 3** — Test Data Builders : simplifier le setup des tests

**Exercice 4** — `IClassFixture` : partager le setup entre les tests

<v-click>

<br>

Un **Test Data Builder** encapsule la construction d'objets de test avec des valeurs par défaut :

```csharp
var user = new UserBuilder().AsPremium().WithEmail("alice@test.com").Build();
```

</v-click>

<br>

*"Ai-je testé le comportement ou l'implémentation ?"*

<!--
Le TP comporte quatre parties :
1. Un NotificationService avec des dépendances déjà injectées — doublures manuelles puis NSubstitute
2. Un code tightly coupled — les étudiants refactorent pour injecter les dépendances, puis testent
3. Les étudiants refactorent le setup de leurs tests pour utiliser le pattern Builder
4. Les étudiants extraient le setup partagé dans une IClassFixture
-->
