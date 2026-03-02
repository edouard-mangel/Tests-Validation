---
layout: two-cols-header
---

# Avant / Après

::left::

### ❌ Couplage fort

````md magic-move
```csharp
public class NotificationService
{
    public void Notifier(
        string email, string message)
    {
        // Dépendance créée en dur
        var client = new SmtpClient(
            "smtp.gmail.com", 587);

        client.Send(
            "noreply@app.com",
            email,
            "Notification",
            message);
    }
}
```
````

::right::

### ✅ Injection de dépendances

````md magic-move
```csharp
public class NotificationService
{
    private readonly IEmailService _emailService;

    // Dépendance injectée par le constructeur
    public NotificationService(
        IEmailService emailService)
    {
        _emailService = emailService;
    }

    public void Notifier(
        string email, string message)
    {
        _emailService.Envoyer(
            email, message);
    }
}
```
````

<!--
À gauche : le service crée lui-même son SmtpClient. On ne peut pas le remplacer.
À droite : le service reçoit un IEmailService. On peut lui passer n'importe quelle implémentation.
En production : le vrai SmtpEmailService. En test : une doublure.
-->

---

# L'injection par constructeur

Le pattern le plus simple et le plus courant.

```csharp
public class CommandeService
{
    private readonly ICommandeRepository _repository;
    private readonly IEmailService _emailService;
    private readonly IStockService _stockService;

    // Toutes les dépendances sont injectées via le constructeur
    public CommandeService(
        ICommandeRepository repository,
        IEmailService emailService,
        IStockService stockService)
    {
        _repository = repository;
        _emailService = emailService;
        _stockService = stockService;
    }
}
```

<v-click>

**Règle : une classe ne crée jamais ses propres dépendances. Elle les reçoit.**

</v-click>

<!--
C'est le principe d'inversion de dépendances (le D de SOLID).
Le constructeur déclare explicitement ce dont la classe a besoin.
En ASP.NET, le conteneur d'injection (DI container) fait ça automatiquement en production.
Mais pour les tests, on fait l'injection à la main — et c'est très simple.
-->

---

# L'interface comme contrat

L'interface définit **ce que fait** la dépendance. L'implémentation définit **comment**.

```csharp
// Le contrat — ce que le service attend
public interface IEmailService
{
    void Envoyer(string destinataire, string message);
}

// L'implémentation réelle — en production
public class SmtpEmailService : IEmailService
{
    public void Envoyer(string destinataire, string message)
    {
        // Vrai envoi SMTP via un serveur email
    }
}

// L'implémentation de test — dans les tests
public class FakeEmailService : IEmailService
{
    public void Envoyer(string destinataire, string message)
    {
        // Ne fait rien, ou enregistre l'appel
    }
}
```

<!--
L'interface est le contrat. Le code de production et le code de test respectent le même contrat.
Le service ne sait pas (et ne doit pas savoir) quelle implémentation il utilise.
C'est ce qui rend le code testable : on peut substituer n'importe quelle implémentation.
-->

---

# Maintenant, on peut tester !

```csharp
[Fact]
public void PasserCommande_StockDisponible_SauvegardeCommande()
{
    // Arrange — on injecte des doublures
    var fakeRepo = new FakeCommandeRepository();
    var fakeEmail = new FakeEmailService();
    var stubStock = new StubStockService(disponible: true);

    var service = new CommandeService(fakeRepo, fakeEmail, stubStock);

    // Act
    var commande = new Commande { ProduitId = 1, ClientEmail = "a@b.com" };
    service.PasserCommande(commande);

    // Assert — on vérifie que la commande a été sauvegardée
    Assert.Contains(commande, fakeRepo.CommandesSauvegardees);
}
```

<v-clicks>

- ✅ **Fast** — Pas de réseau, pas de BDD
- ✅ **Isolated** — Aucune dépendance externe
- ✅ **Repeatable** — Même résultat à chaque exécution

</v-clicks>

<!--
C'est le même test que la tentative naïve, mais avec des doublures.
On contrôle entièrement le comportement : le stock est toujours disponible, l'email ne part jamais.
Reste à comprendre les différents types de doublures. C'est le sujet de la prochaine partie.
-->
