---
layout: two-cols-header
zoom: 0.9
---

# Grouper les tests avec `[Trait]`

::left::

<img src="/images/trait-decorator.png" class="h-56 mx-auto" />

::right::

<v-clicks>

`[Trait]` permet de **classifier** les tests par catégorie :

```csharp
public class CalculatorTests
{
    [Fact]
    [Trait("Category", "Unit")]
    public void Plus_ReturnsSum()
    {
        Assert.Equal(5, Calculator.Plus(2, 3));
    }

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Feature", "Addition")]
    public void Plus_WithNegatives_ReturnsSum()
    {
        Assert.Equal(-1, Calculator.Plus(2, -3));
    }
}
```

</v-clicks>

<!--
Trait est un dictionnaire clé/valeur libre. On peut créer ses propres catégories.
Utile pour n'exécuter qu'une partie des tests en CI (ex: seulement les tests rapides).
-->

---
layout: two-cols-header
zoom: 0.85
---

# Filtrer par Trait dans le Test Explorer

::left::

<img src="/images/trait-grouping-1.png" class="h-56 mx-auto" />

::right::

<img src="/images/trait-grouping-2.png" class="h-56 mx-auto" />

<v-click>

Une fois les Traits définis, le Test Explorer permet de **filtrer et grouper** les tests par catégorie.

</v-click>

<!--
Le filtrage par Trait est aussi disponible en ligne de commande :
dotnet test --filter "Category=Unit"
-->

---
layout: two-cols-header
zoom: 0.9
---

# Appliquer `[Trait]` à toute une classe

::left::

<img src="/images/trait-class.png" class="h-48 mx-auto" />

::right::

<v-clicks>

Plutôt que de décorer chaque test, on peut appliquer le Trait **au niveau de la classe** :

```csharp
[Trait("Category", "Unit")]
public class CalculatorTests
{
    [Fact]
    public void Plus_ReturnsSum()
    {
        Assert.Equal(5, Calculator.Plus(2, 3));
    }

    [Fact]
    public void Minus_ReturnsDifference()
    {
        Assert.Equal(1, Calculator.Minus(3, 2));
    }
}
```

*Tous les tests de la classe héritent du Trait.*

</v-clicks>

---
layout: two-cols-header
zoom: 0.9
---

# Ignorer des tests avec `[Fact(Skip = ...)]`

::left::

<img src="/images/skip-test.png" class="h-56 mx-auto" />

::right::

<v-clicks>

Pour ignorer un test sans le supprimer :

```csharp
[Fact(Skip = "En attente de la story US-42")]
public void Payment_ProcessesRefund()
{
    // ...
}
```

<br>

**Quand ignorer un test ?**

- Fonctionnalité pas encore implémentée
- Dépendance externe temporairement indisponible

<br>

> ⚠️ **Jamais** pour masquer un test qui échoue sans savoir pourquoi.

</v-clicks>

<!--
Un test ignoré doit toujours avoir une raison explicite dans le message Skip.
Un test qui échoue "pour une raison inconnue" doit être corrigé, pas ignoré.
-->
