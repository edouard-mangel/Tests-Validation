---
layout: two-cols-header
zoom: 0.9
---

# Le problème des données répétitives

::left::

<img src="/images/inline-data-probleme.png" class="h-64 mx-auto" />

::right::

<v-clicks depth="2">

Avec `[Fact]`, on doit écrire **un test par jeu de données** :

```csharp
[Fact]
public void Plus_2And3_Returns5()
    => Assert.Equal(5, Calculator.Plus(2, 3));

[Fact]
public void Plus_0And0_Returns0()
    => Assert.Equal(0, Calculator.Plus(0, 0));

[Fact]
public void Plus_Neg1And1_Returns0()
    => Assert.Equal(0, Calculator.Plus(-1, 1));
```

❌ Verbeux, difficile à maintenir, duplique la logique.

</v-clicks>

---
layout: two-cols-header
zoom: 0.9
---

# `[Theory]` + `[InlineData]`

::left::

<img src="/images/inline-data-solution.png" class="h-64 mx-auto" />

::right::

<v-clicks depth="2">

`[Theory]` remplace `[Fact]` pour les tests paramétriques :

```csharp
[Theory]
[InlineData(2, 3, 5)]
[InlineData(0, 0, 0)]
[InlineData(-1, 1, 0)]
[InlineData(-5, -3, -8)]
public void Plus_ReturnsExpectedSum(
    int a, int b, int expected)
{
    Assert.Equal(expected, Calculator.Plus(a, b));
}
```

✅ Une seule méthode, plusieurs jeux de données.

</v-clicks>

<!--
[Theory] = le comportement qu'on teste.
[InlineData(...)] = les données qui le paramétrisent.
xUnit génère un test distinct pour chaque [InlineData] → chacun peut passer ou échouer indépendamment.
-->

---
layout: two-cols-header
zoom: 0.9
---

# Partager des données entre tests : `ClassData`

::left::

**Étape 1 :** créer la classe de données

<img src="/images/class-data-impl.png" class="h-56 mx-auto my-2" />

::right::

**Étape 2 :** utiliser dans le test

<img src="/images/class-data-usage.png" class="h-40 mx-auto my-2" />

<v-click>

```csharp
[Theory]
[ClassData(typeof(HealthDamageTestData))]
public void Damage_ReducesHealth(
    int initial, int damage, int expected)
{
    Assert.Equal(expected,
        Calculator.ApplyDamage(initial, damage));
}
```

</v-click>

---
layout: two-cols-header
zoom: 0.9
---

# `ClassData` — Implémentation complète

::left::

```csharp
// La classe de données implémente IEnumerable<object[]>
public class HealthDamageTestData
    : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { 100, 20, 80 };
        yield return new object[] { 50, 50, 0 };
        yield return new object[] { 30, 100, 0 }; // floor à 0
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}
```

::right::

<v-click>

**Avantages vs `[InlineData]` :**

</v-click>

<v-clicks depth="2">

- Données **réutilisables** entre plusieurs méthodes de test
- Logique de génération des données **encapsulée**
- Peut charger les données depuis un **fichier ou une BDD**

</v-clicks>

<v-click>

<br>

```csharp
// Réutilisation dans un autre test
[Theory]
[ClassData(typeof(HealthDamageTestData))]
public void Damage_NeverGoesNegative(
    int initial, int damage, int expected)
{
    var result = Calculator.ApplyDamage(initial, damage);
    Assert.True(result >= 0);
}
```

</v-click>

<!--
ClassData est la solution quand les données sont complexes, générées dynamiquement, ou partagées entre tests.
MemberData permet de partager des données entre méthodes dans la même classe (via une propriété statique).
-->

---

# Résumé — Choisir son attribut

<br>

| Attribut | Quand l'utiliser |
|----------|-----------------|
| `[Fact]` | Test sans données variables |
| `[Theory]` + `[InlineData]` | Quelques jeux de données simples, inline |
| `[Theory]` + `[ClassData]` | Données partagées ou générées dynamiquement |
| `[Theory]` + `[MemberData]` | Données dans une propriété statique de la même classe |

<v-click>

<br>

> Commencer toujours par `[Fact]`. Passer à `[Theory]` uniquement quand on répète la même logique avec des données différentes.

</v-click>

<!--
La règle : ne pas sur-ingénier. Un [Fact] simple est souvent suffisant.
[Theory] apporte de la valeur quand on teste des cas limites (0, négatifs, nulls, max values...).
-->
