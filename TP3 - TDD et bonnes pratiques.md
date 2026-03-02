# TP3 - TDD et bonnes pratiques

## Objectifs

- Evaluer la qualite d'un test avec les principes **FIRST**
- Pratiquer le cycle TDD (**Rouge** / **Vert** / **Refactor**)
- Decouvrir le TDD comme outil de **conception**
- Identifier le **biais de confirmation** du Test After

## Mise en place

Creez un nouveau projet de test xUnit dans votre solution. Utilisez le namespace `TestingTP3` pour le code et `TestingTP3.Tests` pour les tests.


<div class="page" />


## Exercice 1 - Diagnostic FIRST

### Contexte

Votre collegue a ecrit une classe `OrderService` et ses tests unitaires. Le code metier est correct, mais les tests posent probleme : ils violent un ou plusieurs principes **FIRST**.

Voici la classe `OrderService` :

```csharp
namespace TestingTP3;

public class OrderService
{
    private readonly List<Order> _orders = new();

    public void PlaceOrder(string product, int quantity, decimal unitPrice)
    {
        if (string.IsNullOrWhiteSpace(product))
            throw new ArgumentException("Le produit ne peut pas etre vide.");

        if (quantity <= 0)
            throw new ArgumentException("La quantite doit etre positive.");

        if (unitPrice < 0)
            throw new ArgumentException("Le prix ne peut pas etre negatif.");

        _orders.Add(new Order(product, quantity, unitPrice, DateTime.Now));
    }

    public decimal GetTotal()
    {
        return _orders.Sum(o => o.Quantity * o.UnitPrice);
    }

    public int OrderCount => _orders.Count;

    public string GetLastOrderSummary()
    {
        if (_orders.Count == 0)
            throw new InvalidOperationException("Aucune commande.");

        var last = _orders[^1];
        return $"{last.Product} x{last.Quantity} - {last.Total:F2} EUR ({last.Date:dd/MM/yyyy})";
    }
}

public record Order(string Product, int Quantity, decimal UnitPrice, DateTime Date)
{
    public decimal Total => Quantity * UnitPrice;
}
```

Et voici les 6 tests ecrits par votre collegue :

```csharp
using TestingTP3;

namespace TestingTP3.Tests;

public class OrderServiceTests
{
    private static OrderService _service = new();

    [Fact]
    public void Test1_PlaceOrder_IncreasesCount()
    {
        _service.PlaceOrder("Clavier", 1, 49.99m);

        Assert.Equal(1, _service.OrderCount);
    }

    [Fact]
    public void Test2_GetTotal_ReturnsTotalForOrder()
    {
        _service.PlaceOrder("Souris", 2, 25.00m);

        Assert.Equal(50.00m, _service.GetTotal());
    }

    [Fact]
    public void Test3_GetLastOrderSummary_ContainsToday()
    {
        var service = new OrderService();
        service.PlaceOrder("Ecran", 1, 299.99m);

        string summary = service.GetLastOrderSummary();

        string today = DateTime.Now.ToString("dd/MM/yyyy");
        Assert.Contains(today, summary);
    }

    [Fact]
    public async Task Test4_PlaceOrder_AfterCatalogCheck()
    {
        using var http = new HttpClient();
        var response = await http.GetAsync("https://jsonplaceholder.typicode.com/posts/1");
        Assert.True(response.IsSuccessStatusCode);

        var service = new OrderService();
        service.PlaceOrder("Clavier", 1, 49.99m);
        Assert.Equal(1, service.OrderCount);
    }

    [Fact]
    public void Test5_PlaceOrder_DisplaysCorrectly()
    {
        var service = new OrderService();
        service.PlaceOrder("Casque", 1, 79.99m);

        Console.WriteLine("Commande : " + service.GetLastOrderSummary());
        Console.WriteLine("Total : " + service.GetTotal());
    }

    [Fact]
    public void Test6_GetTotal_PersistsToFile()
    {
        var service = new OrderService();
        service.PlaceOrder("Cable USB", 3, 9.99m);

        File.WriteAllText("order_total.txt", service.GetTotal().ToString());

        string saved = File.ReadAllText("order_total.txt");
        Assert.Equal(service.GetTotal().ToString(), saved);
    }
}
```

### Etape 1 - Diagnostic

Pour chaque test, identifiez le ou les principes FIRST violes. Remplissez le tableau suivant :

| Test | Principe(s) viole(s) | Pourquoi c'est un probleme | Correction proposee |
|------|----------------------|---------------------------|---------------------|
| Test 1 | | | |
| Test 2 | | | |
| Test 3 | | | |
| Test 4 | | | |
| Test 5 | | | |
| Test 6 | | | |

**Rappel des principes FIRST :**

| Lettre | Principe | Un bon test... |
|--------|----------|----------------|
| **F** | Fast | S'execute en millisecondes |
| **I** | Isolated | Ne depend d'aucun autre test ni d'etat partage |
| **R** | Repeatable | Donne le meme resultat a chaque execution |
| **S** | Self-validating | Produit un verdict Pass/Fail sans interpretation |
| **T** | Timely | Est ecrit au bon moment |

### Etape 2 - Reecriture

Choisissez **3 tests parmi les 6** et reecrivez-les pour qu'ils respectent les principes FIRST.

### Etape 3 - Verification

Pour chacun de vos tests reecrits, verifiez qu'il respecte **les 5 principes** :

- Est-il **rapide** (pas d'appel reseau, pas de fichier) ?
- Est-il **isole** (pas d'etat partage entre tests) ?
- Est-il **repetable** (meme resultat a chaque execution) ?
- Est-il **auto-validant** (assertion explicite, pas de `Console.WriteLine`) ?
- Est-il **opportun** (ecrit au bon moment) ?


<div class="page" />

---

## Exercice 2 - Chiffres romains en TDD

### Contexte

Vous allez implementer un convertisseur de nombres entiers en chiffres romains, en suivant strictement le cycle TDD : **Rouge / Vert / Refactor**.

**Regle importante** : faites un `git commit` a chaque phase **Verte**. Cela vous permettra de voir l'evolution de votre code.

Creez la classe suivante :

```csharp
namespace TestingTP3;

public class RomanNumerals
{
    public static string Convert(int number)
    {
        throw new NotImplementedException();
    }
}
```

### Etape 1 - Le premier test

Ecrivez le test suivant :

```csharp
using TestingTP3;

namespace TestingTP3.Tests;

public class RomanNumeralsTests
{
    [Fact]
    public void Convert_1_ReturnsI()
    {
        Assert.Equal("I", RomanNumerals.Convert(1));
    }
}
```

**Rouge** : Lancez le test. Il echoue.

**Vert** : Ecrivez le **minimum de code** pour le faire passer. Quel est le code le plus simple possible ?

**Commitez** : `git commit -m "TDD: Convert(1) returns I"`

### Etape 2 - Repetition

Ajoutez les cas suivants :

```csharp
[Theory]
[InlineData(1, "I")]
[InlineData(2, "II")]
[InlineData(3, "III")]
public void Convert_SmallNumbers_ReturnsCorrectRoman(int number, string expected)
{
    Assert.Equal(expected, RomanNumerals.Convert(number));
}
```

**Rouge** : Les tests pour 2 et 3 echouent.

**Vert** : Adaptez votre code. Un pattern de repetition devrait emerger.

**Commitez** : `git commit -m "TDD: Convert handles 1, 2, 3"`

### Etape 3 - Le moment cle

Ajoutez ce test :

```csharp
[Fact]
public void Convert_4_ReturnsIV()
{
    Assert.Equal("IV", RomanNumerals.Convert(4));
}
```

**Rouge** : Votre approche actuelle ne peut pas produire `"IV"`. C'est normal.

**Vert** : Faites passer le test. Vous devrez probablement repenser votre approche.

Que constatez-vous ? L'ajout d'un seul test vous a force a changer de strategie. C'est exactement le role du TDD : les tests **guident le design**.

**Commitez** : `git commit -m "TDD: Convert handles 4 (subtractive form)"`

### Etape 4 - Le pattern soustractif

Ajoutez les cas suivants un par un, en faisant Rouge / Vert / Commit a chaque fois :

```csharp
[Theory]
[InlineData(5, "V")]
[InlineData(9, "IX")]
[InlineData(10, "X")]
public void Convert_SubtractiveForms_ReturnsCorrectRoman(int number, string expected)
{
    Assert.Equal(expected, RomanNumerals.Convert(number));
}
```

Voyez-vous un pattern se dessiner dans votre code ?

**Commitez** apres chaque phase verte.

### Etape 5 - Monter en puissance

Ajoutez les valeurs suivantes :

```csharp
[Theory]
[InlineData(40, "XL")]
[InlineData(50, "L")]
[InlineData(90, "XC")]
[InlineData(100, "C")]
[InlineData(400, "CD")]
[InlineData(500, "D")]
[InlineData(900, "CM")]
[InlineData(1000, "M")]
public void Convert_LargerValues_ReturnsCorrectRoman(int number, string expected)
{
    Assert.Equal(expected, RomanNumerals.Convert(number));
}
```

A ce stade, une **table de correspondance** devrait emerger naturellement dans votre code. Si ce n'est pas le cas, c'est le moment de refactorer.

**Commitez** apres chaque phase verte.

### Etape 6 - Verification

Ajoutez ces tests composites :

```csharp
[Theory]
[InlineData(42, "XLII")]
[InlineData(99, "XCIX")]
[InlineData(2024, "MMXXIV")]
public void Convert_CompositeNumbers_ReturnsCorrectRoman(int number, string expected)
{
    Assert.Equal(expected, RomanNumerals.Convert(number));
}
```

Si votre implementation est correcte, **ces tests passent sans modifier le code**. C'est le signe d'un bon design.

### Etape 7 - Reflexion

Voici la solution qui devrait avoir emerge de votre demarche TDD :

```csharp
public static string Convert(int number)
{
    int[] values =    { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
    string[] symbols = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

    string result = "";
    for (int i = 0; i < values.Length; i++)
    {
        while (number >= values[i])
        {
            result += symbols[i];
            number -= values[i];
        }
    }
    return result;
}
```

Repondez aux questions suivantes :

1. A quelle etape avez-vous senti que le design "emergeait" des tests ?
2. Qu'est-ce qui s'est passe quand vous avez ajoute le test pour `4` ? Avez-vous du reecrire du code existant ?
3. Auriez-vous pu deviner la solution finale (table de correspondance) a l'etape 1 ?
4. Regardez votre historique git. Comment votre code a-t-il evolue ?

> Le TDD n'est pas qu'une technique de test. C'est un **outil de conception** : les tests guident le design du code.


<div class="page" />

---

## Exercice 3 - Reparer du "Test After"

### Contexte

Un collegue a developpe une classe `ShoppingCart` puis a ecrit les tests **apres coup**. Il vous assure que "tout est teste et tout passe".

Voici sa classe :

```csharp
namespace TestingTP3;

public class ShoppingCart
{
    private readonly List<CartItem> _items = new();

    public void AddItem(string product, decimal unitPrice, int quantity = 1)
    {
        _items.Add(new CartItem(product, unitPrice, quantity));
    }

    public decimal CalculateTotal()
    {
        decimal total = _items.Sum(i => i.UnitPrice * i.Quantity);
        return Math.Round(total, 1);
    }

    public decimal ApplyDiscount(string code)
    {
        decimal total = CalculateTotal();

        if (code == "SOLDES20")
            return Math.Round(total * 0.80m, 2);
        if (code == "PROMO10")
            return Math.Round(total * 0.90m, 2);

        return 0m;
    }

    public int ItemCount => _items.Count;

    public List<CartItem> GetItems() => _items.ToList();
}

public record CartItem(string Product, decimal UnitPrice, int Quantity);
```

Et voici ses tests :

```csharp
using TestingTP3;

namespace TestingTP3.Tests;

public class ShoppingCartTests
{
    [Fact]
    public void AddItem_SingleItem_AppearsInCart()
    {
        var cart = new ShoppingCart();
        cart.AddItem("Stylo", 2.00m);

        Assert.Equal(1, cart.ItemCount);
    }

    [Fact]
    public void CalculateTotal_SingleItem_ReturnsPrice()
    {
        var cart = new ShoppingCart();
        cart.AddItem("Cahier", 5.00m);

        Assert.Equal(5.00m, cart.CalculateTotal());
    }

    [Fact]
    public void CalculateTotal_MultipleItems_ReturnsSum()
    {
        var cart = new ShoppingCart();
        cart.AddItem("Cahier", 5.00m);
        cart.AddItem("Stylo", 2.00m);

        Assert.Equal(7.00m, cart.CalculateTotal());
    }

    [Fact]
    public void ApplyDiscount_SOLDES20_Returns80Percent()
    {
        var cart = new ShoppingCart();
        cart.AddItem("Sac", 50.00m);

        Assert.Equal(40.00m, cart.ApplyDiscount("SOLDES20"));
    }

    [Fact]
    public void ItemCount_ThreeItems_ReturnsThree()
    {
        var cart = new ShoppingCart();
        cart.AddItem("Stylo", 2.00m);
        cart.AddItem("Cahier", 5.00m);
        cart.AddItem("Gomme", 1.00m);

        Assert.Equal(3, cart.ItemCount);
    }
}
```

### Etape 1 - Lancer les tests

Lancez les tests. Ils passent tous au vert.

Le code est-il pour autant correct ?

### Etape 2 - Lire la specification

Voici le comportement attendu du `ShoppingCart` :

| Methode | Comportement attendu |
|---------|---------------------|
| `AddItem` | Ajoute un article au panier. Si le produit existe deja, **augmente la quantite** au lieu de creer un doublon |
| `CalculateTotal` | Retourne la somme (prix x quantite) de tous les articles, arrondie a **2 decimales** |
| `ApplyDiscount` | Applique un code promo (`"SOLDES20"` = -20%, `"PROMO10"` = -10%). Si le code est **inconnu**, retourne le **prix total sans reduction** |
| `ItemCount` | Retourne le **nombre total d'articles** dans le panier (somme des quantites) |

Comparez cette specification avec le code. Identifiez les ecarts.

### Etape 3 - Ecrire les tests qui revelent les bugs

Ecrivez **4 nouveaux tests** qui echouent et mettent en evidence les bugs. Voici des indices :

1. **Indice** : Ajoutez le meme produit deux fois avec `AddItem`. Combien de lignes voyez-vous dans `GetItems()` ?
2. **Indice** : Essayez avec des prix a centimes non arrondis, par exemple `19.99m` multiplie par une quantite de `3`.
3. **Indice** : Que retourne `ApplyDiscount` avec un code promo qui n'existe pas, comme `"INEXISTANT"` ?
4. **Indice** : Ajoutez un article avec une quantite de `5`. Que retourne `ItemCount` ?

### Etape 4 - Corriger les bugs

Corrigez l'implementation de `ShoppingCart` pour que **tous les tests** (anciens et nouveaux) passent.

### Etape 5 - Reflexion

Repondez aux questions suivantes :

1. Pourquoi les tests originaux ne detectaient-ils aucun bug ?
2. Quel type de valeurs les tests utilisaient-ils ? Regardez les prix, les quantites, les produits.
3. C'est le **biais de confirmation** : quand on ecrit les tests apres le code, on teste inconsciemment ce qu'on sait qui marche. Comment le TDD aurait-il evite ce probleme ?

---

## Recapitulatif

| Concept | Ou l'avez-vous pratique ? |
|---------|--------------------------|
| Principes FIRST | Exercice 1, diagnostic et reecriture |
| **F**ast | Exercice 1, Test 4 (appel HTTP) |
| **I**solated | Exercice 1, Tests 1-2 (etat statique partage), Test 6 (fichier partage) |
| **R**epeatable | Exercice 1, Test 3 (DateTime.Now), Test 4 (reseau), Test 6 (fichier) |
| **S**elf-validating | Exercice 1, Test 5 (Console.WriteLine sans assertion) |
| Cycle TDD (Rouge/Vert/Refactor) | Exercice 2, toutes les etapes |
| TDD comme outil de conception | Exercice 2, etapes 3 et 7 |
| Refactoring sous couverture de tests | Exercice 2, etape 5 |
| Biais de confirmation | Exercice 3, etape 5 |
| Discipline de commit git | Exercice 2, commit a chaque phase verte |
