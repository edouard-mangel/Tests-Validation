# Quand écrire les tests ?

Trois approches, trois philosophies différentes.

<v-clicks>

| Approche | Principe | Usage typique |
|---|---|---|
| **Test After** | On code d'abord, on teste ensuite | La réalité de beaucoup d'équipes |
| **Test First** | On écrit le test avant le code | Architecture déjà connue |
| **TDD** | Le test *guide* l'écriture du code | Développement dirigé par les tests |

</v-clicks>

<!--
Ces trois approches ne sont pas équivalentes en valeur.
Test After est la plus courante mais la moins efficace.
TDD est la plus disciplinée et la plus puissante.
On va les passer en revue une par une.
-->

---
layout: two-cols-header
---

# Test After

On écrit le code d'abord. On ajoute les tests ensuite.

::left::

### ✅ Avantages

<v-clicks>

- Naturel, intuitif
- Pas besoin de penser aux tests pendant le développement

</v-clicks>

::right::

### ❌ Inconvénients

<v-clicks>

- **Biais de confirmation** : on teste ce qu'on sait qui marche
- Capture les bugs comme comportement attendu
- Seule valeur réelle : la **non-régression**
- Le code n'a pas été pensé pour être testable

</v-clicks>

<!--
Le biais de confirmation est le vrai problème : quand on a écrit le code, on écrit les tests pour qu'ils passent.
On ne cherche pas à trouver des bugs, on cherche à valider ce qu'on a fait.
Résultat : les tests n'ont de valeur que comme filet de sécurité contre les régressions futures.
-->

---
layout: two-cols-header
---

# Test First

On écrit le test **avant** d'écrire le code.

::left::

### ✅ Avantages

<v-clicks>

- Force à réfléchir au comportement attendu
- Le code est **conçu pour être testable**
- Moins de biais de confirmation

</v-clicks>

::right::

### ❌ Inconvénients

<v-clicks>

- Nécessite une architecture **déjà connue**
- Difficile si on découvre le domaine en codant
- Cas typique : développement sous-traité avec specs complètes

</v-clicks>

<!--
Test First est une bonne approche quand les specs sont figées et l'architecture connue à l'avance.
C'est souvent le cas dans les projets outsourcés où le client fournit les cas de test.
La limite : si on découvre le design en codant (comme en TDD), l'approche ne fonctionne pas bien.
-->
