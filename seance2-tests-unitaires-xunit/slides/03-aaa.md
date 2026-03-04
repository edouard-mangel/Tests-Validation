# Le pattern Arrange / Act / Assert

<br>

<v-clicks>

Tout test unitaire suit la même structure en **3 phases** :

| Phase | Rôle |
|-------|------|
| **Arrange** | Créer le contexte, les objets, les données d'entrée |
| **Act** | Exécuter le code à tester |
| **Assert** | Vérifier que le résultat est bien celui attendu |

</v-clicks>

<!--
Le pattern AAA est universel : il s'applique à xUnit, NUnit, MSTest, Jest, JUnit...
C'est la structure minimale d'un test lisible et maintenable.
-->

---
layout: two-cols-header
zoom: 0.9
---

# Ecriture de notre premier test

::left::

<img src="/images/premier-test-code.png" class="h-72 mx-auto" />

::right::

<v-clicks>

```csharp
public class CalculatorTests
{
    [Fact]
    public void Plus_ShouldReturnSum_WhenGivenTwoIntegers()
    {
        // Arrange
        int a = 2;
        int b = 3;

        // Act
        int result = Calculator.Plus(a, b);

        // Assert
        Assert.Equal(5, result);
    }
}
```

</v-clicks>

<!--
Faire remarquer le nom du test : il décrit le comportement attendu.
La séparation AAA avec des commentaires est optionnelle mais aide à la lisibilité au début.
-->

---
layout: two-cols-header
zoom: 0.9
---

# Lancer les tests

::left::

<img src="/images/premier-test-run.png" class="h-72 mx-auto" />

::right::

<v-clicks>

- **Test Explorer** → *Exécuter tout* (Ctrl+R, A)
- Un test vert ✅ = le comportement est correct
- Un test rouge ❌ = le comportement est incorrect

<br>

**Question :** quelle assertion parmi plusieurs est inutile ?

```csharp
Assert.Equal(5, result);     // ✅ utile
Assert.True(result == 5);    // ⚠️ redondant
Assert.IsType<int>(result);  // ❌ inutile
```

</v-clicks>

<!--
Faire réfléchir les étudiants : quand une assertion est-elle redondante ?
Assert.IsType<int> est inutile si on teste déjà la valeur avec Equal — le type est implicitement vérifié.
Assert.True(x == y) est moins expressif que Assert.Equal(y, x) car le message d'erreur est moins précis.
-->

---

# Combien d'assertions par test ?

<br>

<v-clicks>

La règle stricte : **un seul `Assert` par test**.

La règle pragmatique : **plusieurs assertions si elles testent le même comportement**.

<br>

```csharp
// ✅ OK : les deux assertions vérifient le même résultat
[Fact]
public void CreateUser_SetsNameAndEmail()
{
    var user = new User("Alice", "alice@test.com");

    Assert.Equal("Alice", user.Name);
    Assert.Equal("alice@test.com", user.Email);
}
```

<br>

> La question à se poser : *si l'un des asserts échoue, est-ce le même bug ?*

</v-clicks>

<!--
La règle "un assert" vient de la difficulté de nommer clairement ce qu'on teste avec plusieurs assertions.
En pratique, on peut avoir plusieurs assertions tant qu'elles testent le même comportement atomique.
-->
