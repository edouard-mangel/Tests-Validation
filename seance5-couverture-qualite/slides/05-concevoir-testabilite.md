# Concevoir pour la testabilité

<br>

<v-clicks>

Du code **difficile à tester** est un signal de **problème de conception**.

Les principes **SOLID** vus sous l'angle de la testabilité :

| Principe | Testabilité |
|----------|-------------|
| **S** — Single Responsibility | Une classe = un sujet à tester |
| **O** — Open/Closed | On peut étendre sans modifier les tests existants |
| **L** — Liskov Substitution | Les doublures peuvent remplacer les vraies implémentations |
| **I** — Interface Segregation | Les interfaces sont petites → faciles à doubler |
| **D** — Dependency Inversion | On injecte des doublures via les interfaces |

</v-clicks>

<!--
SOLID n'est pas juste un ensemble de règles théoriques.
C'est un guide pratique pour écrire du code qu'on PEUT tester.
Si vous n'arrivez pas à tester une classe, relisez SOLID.
-->

---

# SRP — Single Responsibility et testabilité

<br>

````md magic-move
```csharp
// ❌ Cette classe fait TOUT → impossible à tester unitairement
public class OrderProcessor
{
    public void Process(Order order)
    {
        // Validation
        if (order.Items.Count == 0) throw new Exception("No items");

        // Calcul
        order.Total = order.Items.Sum(i => i.Price);
        if (order.Customer.IsPremium) order.Total *= 0.8m;

        // Persistance
        var db = new SqlConnection("...");
        db.Execute("INSERT INTO Orders ...", order);

        // Notification
        var smtp = new SmtpClient("...");
        smtp.Send(new MailMessage(...));
    }
}
```

```csharp
// ✅ Chaque responsabilité est une classe testable indépendamment
public class OrderProcessor
{
    private readonly IOrderValidator _validator;
    private readonly IPricingService _pricing;
    private readonly IOrderRepository _repository;
    private readonly INotificationService _notifier;

    public OrderProcessor(
        IOrderValidator validator, IPricingService pricing,
        IOrderRepository repository, INotificationService notifier)
    {
        _validator = validator;
        _pricing = pricing;
        _repository = repository;
        _notifier = notifier;
    }

    public void Process(Order order)
    {
        _validator.Validate(order);
        _pricing.ApplyPricing(order);
        _repository.Save(order);
        _notifier.NotifyCustomer(order);
    }
}
```
````

<!--
La version refactorisée est testable : on peut injecter des doublures pour chaque dépendance.
Chaque sous-service (validator, pricing, etc.) a ses propres tests unitaires.
Le OrderProcessor lui-même orchestre — on le teste avec des doublures.
-->

---

# DIP — Dependency Inversion et testabilité

<br>

<v-clicks>

Le **Dependency Inversion Principle** est la clé de voûte de la testabilité :

```
❌ SANS DIP : le code haut-niveau dépend du code bas-niveau
   OrderService → SqlOrderRepository → SQL Server

✅ AVEC DIP : les deux dépendent d'une abstraction
   OrderService → IOrderRepository ← SqlOrderRepository
                                    ← FakeOrderRepository (test)
```

Sans DIP : on ne peut pas tester `OrderService` sans SQL Server.

Avec DIP : on injecte un `FakeOrderRepository` et on teste en isolation.

</v-clicks>

<v-click>

<br>

> Si vous ne retenez qu'un principe SOLID pour la testabilité : **DIP**.

</v-click>

<!--
C'est ce qu'on a vu en séance 3 avec l'injection de dépendances.
DIP + DI = testabilité.
-->

---
layout: two-cols-header
---

# London school vs Detroit school

<br>

Deux écoles de TDD avec des philosophies différentes :

::left::

### London school (Outside-In)

<v-clicks>

- On part de l'**extérieur** (API, contrôleur)
- On **mocke** les dépendances internes
- Le design émerge de **haut en bas**
- Beaucoup de mocks
- Tests couplés aux **interactions**

</v-clicks>

::right::

### Detroit school (Inside-Out)

<v-clicks>

- On part de l'**intérieur** (domaine, logique)
- On utilise les **vrais objets** quand possible
- Le design émerge de **bas en haut**
- Peu de mocks (seulement aux frontières)
- Tests couplés aux **résultats**

</v-clicks>

<!--
Il n'y a pas de "bonne" école. Les deux ont leurs mérites.
En pratique, la plupart des développeurs utilisent un mélange des deux.
La clé : être conscient de l'approche qu'on utilise et de ses conséquences sur les tests.
-->

---

# Résumé — Qualité des tests

<br>

<v-clicks>

Ce qui fait de **bons tests** :

1. **Vérifient le comportement**, pas l'implémentation
2. **Valeurs attendues en dur**, pas de logique dans le test
3. **Noms descriptifs** qui documentent le comportement
4. **Isolation** : un test ne dépend pas d'un autre
5. **Mocks avec parcimonie** : préférer stubs et fakes
6. **Couverture comme outil**, pas comme objectif
7. **Mutation score** pour valider la qualité réelle

</v-clicks>

<v-click>

<br>

> La qualité des tests est plus importante que la quantité.

</v-click>

<!--
Ce résumé reprend tout ce qu'on a vu depuis la séance 1.
Ce sont les critères qui seront évalués dans le projet de groupe.
-->

---

# Panorama — Autres types de tests

<br>

<v-clicks>

Au-delà de ce module, d'autres types de tests existent dans l'industrie :

| Type | Objectif | Outils |
|------|----------|--------|
| **Chaos testing** | Injecter des pannes pour tester la résilience | Netflix Chaos Monkey, Litmus |
| **Performance / load testing** | Mesurer les temps de réponse sous charge | k6, NBomber, JMeter |
| **Accessibility testing** | Vérifier la conformité d'accessibilité (a11y) | axe, Lighthouse, Playwright |
| **Visual regression testing** | Comparer des captures d'écran pixel par pixel | Playwright, Percy, Applitools |

</v-clicks>

<v-click>

<br>

> Ce sont des sujets spécialisés au-delà du périmètre de ce cours, mais il est important de **savoir qu'ils existent** pour choisir les bons outils selon le contexte projet.

</v-click>

<!--
Chacun de ces types de tests répond à un besoin spécifique.
Le chaos testing est utilisé par les grandes plateformes (Netflix, Amazon) pour tester la résilience.
Le performance testing est essentiel pour les applications à forte charge.
L'accessibility testing est de plus en plus exigé légalement (RGAA, WCAG).
Le visual regression testing est utile pour les applications front-end complexes.
-->
