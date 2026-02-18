# Les principes FIRST

Un bon test unitaire respecte 5 propriétés.

<v-clicks>

| Lettre | Principe | Ce que ça signifie |
|---|---|---|
| **F** | **Fast** | S'exécute en millisecondes |
| **I** | **Isolated** | Ne dépend d'aucun autre test, ni d'état partagé |
| **R** | **Repeatable** | Donne le même résultat à chaque exécution |
| **S** | **Self-validating** | Passe ou échoue sans interprétation manuelle |
| **T** | **Timely** | Écrit au bon moment (en TDD : avant le code) |

</v-clicks>

<!--
FIRST est un acronyme mnémotechnique pour les qualités d'un bon test unitaire.
Ces critères sont interdépendants : un test lent est souvent lent parce qu'il n'est pas isolé (accès BDD, réseau...).
Un test non-déterministe (random, date du jour...) viole R et S à la fois.
-->

---

# FIRST — Exemples de violations

<br>

````md magic-move
```csharp
// ❌ Viole I et R : dépend d'un fichier externe et d'un état global
[Fact]
public void ProcessOrder_SavesToFile()
{
    var processor = new OrderProcessor();
    processor.Process(new Order { Id = 1 });

    // Le fichier existe-t-il déjà d'un test précédent ?
    var content = File.ReadAllText("orders.txt");
    Assert.Contains("Order 1", content);
}
```

```csharp
// ❌ Viole F et R : dépend du réseau et d'un service externe
[Fact]
public async Task GetUser_ReturnsUserFromApi()
{
    var client = new UserApiClient("https://api.example.com");

    // Lent, fragile (réseau), non-déterministe (API peut changer)
    var user = await client.GetUser(42);

    Assert.Equal("Alice", user.Name);
}
```

```csharp
// ✅ Respecte FIRST : rapide, isolé, déterministe, auto-validant
[Fact]
public void CalculateDiscount_PremiumUser_Returns20Percent()
{
    var service = new DiscountService();
    var user = new User { IsPremium = true };

    var discount = service.Calculate(user, amount: 100m);

    Assert.Equal(20m, discount); // Pass/Fail sans interprétation
}
```
````

<!--
Les deux premiers exemples montrent des anti-patterns courants.
Le test avec fichier crée de l'état partagé entre tests → ordre d'exécution imprévisible.
Le test avec API externe n'est pas un test unitaire, c'est un test d'intégration déguisé.
Le dernier exemple : pas de dépendances externes, résultat déterministe, verdict binaire.
-->
