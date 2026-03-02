# TDD en pratique — StringCalculator

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

# Récapitulatif — Ce qu'on a fait

<br>

| Étape | Test | Implémentation |
|-------|------|----------------|
| 1 | `Add("")` → `0` | `return 0;` |
| 2 | `Add("5")` → `5` | `if ("") return 0; return int.Parse(...)` |
| 3 | `Add("1,2")` → `3` | `Split(',')` + `Sum(int.Parse)` |

<v-clicks>

À chaque étape :
- **Un seul test** ajouté
- **Le minimum** de code pour passer
- **Tous les tests** repassent au vert avant de continuer

C'est ça, le TDD.

</v-clicks>

<!--
Le pattern est clair : petit pas, petit pas, petit pas.
On ne saute jamais d'étapes. C'est la discipline qui fait la différence.
-->

---

# La discipline du commit

<br>

<v-clicks>

En TDD, on **committe à chaque test vert** :

```bash
git add -A && git commit -m "Add: empty string returns 0"
git add -A && git commit -m "Add: single number returns itself"
git add -A && git commit -m "Add: two numbers returns sum"
```

Pourquoi ?

- L'historique git **raconte l'histoire** du développement
- Chaque commit est un **point de retour** sûr
- Si un refactoring casse tout, on revient d'**un seul commit**
- L'historique prouve la discipline TDD (important pour le projet !)

</v-clicks>

<!--
C'est un réflexe à prendre dès maintenant. Le TP est l'occasion de pratiquer.
Pour le projet de groupe (séances 6-8), l'historique de commits sera évalué.
-->

---
layout: center
class: text-center
---

# TP — Pratiquer le TDD

<br>

**Kata 1 — FizzBuzz** (guidé, ensemble)

**Kata 2 — String Calculator** (autonome)

<br>

*Committer à chaque test vert.*

<!--
Le TP se déroule en deux phases :
1. FizzBuzz en live-coding avec toute la classe — on pratique ensemble le cycle TDD
2. String Calculator en autonomie — les étudiants appliquent seuls, en ajoutant les features une par une selon la spec
-->
