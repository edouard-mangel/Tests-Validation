# Code réel = dépendances

En TP, votre code était **autonome**. En entreprise, il ressemble à ça :

```csharp
public class CommandeService
{
    private readonly ICommandeRepository _repository;
    private readonly IEmailService _emailService;
    private readonly IStockService _stockService;

    public CommandeService(
        ICommandeRepository repository,
        IEmailService emailService,
        IStockService stockService)
    {
        _repository = repository;
        _emailService = emailService;
        _stockService = stockService;
    }

    public void PasserCommande(Commande commande)
    {
        if (!_stockService.EstDisponible(commande.ProduitId))
            throw new StockInsuffisantException();

        _repository.Sauvegarder(commande);
        _emailService.EnvoyerConfirmation(commande.ClientEmail);
    }
}
```

<v-click>

**Comment tester ça sans base de données, sans serveur email, sans API de stock ?**

</v-click>

<!--
Montrer le code et poser la question. Laisser les étudiants réfléchir.
Ce service fait 3 choses : vérifier le stock, sauvegarder en BDD, envoyer un email.
On ne peut pas instancier ce service sans fournir les 3 dépendances.
-->

---
layout: two-cols-header
---

# Tentative naïve : tout brancher

::left::

```csharp
[Fact]
public void PasserCommande_StockOk_Sauvegarde()
{
    // 😱 Vraie base de données
    var repo = new SqlCommandeRepository(
        "Server=localhost;Database=test");

    // 😱 Vrai serveur email
    var email = new SmtpEmailService(
        "smtp.gmail.com", 587);

    // 😱 Vraie API de stock
    var stock = new HttpStockService(
        "https://api.stock.com");

    var service = new CommandeService(
        repo, email, stock);

    service.PasserCommande(uneCommande);
}
```

::right::

### Violations FIRST

<v-clicks>

- ❌ **Fast** — Requêtes réseau + BDD = lent
- ❌ **Isolated** — Dépend de serveurs externes
- ❌ **Repeatable** — Le stock change, la BDD aussi
- ❌ **Self-validating** — Vérifier l'email envoyé ?
- 💩 Impossible en CI/CD

</v-clicks>

<!--
Rappel des principes FIRST vus en Séance 3.
Chaque dépendance externe ajoute de la lenteur, de l'instabilité et de la complexité.
On ne peut pas exécuter ça dans la CI : il faudrait un serveur SQL, un serveur SMTP, une API de stock...
C'est exactement le problème qu'on va résoudre.
-->

---

# Le couplage est l'ennemi

Le pire cas : les dépendances sont **créées à l'intérieur** de la classe.

````md magic-move
```csharp
// 💩 Impossible à tester — le client SMTP est créé en dur
public class NotificationService
{
    public void Notifier(string email, string message)
    {
        var client = new SmtpClient("smtp.gmail.com", 587);
        client.Send("noreply@app.com", email, "Notification", message);
    }
}
```

```csharp
// 💩 Comment tester sans envoyer un vrai email ?
[Fact]
public void Notifier_EnvoieUnEmail()
{
    var service = new NotificationService();

    // Ceci envoie un VRAI email à chaque exécution du test 😱
    service.Notifier("test@example.com", "Hello");

    // Et comment vérifier qu'il a été envoyé ?
    // Assert... quoi exactement ?
}
```
````

<v-click>

Le `new SmtpClient()` en dur rend le code **impossible à tester** sans effets de bord.

</v-click>

<!--
C'est le pattern le plus courant dans le code legacy : les dépendances sont instanciées directement.
On ne peut pas remplacer SmtpClient par autre chose.
C'est ce qu'on appelle le couplage fort. La solution : l'injection de dépendances.
-->

---

# La solution en deux étapes

<br>

<v-clicks>

1. **Rendre le code testable** → Injection de dépendances (DI)
2. **Remplacer les dépendances** → Doublures de test

</v-clicks>

<br>

<v-click>

> *On ne change pas ce que le code fait — on change comment il obtient ses dépendances.*

</v-click>

<!--
Deux étapes distinctes. D'abord on refactore pour que les dépendances soient injectées (pas créées en dur).
Ensuite on fournit des fausses dépendances dans les tests.
Rassurer : ce refactoring ne change pas le comportement du code en production.
-->
