# Qu'est-ce qu'une assertion ?

<br>

<v-click>

> En programmation, une **assertion** est une expression qui doit être évaluée à **vrai**.
>
> Si elle échoue, le test est marqué en erreur avec un message descriptif.

</v-click>

<v-clicks depth="2">

<br>

xUnit propose une classe statique `Assert` avec des méthodes pour chaque type de vérification.

```bash
dotnet add package xunit
```

</v-clicks>

<!--
La différence avec un simple if : Assert génère un message d'erreur précis ("Expected 5, but was 4").
-->

---
layout: two-cols-header
zoom: 0.9
---

# Assert — Booléens et valeurs numériques

::left::

### Booléens

```csharp
Assert.True(condition);
Assert.False(condition);
```

### Valeurs numériques

```csharp
// Égalité
Assert.Equal(expected, actual);
Assert.NotEqual(expected, actual);

// Dans des bornes
Assert.InRange(value, low, high);

// Précision des floats
Assert.Equal(3.14, result, precision: 2);
```

::right::

<v-clicks depth="2">

### Exemples

```csharp
Assert.True(user.IsActive);
Assert.False(order.IsCancelled);

Assert.Equal(42, Calculator.Plus(20, 22));
Assert.NotEqual(0, cart.TotalPrice);

Assert.InRange(score, 0, 100);

// 3.14159... ≈ 3.14 (2 décimales)
Assert.Equal(3.14, Math.PI, precision: 2);
```

</v-clicks>

<!--
Equal(expected, actual) : l'ordre des paramètres est important pour le message d'erreur.
Par convention : expected en premier, actual en second.
-->

---
layout: two-cols-header
zoom: 0.9
---

# Assert — Strings

::left::

```csharp
// Égalité
Assert.Equal("hello", str);

// Vide / non-vide
Assert.Empty(str);
Assert.NotEmpty(str);

// Contenu
Assert.StartsWith("He", greeting);
Assert.EndsWith("World", greeting);
Assert.Contains("ello", str);

// Expression régulière
Assert.Matches(@"^\d{5}$", zipCode);
```

::right::

<v-clicks depth="2">

<img src="/images/setup-projet-tests-1.png" class="hidden" />

```csharp
var name = "Hello World";

Assert.Equal("Hello World", name);
Assert.NotEmpty(name);
Assert.StartsWith("Hello", name);
Assert.EndsWith("World", name);
Assert.Contains("llo", name);

// Code postal français
Assert.Matches(@"^\d{5}$", "75001");
```

</v-clicks>

<!--
Les assertions de string sont plus expressives que Assert.True(str.Contains("x")) — le message d'erreur est plus clair.
-->

---
layout: two-cols-header
zoom: 0.9
---

# Assert — Collections et types

::left::

### Collections

```csharp
Assert.Empty(list);
Assert.NotEmpty(list);
Assert.Contains(item, list);
Assert.DoesNotContain(item, list);
Assert.Equal(expected, actual); // même ordre

// Tous les éléments valident une condition
Assert.All(list, item => Assert.True(item > 0));
```

### Types d'objets

```csharp
Assert.IsType<UserDto>(result);
Assert.IsAssignableFrom<IService>(obj);
Assert.NotNull(obj);
Assert.Null(obj);
```

::right::

<v-clicks depth="2">

```csharp
var users = new List<string> { "Alice", "Bob" };

Assert.NotEmpty(users);
Assert.Contains("Alice", users);
Assert.DoesNotContain("Charlie", users);

// Tous les noms ont plus de 2 caractères
Assert.All(users, u => Assert.True(u.Length > 2));

// Vérifier le type du résultat
var service = factory.Create();
Assert.IsType<UserService>(service);
Assert.NotNull(service);
```

</v-clicks>

<!--
Assert.All est très pratique pour vérifier une propriété sur tous les éléments d'une liste.
-->

---
layout: two-cols-header
zoom: 0.9
---

# Assert — Exceptions

::left::

<v-click>

C'est une **bonne pratique** de tester qu'un code lève bien (ou ne lève pas) une exception.

</v-click>

<v-clicks depth="2">

```csharp
// Vérifier qu'une exception est levée
Assert.Throws<ArgumentException>(
    () => Calculator.Divide(10, 0)
);

// Vérifier le message de l'exception
var ex = Assert.Throws<ArgumentException>(
    () => service.Register(null)
);
Assert.Contains("null", ex.Message);

// Vérifier qu'aucune exception n'est levée
var exception = Record.Exception(
    () => service.ProcessOrder(validOrder)
);
Assert.Null(exception);
```

</v-clicks>

::right::

<v-clicks depth="2">

**Pourquoi tester les exceptions ?**

- Documenter le contrat d'une API
- Garantir la robustesse aux entrées invalides
- Éviter les régressions silencieuses

<br>

> Si votre code doit lever une exception dans un cas précis, **c'est un comportement à tester**.

</v-clicks>

<!--
Trop souvent on ne teste que le "happy path". Les cas d'erreur sont tout aussi importants.
Assert.Throws retourne l'exception, ce qui permet de vérifier son message, son type interne, etc.
-->
