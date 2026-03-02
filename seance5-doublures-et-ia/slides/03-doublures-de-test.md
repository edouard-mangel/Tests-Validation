# Les types de doublures

<br>

| Type | Rôle | Exemple |
|------|------|---------|
| **Dummy** | Remplit un paramètre, jamais utilisé | `new FakeLogger()` passé mais jamais appelé |
| **Stub** | Retourne des valeurs prédéfinies | `StockService` qui retourne toujours `true` |
| **Fake** | Implémentation simplifiée mais fonctionnelle | Repository en mémoire (remplace la BDD) |
| **Mock** | Vérifie qu'une méthode a été appelée | Vérifier que `Envoyer()` a été appelé |
| **Spy** | Enregistre les appels pour vérification | Compte le nombre d'appels à `Envoyer()` |

<v-click>

En pratique, on utilise surtout **stubs**, **fakes** et **mocks**.

</v-click>

<!--
Terminologie de Gerard Meszaros (xUnit Test Patterns).
Ne pas s'attarder sur les définitions exactes — ce qui compte c'est de savoir quand utiliser quoi.
On va voir les trois principaux en détail avec des exemples de code.
-->

---

# Stub : contrôler les entrées

Un stub retourne des **valeurs prédéfinies**. On contrôle ce que la dépendance "répond".

```csharp
// Stub manuel — retourne toujours la même valeur
public class StubStockService : IStockService
{
    private readonly bool _disponible;

    public StubStockService(bool disponible)
    {
        _disponible = disponible;
    }

    public bool EstDisponible(int produitId) => _disponible;
}
```

```csharp
[Fact]
public void PasserCommande_StockIndisponible_LeveException()
{
    // Arrange — le stub dit "pas de stock"
    var stubStock = new StubStockService(disponible: false);
    var service = new CommandeService(new FakeRepo(), new FakeEmail(), stubStock);

    // Act & Assert
    Assert.Throws<StockInsuffisantException>(
        () => service.PasserCommande(uneCommande));
}
```

<!--
Le stub contrôle les données en ENTRÉE du système sous test.
On ne vérifie pas que le stub a été appelé — on vérifie le comportement du service QUAND le stock est indisponible.
Le stub permet de simuler tous les scénarios : stock OK, stock vide, erreur réseau...
-->

---

# Fake : simuler un comportement

Un fake est une **vraie implémentation**, mais simplifiée. Comme une BDD en mémoire.

```csharp
// Fake — un repository qui stocke en mémoire au lieu de la BDD
public class FakeCommandeRepository : ICommandeRepository
{
    public List<Commande> CommandesSauvegardees { get; } = new();

    public void Sauvegarder(Commande commande)
    {
        CommandesSauvegardees.Add(commande);
    }

    public Commande? TrouverParId(int id)
    {
        return CommandesSauvegardees.FirstOrDefault(c => c.Id == id);
    }
}
```

```csharp
[Fact]
public void PasserCommande_StockOk_SauvegardeEnBase()
{
    var fakeRepo = new FakeCommandeRepository();
    var service = new CommandeService(fakeRepo, new FakeEmail(), new StubStock(true));

    service.PasserCommande(new Commande { Id = 1, ProduitId = 42, ClientEmail = "a@b.com" });

    Assert.Single(fakeRepo.CommandesSauvegardees);
    Assert.Equal(42, fakeRepo.CommandesSauvegardees[0].ProduitId);
}
```

<!--
Le fake se comporte comme une vraie BDD, mais en mémoire. Pas besoin de SQL Server.
C'est utile quand on veut vérifier que des données ont été correctement sauvegardées.
La différence avec un stub : le fake a un vrai comportement interne (une liste qui grandit).
-->

---

# Mock : vérifier les interactions

Quand on veut vérifier qu'une action a **eu lieu** (effet de bord).

```csharp
// Mock manuel — enregistre si Envoyer a été appelé
public class MockEmailService : IEmailService
{
    public bool EnvoyerAppele { get; private set; }
    public string? DernierDestinataire { get; private set; }

    public void Envoyer(string destinataire, string message)
    {
        EnvoyerAppele = true;
        DernierDestinataire = destinataire;
    }
}
```

```csharp
[Fact]
public void PasserCommande_StockOk_EnvoieEmailDeConfirmation()
{
    var mockEmail = new MockEmailService();
    var service = new CommandeService(new FakeRepo(), mockEmail, new StubStock(true));

    service.PasserCommande(new Commande { ClientEmail = "client@test.com", ProduitId = 1 });

    Assert.True(mockEmail.EnvoyerAppele);
    Assert.Equal("client@test.com", mockEmail.DernierDestinataire);
}
```

<!--
Le mock vérifie les SORTIES du système : est-ce que l'email a bien été envoyé ?
On ne vérifie pas le contenu de l'email (trop fragile), on vérifie qu'il a été envoyé au bon destinataire.
Écrire des mocks manuels est verbeux. C'est là que Moq entre en jeu.
-->

---

# Moq : simplifier les doublures

Écrire des doublures manuelles est **verbeux**. Moq génère les doublures automatiquement.

````md magic-move
```csharp
// ❌ Mock manuel — beaucoup de code pour pas grand-chose
public class MockEmailService : IEmailService
{
    public bool EnvoyerAppele { get; private set; }
    public string? DernierDestinataire { get; private set; }

    public void Envoyer(string destinataire, string message)
    {
        EnvoyerAppele = true;
        DernierDestinataire = destinataire;
    }
}
```

```csharp
// ✅ Avec Moq — une ligne pour créer, une ligne pour vérifier
[Fact]
public void PasserCommande_StockOk_EnvoieEmail()
{
    // Arrange
    var mockEmail = new Mock<IEmailService>();
    var stubStock = new Mock<IStockService>();
    stubStock.Setup(s => s.EstDisponible(It.IsAny<int>())).Returns(true);

    var service = new CommandeService(
        new FakeCommandeRepository(), mockEmail.Object, stubStock.Object);

    // Act
    service.PasserCommande(new Commande { ClientEmail = "a@b.com", ProduitId = 1 });

    // Assert — vérifier que Envoyer a été appelé avec le bon email
    mockEmail.Verify(e => e.Envoyer("a@b.com", It.IsAny<string>()), Times.Once);
}
```
````

<!--
Magic-move : on passe du mock manuel (verbeux) au mock Moq (concis).
Mock<T> crée une doublure automatique. Setup configure le comportement. Verify vérifie les appels.
Moq est le framework de mocking le plus utilisé en C#.
Montrer : stubStock.Setup = on contrôle l'entrée (stub), mockEmail.Verify = on vérifie la sortie (mock).
-->

---
layout: two-cols-header
---

# Règle : stub les entrées, mock les sorties

::left::

### Stub = données entrantes

<v-clicks>

- Le stock est-il disponible ? → `true`
- Quel est le prix du produit ? → `29.99`
- L'utilisateur existe-t-il ? → `new User(...)`
- On **contrôle** ce que la dépendance retourne

</v-clicks>

::right::

### Mock = effets sortants

<v-clicks>

- L'email a-t-il été envoyé ? → `Verify()`
- La commande a-t-elle été sauvegardée ? → `Verify()`
- Le log a-t-il été écrit ? → `Verify()`
- On **vérifie** que la dépendance a été appelée

</v-clicks>

<!--
Heuristique simple pour savoir quand utiliser un stub vs un mock.
Stub = on configure les données qui ENTRENT dans notre code.
Mock = on vérifie les actions qui SORTENT de notre code (les effets de bord).
Si la dépendance retourne une valeur qu'on utilise → stub.
Si la dépendance fait une action qu'on veut vérifier → mock.
-->
