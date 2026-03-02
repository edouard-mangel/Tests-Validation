# Test-Driven Development (TDD)

Le test **guide** l'écriture du code. On ne code que ce que les tests demandent.

<v-clicks>

Le cycle en 3 étapes :

1. 🔴 **Rouge** — Écrire un test qui échoue (le comportement n'existe pas encore)
2. 🟢 **Vert** — Écrire le minimum de code pour faire passer le test
3. 🔵 **Refactor** — Améliorer le code sans casser les tests

</v-clicks>

<v-click>

> *"Make it work, make it right, make it fast."* — Kent Beck

</v-click>

<!--
TDD inverse la façon de penser : on commence par décrire le comportement attendu (le test), puis on fait en sorte que ça marche.
La règle d'or : on n'écrit pas une ligne de code de production sans un test rouge qui le justifie.
C'est ce qui distingue TDD de Test First : le design ÉMERGE des tests.
-->

---
layout: image
image: /images/tdd-cycle.png
backgroundSize: contain
---

<!--
Ce diagramme montre le cycle TDD : Red → Green → Refactor.
Rouge : le test échoue car la fonctionnalité n'existe pas.
Vert : on fait le minimum pour passer au vert.
Refactor : on améliore le code en gardant les tests verts.
Puis on recommence avec le prochain test.
-->

---

# TDD en pratique — Exemple

<br>

On veut une méthode `Add` pour `StringCalculator`. On commence par le test :

````md magic-move
```csharp
// 🔴 ROUGE — Le test échoue : StringCalculator n'existe pas encore
[Fact]
public void Add_EmptyString_ReturnsZero()
{
    var calc = new StringCalculator();

    var result = calc.Add("");

    Assert.Equal(0, result);
}
```

```csharp
// 🟢 VERT — Minimum de code pour passer au vert
public class StringCalculator
{
    public int Add(string numbers)
    {
        return 0; // Le minimum pour passer le premier test
    }
}
```

```csharp
// 🔴 ROUGE — On ajoute un test pour le cas "1 chiffre"
[Fact]
public void Add_SingleNumber_ReturnsThatNumber()
{
    var calc = new StringCalculator();

    var result = calc.Add("5");

    Assert.Equal(5, result);
}
```

```csharp
// 🟢 VERT — On étend l'implémentation
public class StringCalculator
{
    public int Add(string numbers)
    {
        if (numbers == "") return 0;
        return int.Parse(numbers);
    }
}
```

```csharp
// 🔴 ROUGE — On ajoute le cas "deux chiffres"
[Fact]
public void Add_TwoNumbers_ReturnsSum()
{
    var calc = new StringCalculator();

    var result = calc.Add("1,2");

    Assert.Equal(3, result);
}
```

```csharp
// 🟢 VERT — On gère la virgule
public class StringCalculator
{
    public int Add(string numbers)
    {
        if (numbers == "") return 0;
        var parts = numbers.Split(',');
        return parts.Sum(int.Parse);
    }
}
// 🔵 REFACTOR — Le code est déjà propre, rien à changer.
// On relance les 3 tests : ils passent tous. On continue.
```
````

<!--
Montrer le cycle en action : chaque test pousse l'implémentation d'un tout petit pas.
On ne code jamais "en avance" sur ce que les tests demandent.
C'est contre-intuitif au début, mais ça force à penser petit et à rester concentré.
-->

---
layout: two-cols-header
---

# TDD — Bilan

::left::

### ✅ Avantages

<v-clicks>

- Le code **émerge des contraintes métier**
- Chaque ligne de code est justifiée par un test
- Cycle de refactoring intégré → meilleure qualité
- Couverture de tests élevée (~100%) naturellement
- Feedback immédiat sur les régressions

</v-clicks>

::right::

### ❌ Inconvénients

<v-clicks>

- **Courbe d'apprentissage** importante
- Demande de la **discipline** (ne pas tricher avec le cycle)
- Peut sembler lent au début
- Difficile sur du code legacy ou des APIs inconnues

</v-clicks>

<!--
TDD n'est pas une technique de test, c'est une technique de conception.
Les tests sont un sous-produit du processus — l'objectif est d'avoir un design qui émerge des besoins.
La discipline est le vrai défi : il faut résister à l'envie d'écrire du code sans test rouge.
-->
