# TP5 - Qualite des tests

## Objectifs

- Identifier les **test smells** dans une suite de tests existante
- Ecrire des **characterization tests** pour capturer le comportement d'un code legacy
- **Reecrire** des tests de mauvaise qualite en tests pertinents
- Mesurer la qualite des tests avec **Stryker** (tests de mutation)

## Mise en place

Creez un nouveau projet de test xUnit dans votre solution et copiez-y le code fourni ci-dessous.

```bash
dotnet new xunit -n TestingTP5.Tests
```


<div class="page" />


## Le code de production : LegacyPricingService

Voici le service que vous allez tester. **Ne le modifiez pas** pendant les exercices 1 et 2.

```csharp
namespace TestingTP5;

public class LegacyPricingService
{
    private decimal _lastTotal = 0;

    public decimal CalculateTotal(
        List<decimal> itemPrices,
        decimal discountPercent,
        bool isPremiumCustomer,
        int loyaltyYears)
    {
        if (itemPrices == null || itemPrices.Count == 0)
            return 0;

        decimal subtotal = 0;
        foreach (var price in itemPrices)
            subtotal += price;

        // Remise standard
        decimal discount = subtotal * (discountPercent / 100m);
        decimal afterDiscount = subtotal - discount;

        // Remise fidelite
        if (loyaltyYears >= 5)
            afterDiscount *= 0.95m; // 5% supplementaire
        else if (loyaltyYears >= 2)
            afterDiscount *= 0.98m; // 2% supplementaire

        // Remise premium
        if (isPremiumCustomer)
            afterDiscount *= 0.90m; // 10% premium

        // Remise volume (bulk)
        if (itemPrices.Count >= 10)
            afterDiscount *= 0.85m; // 15% volume
        else if (itemPrices.Count >= 5)
            afterDiscount *= 0.92m; // 8% volume

        // Taxe — attention : taux different selon le montant
        decimal tax;
        if (afterDiscount > 1000)
            tax = afterDiscount * 0.20m; // 20% TVA
        else
            tax = afterDiscount * 0.08m; // 8% TVA reduite (bug probable)

        decimal total = afterDiscount + tax;

        // Arrondi
        total = Math.Round(total, 2);

        _lastTotal = total;
        return total;
    }

    public decimal GetLastTotal() => _lastTotal;
}
```


<div class="page" />


## La suite de tests existante (pleine de smells)

Voici la suite de tests que votre collegue a ecrite. Elle "passe au vert" mais est truffee de problemes.

```csharp
namespace TestingTP5.Tests;

public class PricingTests
{
    private static LegacyPricingService _service = new();

    [Fact]
    public void Test1()
    {
        var result = _service.CalculateTotal(
            new List<decimal> { 100m }, 0, false, 0);
        Assert.True(true);
    }

    [Fact]
    public void Test2()
    {
        var result = _service.CalculateTotal(
            new List<decimal> { 100m }, 0, false, 0);
        Assert.NotNull(result.ToString());
    }

    [Fact]
    public void TestCalculation()
    {
        var prices = new List<decimal> { 100m, 200m, 50m };
        var result = _service.CalculateTotal(prices, 10m, false, 0);

        var expected = prices.Sum() * (1 - 10m / 100m) * 1.08m;
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestCalculation2()
    {
        var prices = new List<decimal> { 100m, 200m, 50m };
        var result = _service.CalculateTotal(prices, 10m, false, 0);

        var expected = prices.Sum() * (1 - 10m / 100m) * 1.08m;
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestCalculation3()
    {
        var prices = new List<decimal> { 100m, 200m, 50m };
        var result = _service.CalculateTotal(prices, 10, false, 0);

        var expected = prices.Sum() * (1 - 10m / 100m) * 1.08m;
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestPremium()
    {
        _service.CalculateTotal(new List<decimal> { 500m }, 0, true, 0);
        var result = _service.GetLastTotal();
        Assert.True(result > 0);
    }

    [Fact]
    public void TestDependsOnPrevious()
    {
        // Ce test depend du fait que TestPremium a ete execute avant
        var last = _service.GetLastTotal();
        Assert.True(last > 0);
    }

    [Fact]
    public void TestInternalMethod()
    {
        var service = new LegacyPricingService();
        var method = typeof(LegacyPricingService)
            .GetMethod("CalculateTotal");
        var result = method!.Invoke(service, new object[]
        {
            new List<decimal> { 100m }, 0m, false, 0
        });
        Assert.NotNull(result);
    }

    [Fact]
    public void TestEmpty()
    {
        var result = _service.CalculateTotal(new List<decimal>(), 0, false, 0);
        Assert.Equal(0m, result);
    }

    [Fact]
    public void TestNull()
    {
        var result = _service.CalculateTotal(null!, 0, false, 0);
        Assert.Equal(0m, result);
    }

    [Fact]
    public void TestSingleItem()
    {
        var result = _service.CalculateTotal(
            new List<decimal> { 100m }, 0, false, 0);
        Assert.Equal(108m, result);
    }

    [Fact]
    public void TestDiscount_ChecksMockCallOrder()
    {
        // Simule une verification d'ordre d'appel
        var service = new LegacyPricingService();
        var result1 = service.CalculateTotal(
            new List<decimal> { 100m }, 10, false, 0);
        var result2 = service.GetLastTotal();

        Assert.Equal(result1, result2);
        // Ce test ne verifie pas le bon resultat,
        // il verifie que l'etat interne est coherent
    }

    [Fact]
    public void Test_PricesArePositive()
    {
        var prices = new List<decimal> { 10m, 20m, 30m };
        Assert.All(prices, p => Assert.True(p > 0));
        // Teste les donnees de test, pas le service !
    }
}
```


<div class="page" />


## Exercice 1 - Identifier les test smells (20 min)

### Objectif

Lisez la suite de tests ci-dessus. Identifiez et classifiez chaque smell.

### Criteres de reussite

- Identifiez au moins **5 smells differents**
- Pour chaque smell, indiquez :
  - **Le test concerne** (nom de la methode)
  - **Le type de smell** (assertion tautologique, logique dupliquee, tests interdependants, test d'implementation, nom vague, test redondant, test de methode privee, assertion sur les donnees de test)
  - **Une phrase d'explication** : pourquoi c'est un probleme

Notez vos reponses dans un commentaire en haut du fichier de test, ou dans un fichier `smells.md` separe.


<div class="page" />

---

## Exercice 2 - Characterization Tests (30 min)

### Objectif

Avant de reecrire les tests, vous devez comprendre ce que `LegacyPricingService` fait **reellement**. Ecrivez des characterization tests qui capturent son comportement actuel.

### Criteres de reussite

- Appeler `CalculateTotal` avec au moins **8 combinaisons d'entrees** couvrant :
  - Montant bas (< 1000 apres remise) et montant haut (> 1000 apres remise)
  - Avec et sans remise
  - Client premium et standard
  - Differentes valeurs de `loyaltyYears` (0, 2, 5, 10)
  - Nombre d'items faible (1-4), moyen (5-9), et eleve (10+)
- **Capturer les resultats reels** dans les assertions, meme si certains semblent incorrects
- **Documenter en commentaire** au moins 1 comportement qui semble etre un bug
- Tous les characterization tests passent au vert — ils capturent le comportement **actuel**, pas le comportement "correct"

### Methode

1. Appelez `CalculateTotal` avec des entrees connues
2. Lancez le test **sans assertion** — observez la valeur retournee
3. Ecrivez l'assertion avec la valeur **reelle** obtenue
4. Si la valeur vous surprend, ajoutez un commentaire explicatif

```csharp
public class LegacyPricingCharacterizationTests
{
    private readonly LegacyPricingService _service = new();

    [Fact]
    public void SingleItem_NoDiscount_StandardCustomer()
    {
        var result = _service.CalculateTotal(
            new List<decimal> { 100m }, 0, false, 0);

        // Comportement capture : 100 * 1.08 = 108
        // Note : taxe a 8% pour les montants <= 1000 (bug probable ? TVA normale = 20%)
        Assert.Equal(108m, result);
    }

    // A vous d'ecrire les 7+ autres tests...
}
```


<div class="page" />

---

## Exercice 3 - Reecrire les tests (25 min)

### Objectif

Remplacez la suite de tests de votre collegue par des tests de qualite.

### Criteres de reussite

- Chaque test a un **nom descriptif** qui documente le comportement (ex: `CalculateTotal_PremiumCustomer_Applies10PercentDiscount`)
- **Aucune logique** dans les tests — les valeurs attendues sont des constantes calculees a la main
- Les tests sont **isoles** les uns des autres (pas d'etat partage mutable)
- Les characterization tests de l'exercice 2 servent de **filet de securite** : si vos nouveaux tests et les characterization tests passent tous, vous n'avez rien casse
- Chaque branche du code est couverte par au moins un test :
  - Liste vide / null
  - Pas de remise
  - Remise standard
  - Remise fidelite (2 ans, 5 ans)
  - Remise premium
  - Remise volume (5 items, 10 items)
  - Seuil de taxe (sous et au-dessus de 1000)


<div class="page" />

---

## Exercice 4 - Stryker avant/apres (15 min)

### Objectif

Mesurer la qualite des tests originaux de votre collegue vs vos tests reecrits avec les tests de mutation.

### Instructions

**1.** Installez Stryker :

```bash
dotnet tool install -g dotnet-stryker
```

**2.** Lancez Stryker sur les tests **originaux** (ceux de votre collegue). Notez le mutation score.

```bash
dotnet stryker
```

**3.** Remplacez par vos tests reecrits. Relancez Stryker. Notez le nouveau score.

### Criteres de reussite

- Mutation score des tests originaux documente (~30-50% attendu)
- Mutation score des tests reecrits significativement meilleur (~80-90% attendu)
- Vous pouvez expliquer pourquoi au moins **2 mutants survivants specifiques** n'ont pas ete tues

### Questions

Pour chaque mutant survivant, demandez-vous :
- Quel test manque-t-il pour tuer ce mutant ?
- Est-ce un mutant qu'il est **important** de tuer, ou un cas equivalent ?


<div class="page" />

---

## Formation des groupes (30 min)

C'est le moment de former les groupes pour le projet des seances 6, 7 et 8.

### Instructions

1. Formez des groupes de **3-4 etudiants**
2. Choisissez un sujet de projet (ou proposez le votre) :
   - Un systeme de gestion de bibliotheque (API)
   - Un systeme de reservation
   - Un moteur de panier e-commerce + checkout
   - Un gestionnaire de taches en CLI avec persistance
3. Commencez a reflechir au **domaine** : quelles entites ? quelles regles metier ?
4. Pensez a la **strategie de test** : quels niveaux de test utiliserez-vous et pour quoi ?

---

## Recapitulatif

| Concept | Ou l'avez-vous pratique ? |
|---------|--------------------------|
| Assertion tautologique | Exercice 1 |
| Logique dans le test | Exercice 1 |
| Tests interdependants | Exercice 1 |
| Test de methode privee | Exercice 1 |
| Tests redondants | Exercice 1 |
| Noms de tests vagues | Exercice 1 |
| Characterization Tests | Exercice 2 |
| Capturer le comportement legacy | Exercice 2 |
| Noms de tests descriptifs | Exercice 3 |
| Valeurs attendues en dur (pas de logique) | Exercice 3 |
| Isolation des tests | Exercice 3 |
| Stryker / tests de mutation | Exercice 4 |
| Mutation score | Exercice 4 |
| Mutants survivants | Exercice 4 |
