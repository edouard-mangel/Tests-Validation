# Les outils : xUnit pour .NET

<br>

<v-clicks>

- Framework de test **open-source** pour .NET (utilisé par l'équipe .NET elle-même)
- S'intègre avec Visual Studio, Rider, `dotnet test`

</v-clicks>

<v-click>

```bash
# Créer un projet de test
dotnet new xunit -n MonProjet.Tests

# Référencer le projet à tester
dotnet add reference ../MonProjet/MonProjet.csproj

# Lancer les tests
dotnet test
```

</v-click>

<!--
xUnit est le framework standard en .NET. Il remplace MSTest et NUnit dans la plupart des projets modernes.
La commande `dotnet test` est ce que la CI/CD exécutera aussi.
-->

---

# `[Fact]` — Un test simple

<br>

Un `[Fact]` est un test qui s'exécute **sans paramètre**. Un fait est toujours vrai.

```csharp
using Xunit;

public class CalculatorTests
{
    [Fact]
    public void Add_TwoPositiveNumbers_ReturnsSum()
    {
        // Arrange — Préparer le contexte
        var calc = new Calculator();

        // Act — Exécuter l'action
        var result = calc.Add(2, 3);

        // Assert — Vérifier le résultat
        Assert.Equal(5, result);
    }
}
```

<v-clicks>

- L'attribut `[Fact]` signale au framework que c'est un test
- Pas besoin de `[TestClass]` ni d'héritage — xUnit est minimaliste
- xUnit crée une **nouvelle instance** de la classe pour chaque test → isolation naturelle

</v-clicks>

<!--
Comparer avec MSTest : pas besoin de [TestClass], pas besoin d'héritage.
xUnit crée une nouvelle instance de la classe pour chaque test → isolation naturelle.
-->

---

# Arrange / Act / Assert (AAA)

<br>

Tout test unitaire suit le même patron en **3 phases** :

<v-clicks>

| Phase | Rôle | Analogie |
|-------|------|----------|
| **Arrange** | Préparer le contexte (données, objets) | Mettre en place la scène |
| **Act** | Exécuter l'action à tester | Appuyer sur le bouton |
| **Assert** | Vérifier le résultat | Vérifier que ça a marché |

</v-clicks>

<v-click>

- **Un seul Act** par test (une seule action testée)
- **Un seul concept vérifié** par test (une ou quelques assertions liées)

</v-click>

<!--
AAA est le patron universel. Chaque test devrait avoir exactement ces 3 phases.
Certains utilisent aussi Given/When/Then (BDD) — c'est la même idée avec un vocabulaire différent.
Si un test a besoin de 3 Act différents, c'est probablement 3 tests distincts.
-->

---

# `[Theory]` — Tests paramétrés

<br>

Un `[Theory]` est un test qui s'exécute **avec des données**. Une théorie est vraie sous certaines conditions.

```csharp
[Theory]
[InlineData(2, 3, 5)]
[InlineData(0, 0, 0)]
[InlineData(-1, 1, 0)]
[InlineData(100, -50, 50)]
public void Add_VariousInputs_ReturnsExpectedSum(int a, int b, int expected)
{
    var calc = new Calculator();

    var result = calc.Add(a, b);

    Assert.Equal(expected, result);
}
```

<v-clicks>

- `[Theory]` + `[InlineData]` = un test exécuté **plusieurs fois** avec des données différentes
- Chaque `[InlineData]` génère un **test indépendant** dans le rapport
- Idéal pour tester des **cas limites** et des **variations**

</v-clicks>

<!--
C'est beaucoup plus concis que d'écrire 4 tests séparés.
Si un cas échoue, on sait exactement lequel.
-->

---

# Les assertions courantes

<br>

<v-clicks>

| Assertion | Usage |
|-----------|-------|
| `Assert.Equal(expected, actual)` | Vérifie l'égalité |
| `Assert.True(condition)` / `False` | Vérifie une condition |
| `Assert.Null(object)` / `NotNull` | Vérifie null |
| `Assert.Contains(item, collection)` | Présence dans une collection |
| `Assert.Empty(collection)` | Collection vide |
| `Assert.Throws<T>(action)` | Exception levée |

</v-clicks>

<v-click>

```csharp
[Fact]
public void Divide_ByZero_ThrowsDivideByZeroException()
{
    var calc = new Calculator();

    var act = () => calc.Divide(10, 0);

    Assert.Throws<DivideByZeroException>(act);
}
```

</v-click>

<!--
Les plus utilisées : Equal, True/False, Throws.
Assert.Throws capture l'exception et renvoie l'objet exception pour inspection.
-->

---

# FluentAssertions — Des assertions lisibles

<br>

<v-clicks>

Le NuGet **FluentAssertions** propose une syntaxe plus naturelle pour les assertions :

```bash
dotnet add package FluentAssertions
```

| xUnit classique | FluentAssertions |
|----------------|-----------------|
| `Assert.Equal(5, result)` | `result.Should().Be(5)` |
| `Assert.True(isValid)` | `isValid.Should().BeTrue()` |
| `Assert.Contains("alice", list)` | `list.Should().Contain("alice")` |
| `Assert.Throws<ArgumentException>(act)` | `act.Should().Throw<ArgumentException>()` |
| `Assert.Equal(expected, obj)` | `obj.Should().BeEquivalentTo(expected)` |

</v-clicks>

<v-click>

> Les messages d'erreur de FluentAssertions sont souvent **plus explicites** que ceux de xUnit.

</v-click>

<!--
FluentAssertions est très utilisé en entreprise. La syntaxe .Should() se lit comme une phrase en anglais.
Les deux styles (Assert classique et FluentAssertions) sont valides dans ce cours.
FluentAssertions est optionnel mais recommandé pour la lisibilité.
-->

---

# FluentAssertions — Exemple complet

<br>

````md magic-move
```csharp
// Avec Assert classique (xUnit)
[Fact]
public void GetUser_ExistingId_ReturnsCorrectUser()
{
    var service = new UserService();

    var user = service.GetById(1);

    Assert.NotNull(user);
    Assert.Equal("Alice", user.Name);
    Assert.Equal("alice@test.com", user.Email);
    Assert.True(user.IsActive);
}
```

```csharp
// Avec FluentAssertions — plus lisible
using FluentAssertions;

[Fact]
public void GetUser_ExistingId_ReturnsCorrectUser()
{
    var service = new UserService();

    var user = service.GetById(1);

    user.Should().NotBeNull();
    user.Name.Should().Be("Alice");
    user.Email.Should().Be("alice@test.com");
    user.IsActive.Should().BeTrue();
}
```
````

<v-click>

<br>

Les deux approches sont **équivalentes**. FluentAssertions apporte de la lisibilité sur les gros projets.

</v-click>

<!--
FluentAssertions brille surtout quand les messages d'erreur comptent : "Expected user.Name to be 'Alice', but found 'Bob'" est plus clair que "Assert.Equal() Failure".
Dans ce cours, les deux styles sont acceptés. En entreprise, le choix est souvent une convention d'équipe.
-->

---

# Nommer ses tests

<br>

Le nom d'un test doit répondre à **3 questions** :

<v-clicks>

1. **Quoi ?** — Quelle méthode ou quel scénario
2. **Quand ?** — Dans quelles conditions
3. **Alors ?** — Quel résultat attendu

</v-clicks>

<v-click>

Convention courante : `Methode_Condition_ResultatAttendu`

```csharp
Add_EmptyString_ReturnsZero()
Calculate_PremiumUser_Returns20Percent()
Register_MinorUser_ThrowsException()
IsValid_NullEmail_ReturnsFalse()
```

</v-click>

<v-click>

> Le nom du test **raconte une histoire**. On doit comprendre ce qu'il teste sans lire le code.

</v-click>

<!--
L'important : le nom du test est de la documentation vivante.
Si le nom n'est pas clair, le test est difficile à maintenir.
-->

---

# Given / When / Then — Vocabulaire alternatif

<br>

<v-clicks>

Le patron **AAA** a un équivalent issu du BDD (*Behavior-Driven Development*) :

| AAA | GWT | Rôle |
|-----|-----|------|
| **Arrange** | **Given** | Le contexte initial |
| **Act** | **When** | L'action déclenchée |
| **Assert** | **Then** | Le résultat vérifié |

</v-clicks>

<v-click>

Même test, vocabulaire GWT dans le nom :

```csharp
[Fact]
public void GivenPremiumUser_WhenCalculatePrice_ThenReturns20PercentDiscount()
{
    // Given
    var service = new PricingService();
    var user = new User { IsPremium = true };

    // When
    var price = service.Calculate(user, basePrice: 100m);

    // Then
    Assert.Equal(80m, price);
}
```

</v-click>

<v-click>

> AAA et GWT sont **la même idée** avec un vocabulaire différent. Les deux sont valides.

</v-click>

<!--
Le vocabulaire Given/When/Then vient du BDD (Behavior-Driven Development).
Des outils comme SpecFlow (en .NET) ou Cucumber utilisent cette syntaxe.
Dans ce cours, on utilise AAA, mais les étudiants rencontreront GWT en entreprise.
L'important : structurer ses tests en 3 phases, quel que soit le vocabulaire.
-->

---

# Les cas limites et les principes FIRST

<br>

<v-clicks>

**Les cas limites** sont là où les bugs se cachent : `null`, `""`, `0`, `-1`, `int.MaxValue`...

```csharp
[Theory]
[InlineData(null)]
[InlineData("")]
[InlineData("   ")]
public void IsValid_EmptyOrNullInput_ReturnsFalse(string? input)
{
    Assert.False(new InputValidator().IsValid(input));
}
```

</v-clicks>

<v-click>

Un bon test respecte les principes **FIRST** :

| | Principe | Signification |
|---|---------|---------------|
| **F** | Fast | Millisecondes, pas secondes |
| **I** | Isolated | Ne dépend d'aucun autre test |
| **R** | Repeatable | Même résultat à chaque exécution |
| **S** | Self-validating | Pass/Fail sans interprétation |
| **T** | Timely | Écrit au bon moment (idéalement avant le code) |

</v-click>

<!--
FIRST est un bon point de départ — on reviendra sur un modèle plus complet en séance 5 : les Test Desiderata de Kent Beck (12 propriétés).
FIRST et les cas limites sont des réflexes à acquérir. On va les retrouver tout au long du module.
-->
