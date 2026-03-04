---
layout: image
image: /images/tests-unitaires.png
---

<!--
Schéma d'architecture : les flèches vertes restent INTERNES à chaque composant.
On ne sort pas du composant. Pas de BDD, pas de réseau, pas d'API externe.
C'est pour ça que c'est rapide et stable.
-->

---

# Tests unitaires

Les plus **rapides**, les plus **stables**, les moins **chers**.

C'est la base de la pyramide.

<br>

<v-click>

Mais que signifie "unitaire" ?

<br>

Il existe **deux écoles** de pensée...

</v-click>

<!--
Transition vers le sujet clé de la séance.
Poser la question : "Pour vous, un test unitaire, ça teste quoi ?"
Laisser les étudiants répondre avant de montrer les deux écoles.
-->

---
layout: two-cols-header
zoom: 0.9
---

# 1ère école : tester par classe / fonction

::left::

<v-clicks>

</v-clicks>

<br>

```csharp
[TestClass]
public class UserValidatorTests
{
    [TestMethod]
    public void CanVote_Returns_True_When_Over18()
    {
        var validator = new UserValidator();
        var user = new User { Id = 1, Age = 18 };
        Assert.IsTrue(validator.CanVote(user));
    }

    [TestMethod]
    public void CanVote_Returns_False_When_Under18()
    {
        var validator = new UserValidator();
        var user = new User { Id = 1, Age = 17 };
        Assert.IsFalse(validator.CanVote(user));
    }
}
```

::right::

<v-clicks>

- Un test par méthode publique (minimum)
- Revient à tester l'**implémentation**
- Force le couplage du test au code

- Si vous refactorez → **tous les tests cassent**
- Couverture de code non représentative
- Tests **rigides et fragiles**

</v-clicks>

<!--
Première école : "unitaire" = une unité de code (classe, méthode).
On teste CanVote directement. Ça marche... jusqu'au refactoring.

Le problème : si demain on fusionne UserValidator dans RegistrationService, tous ces tests sont à réécrire.
On teste le "comment" au lieu du "quoi".
-->

---
layout: two-cols-header
zoom: 0.8
---

# 2ème école : tester par scénario métier

::left::

````md magic-move
```csharp
[TestClass]
public class UserValidatorTests
{
    [TestMethod]
    public void CanVote_Returns_True_When_Over18()
    {
        var validator = new UserValidator();
        var user = new User { Id = 1, Age = 18 };
        Assert.IsTrue(validator.CanVote(user));
    }

    [TestMethod]
    public void CanVote_Returns_False_When_Under18()
    {
        var validator = new UserValidator();
        var user = new User { Id = 1, Age = 17 };
        Assert.IsFalse(validator.CanVote(user));
    }
}
```
```csharp
[TestClass]
public class UserRegistrationTests
{
    [TestMethod]
    public void AdultUser_CanRegisterForVoting()
    {
        // Arrange
        var service = new RegistrationService();
        var user = new User { Id = 1, Age = 18 };

        // Act
        service.RegisterForElection(user);

        // Assert - Vérifie le changement dans le système
        var registered = service.GetRegisteredVoters();
        Assert.Contains(user, registered);
    }

    [TestMethod]
    public void MinorUser_CannotRegisterForVoting()
    {
        var service = new RegistrationService();
        var user = new User { Id = 2, Age = 17 };

        service.RegisterForElection(user);

        Assert.Empty(service.GetRegisteredVoters());
    }
}
```
````

::right::

<v-clicks>

- Test du **cas d'usage métier** complet
- Vérifie le **changement d'état** du système
- **Résilient** aux refactorings internes
- Le nom du test documente le comportement attendu

</v-clicks>

<!--
Deuxième école : "unitaire" = une unité de comportement (un scénario métier).
On ne teste plus CanVote directement. On teste "un adulte peut s'inscrire pour voter".

Si demain on refactore l'intérieur de RegistrationService, les tests continuent de passer.
On teste le "quoi" (le comportement) au lieu du "comment" (l'implémentation).

PRÉCISER : c'est une généralité. Parfois c'est OK d'avoir des tests unitaires par méthode pour les cas algorithmiques complexes.
-->

---
layout: two-cols-header
zoom: 0.8
---

# La différence en pratique

::left::

### Couplé à l'implémentation 💩

```csharp
[TestMethod]
public void CanVote_Returns_True_When_Over18()
{
    var validator = new UserValidator();
    var user = new User { Id = 1, Age = 18 };
    Assert.IsTrue(validator.CanVote(user));
}
```

::right::

### Couplé au comportement ✅

```csharp
[TestMethod]
public void AdultUser_CanRegisterForVoting()
{
    // Arrange
    var service = new RegistrationService();
    var user = new User { Id = 1, Age = 18 };

    // Act
    service.RegisterForElection(user);

    // Assert
    var registered =
        service.GetRegisteredVoters();
    Assert.Contains(user, registered);
}
```

<br>

<v-click>

## Le nom des tests participe à la **documentation vivante** du projet.

On peut aussi parler de **spécifications exécutables**.

</v-click>

<!--
Mettre les deux côte à côte pour bien voir la différence.
A gauche : on teste une méthode. A droite : on teste un scénario.
Le nom du test à droite raconte une histoire : "Un adulte peut s'inscrire pour voter".
C'est lisible par un non-développeur, ça documente le besoin métier.
-->

---

# Tests unitaires — Résumé

<br>

### ✅ Avantages

<v-clicks>

- **Rapides** : des milliers de tests en quelques secondes
- Accélèrent le développement
- Réduisent les régressions et améliorent la qualité
- Permettent l'évolutivité et la maintenabilité
- Participent à la documentation du projet

</v-clicks>

<br>

<v-click>

### ⚠️ Inconvénient

- Phase d'apprentissage, plus ou moins longue selon les développeurs

</v-click>

<br>

<v-click>

> *C'est justement pour ça qu'on est là aujourd'hui.* 😉

</v-click>

<!--
Rassurer les étudiants : c'est normal que ce soit difficile au début.
L'objectif du TP qui suit est justement de commencer à pratiquer.
La difficulté n'est pas dans la syntaxe (xUnit est simple), mais dans le choix de QUOI tester.
-->

---
layout: image
image: /images/tests-unitaires-avantages.png
---

<!--
Image humoristique : "Un coup de main ?" / "Non merci, pas le temps" / "Nous sommes trop occupés"
C'est exactement l'argument classique contre les tests : "on n'a pas le temps".
Mais sans tests, on passe encore plus de temps à corriger des bugs...
-->

---

# En résumé

<br>

| | E2E | Intégration | Unitaire |
|---|-----|-------------|----------|
| **Vitesse** | 🐌 Lent | 🚶 Moyen | 🚀 Rapide |
| **Coût** | 💰💰💰 | 💰💰 | 💰 |
| **Fragilité** | Fragile | Moyen | Stable |
| **Valeur métier** | Haute | Faible | Haute |
| **Quantité idéale** | Peu | Quelques | Beaucoup |

<br>

<v-click>

La stratégie : **beaucoup de tests unitaires**, quelques tests d'intégration, peu de tests e2e.

C'est la **pyramide des tests**.

</v-click>

<!--
Slide de synthèse avant le TP.
Rappeler la pyramide : la base large = les tests unitaires.
-->
---
layout: two-cols-header
class: text-center
---

# La pyramide des tests, ou l'outside in diamond


::left::

## La Pyramide des tests, de Mike Cohn

<img src="/images/pyramide-agile.png" class="h-60 mx-auto" />

::right::

## L'Outside-In Diamond, de Thomas Pierrain

<img src="/images/diamond.png" class="h-80 mx-auto" />

<!--
Rappel visuel : beaucoup de tests unitaires à la base, quelques tests d'intégration, peu de tests e2e.
-->
