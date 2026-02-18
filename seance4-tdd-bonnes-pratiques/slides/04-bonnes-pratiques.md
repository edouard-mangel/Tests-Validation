# Bonnes pratiques — Ce qu'il faut toujours tester

<v-clicks>

**Les cas limites** sont là où les bugs se cachent le plus souvent :

- Chaîne vide, `null`, liste vide
- Valeurs maximales et minimales
- Valeurs **juste aux limites** : `0`, `-1`, `+1`, `int.MaxValue`

</v-clicks>

<v-click>

**Les exceptions et erreurs** sont aussi du comportement à tester :

```csharp
[Fact]
public void Add_NegativeNumber_ThrowsException()
{
    var calc = new StringCalculator();

    var act = () => calc.Add("-1");

    Assert.Throws<ArgumentException>(act);
}
```

</v-click>

<!--
Les développeurs ont tendance à tester le "happy path" et à oublier les cas aux limites.
Or c'est exactement là que se trouvent les bugs les plus courants en production.
Pour les valeurs numériques : tester n-1, n, n+1 autour de chaque limite.
-->

---
layout: two-cols-header
---

# Testing positif vs négatif

::left::

### ✅ Testing positif

Vérifie que le code fait **ce qu'il doit faire** quand tout va bien.

<v-clicks>

```csharp
// Cas passant : email valide
[Theory]
[InlineData("alice@example.com")]
[InlineData("bob+tag@mail.co.uk")]
public void IsValid_ValidEmail_ReturnsTrue(string email)
{
    var validator = new EmailValidator();

    Assert.True(validator.IsValid(email));
}
```

</v-clicks>

::right::

### ❌ Testing négatif

Vérifie que le code **rejette correctement** les entrées invalides.

<v-clicks>

```csharp
// Cas non-passant : email invalide
[Theory]
[InlineData("")]
[InlineData("pas-un-email")]
[InlineData("@sans-local.com")]
public void IsValid_InvalidEmail_ReturnsFalse(string email)
{
    var validator = new EmailValidator();

    Assert.False(validator.IsValid(email));
}
```

</v-clicks>

<!--
Les deux types sont complémentaires et indispensables.
Testing positif : s'assurer que les cas normaux fonctionnent.
Testing négatif : s'assurer que le code résiste aux mauvaises entrées.
En pratique : pour chaque règle de validation, écrire au moins 1 test positif et 1 test négatif.
-->

---

# Un test n'a de valeur que s'il est exécuté

<br>

<v-clicks>

Un test qui ne tourne jamais est **pire qu'une absence de test** : il donne une fausse impression de sécurité.

**Règle :** les tests doivent tourner à chaque commit, en CI/CD.

</v-clicks>

<v-click>

```yaml
# .github/workflows/tests.yml — exemple minimal
jobs:
  test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - name: Run tests
        run: dotnet test --no-restore
```

</v-click>

<v-click>

> Si les tests ne sont pas automatisés, ils ne sont pas exécutés.
> Si ils ne sont pas exécutés, ils ne servent à rien.

</v-click>

<!--
C'est le point souvent oublié : on peut avoir 1000 tests parfaits, si personne ne les lance, ils n'apportent aucune valeur.
La CI/CD est la seule garantie que les tests tournent vraiment à chaque changement.
Un test qui échoue en CI bloque le merge → c'est exactement l'effet recherché.
-->

---
layout: center
class: text-center
---

# L'automatisation des tests au service de la qualité logicielle

<br>

Tests unitaires · Tests d'intégration · Tests e2e

TDD · FIRST · Testing positif/négatif

<br>

## Passons au TP

*Pratiquer le TDD sur un kata*

<!--
Conclusion de la séance.
On a vu les trois approches (After/First/TDD), les principes FIRST, et les bonnes pratiques.
Le TP va permettre de pratiquer le cycle TDD sur un exercice concret.
-->
