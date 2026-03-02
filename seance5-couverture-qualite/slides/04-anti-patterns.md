# Anti-patterns de test (test smells)

<br>

Des **signes** que vos tests ont un problème :

<v-clicks>

| Smell | Symptôme | Problème |
|-------|----------|----------|
| **Assertion tautologique** | `Assert.True(true)` | Ne vérifie rien |
| **Test de l'implémentation** | Vérifie l'ordre des appels internes | Casse au moindre refactoring |
| **Logique dans le test** | `if`, boucles, calculs dans le test | Le test peut lui-même être bugué |
| **Tests interdépendants** | Un test dépend du résultat d'un autre | Ordre d'exécution imprévisible |
| **Test fragile** | Échoue de façon intermittente | Dépend du temps, du réseau, de l'état global |
| **Test de méthode privée** | Contourne l'encapsulation | Couplage à l'implémentation |

</v-clicks>

<!--
Ces smells sont des signaux d'alarme. On va voir chacun en détail.
-->

---

# Assertion tautologique

<br>

````md magic-move
```csharp
// ❌ Ce test a 100% de couverture mais ne teste RIEN
[Fact]
public void Process_DoesNotThrow()
{
    var service = new OrderService();
    service.Process(new Order());
    Assert.True(true); // Toujours vrai, quel que soit le comportement
}
```

```csharp
// ✅ Ce test vérifie un RÉSULTAT observable
[Fact]
public void Process_ValidOrder_SetsStatusToCompleted()
{
    var service = new OrderService();
    var order = new Order { Items = { new Item(10m) } };

    service.Process(order);

    Assert.Equal(OrderStatus.Completed, order.Status);
}
```
````

<v-click>

<br>

**Règle :** chaque test doit vérifier au moins un **résultat observable** (valeur retournée, état modifié, exception levée).

</v-click>

<!--
L'assertion tautologique est le pire smell : elle donne de la couverture sans aucune valeur.
Un mutant survivrait à ce test à 100%.
-->

---

# Logique dans le test

<br>

````md magic-move
```csharp
// ❌ Le test contient de la logique — il peut être bugué !
[Fact]
public void CalculateTotal_AppliesDiscount()
{
    var items = new[] { 100m, 200m, 50m };
    var service = new PricingService();

    var total = service.CalculateTotal(items, discount: 0.1m);

    // Le test recalcule la même logique → ne prouve rien
    var expected = items.Sum() * (1 - 0.1m);
    Assert.Equal(expected, total);
}
```

```csharp
// ✅ La valeur attendue est une constante — pas de logique
[Fact]
public void CalculateTotal_10PercentDiscount_Returns315()
{
    var items = new[] { 100m, 200m, 50m };
    var service = new PricingService();

    var total = service.CalculateTotal(items, discount: 0.1m);

    Assert.Equal(315m, total); // Valeur calculée à la main
}
```
````

<v-click>

<br>

**Règle :** les valeurs attendues doivent être des **constantes**, pas des calculs.

</v-click>

<!--
Si le test recalcule la même chose que le code de production, une erreur dans la formule passera inaperçue.
La valeur attendue doit être "évidente" ou calculée indépendamment.
-->

---

# Tester des méthodes privées

<br>

<v-clicks>

### ❌ Ne testez pas les méthodes privées directement

- Les méthodes privées sont un **détail d'implémentation**
- Si on a besoin de les tester directement, c'est un signe que la classe en fait trop
- Rendre une méthode `internal` ou `public` juste pour les tests = mauvais design

### ✅ Testez le comportement PUBLIC qui utilise la méthode privée

```csharp
// La méthode privée CalculateDiscount est testée indirectement
// via le comportement public ProcessOrder
[Fact]
public void ProcessOrder_PremiumUser_AppliesDiscount()
{
    var service = new OrderService();
    var order = new Order { Customer = new Customer { IsPremium = true }, Total = 100m };

    service.Process(order);

    Assert.Equal(80m, order.FinalTotal); // Le discount a été appliqué
}
```

</v-clicks>

<!--
Si une méthode privée a une logique suffisamment complexe pour mériter ses propres tests, c'est un signe qu'elle devrait être extraite dans sa propre classe (SRP).
-->

---

# Characterization Tests — Tester le comportement existant

<br>

<v-clicks>

### Le contexte : du code legacy sans tests

Vous héritez d'un code sans tests. Avant de refactorer, il faut **sécuriser le comportement actuel**.

### La technique

1. Appeler le code existant avec des entrées connues
2. **Capturer** la sortie réelle (même si elle semble incorrecte)
3. Écrire un test qui **assert sur cette sortie**
4. Maintenant vous pouvez refactorer **en sécurité**

</v-clicks>

<v-click>

```csharp
// Characterization test : on capture le comportement ACTUEL, pas le comportement "correct"
[Fact]
public void CalculatePrice_LegacyBehavior_Returns108()
{
    var service = new LegacyPricingService();

    var result = service.CalculatePrice(quantity: 5, unitPrice: 20m);

    // Le résultat est 108 (pas 100) — il y a probablement un bug de taxe,
    // mais on capture le comportement actuel comme filet de sécurité
    Assert.Equal(108m, result);
}
```

</v-click>

<!--
Le terme "Characterization Test" vient de Michael Feathers dans "Working Effectively with Legacy Code".
L'objectif n'est PAS de valider que le code est correct, mais de créer un filet de sécurité avant de le modifier.
Une fois les characterization tests en place, on peut refactorer en toute confiance : si un test casse, on sait qu'on a changé un comportement.
-->
