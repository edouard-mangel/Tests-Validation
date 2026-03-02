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
TDD inverse la façon de penser : on commence par décrire le comportement attendu, puis on fait en sorte que ça marche.
La règle d'or : on n'écrit pas une ligne de code de production sans un test rouge qui le justifie.
C'est ce qui distingue TDD de Test First : le design ÉMERGE des tests.
-->

---

# Le cycle Rouge / Vert / Refactor

<br>

```
    ┌──────────┐
    │  🔴 RED   │ ◄── Écrire un test qui échoue
    └────┬─────┘
         │
         ▼
    ┌──────────┐
    │ 🟢 GREEN  │ ◄── Minimum de code pour passer
    └────┬─────┘
         │
         ▼
    ┌──────────────┐
    │ 🔵 REFACTOR   │ ◄── Améliorer sans casser
    └────┬─────────┘
         │
         └──────────► Recommencer avec le prochain test
```

<v-click>

<br>

Le cycle est **court** : quelques minutes par itération, pas des heures.

</v-click>

<!--
Ce diagramme montre le cycle TDD.
Rouge : le test échoue car la fonctionnalité n'existe pas.
Vert : on fait le minimum pour passer au vert.
Refactor : on améliore le code en gardant les tests verts.
Puis on recommence avec le prochain test.
-->

---

# Les trois lois du TDD

<br>

Robert C. Martin (Uncle Bob) résume le TDD en **3 lois** :

<v-clicks>

1. **Loi 1** — On n'écrit pas de code de production tant qu'on n'a pas un test qui échoue
2. **Loi 2** — On n'écrit pas plus de test qu'il n'en faut pour échouer (ne pas compiler = échouer)
3. **Loi 3** — On n'écrit pas plus de code de production qu'il n'en faut pour faire passer le test

</v-clicks>

<v-click>

<br>

> Ces lois imposent des **cycles de quelques minutes**. Si on reste bloqué plus de 10 minutes, c'est qu'on a fait un pas trop grand.

</v-click>

<!--
Ces lois semblent extrêmes mais c'est exactement la discipline qui fait la force du TDD.
Elles empêchent de "coder en avance" sur ce que les tests demandent.
La conséquence : le code est toujours minimal et justifié.
-->

---

# Baby steps — L'art du plus petit pas

<br>

<v-clicks>

Le TDD repose sur le **plus petit incrément possible** :

- Écrire **un seul test** à la fois
- Faire passer ce test avec le **minimum de code**
- Résister à la tentation d'anticiper les cas futurs
- **Committer** à chaque test vert

</v-clicks>

<v-click>

<br>

### Pourquoi ?

- Si un test échoue, on sait **exactement** ce qui a changé
- On peut toujours revenir en arrière d'un seul commit
- Ça force à **décomposer** les problèmes complexes

</v-click>

<!--
Le piège classique du débutant TDD : écrire 5 tests d'un coup, puis coder pendant 30 minutes.
Ce n'est plus du TDD, c'est du Test First.
Le "plus petit pas possible" est contre-intuitif mais c'est ce qui rend le TDD efficace.
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

---

# Quand TDD fonctionne le mieux

<br>

<v-clicks>

### ✅ Bien adapté

- **Logique métier** : règles de calcul, validation, transformations
- **Algorithmes** : tri, parsing, conversions
- **Code nouveau** (greenfield) : pas de contraintes existantes

### ⚠️ Plus difficile

- **UI** : le feedback est visuel, difficile à exprimer en assertions
- **Infrastructure** : accès BDD, réseau, système de fichiers → tests d'intégration
- **Code legacy** : pas de tests existants, couplage fort → nécessite du refactoring préalable

</v-clicks>

<!--
TDD brille sur la logique métier et les algorithmes.
Pour le reste, on peut combiner TDD avec d'autres stratégies (tests d'intégration, tests manuels).
Le code legacy est le cas le plus difficile : on ne peut pas faire du TDD sans pouvoir tester, et on ne peut pas tester sans refactorer.
-->

---

# L'angle IA — Tests et TDD comme compétence durable

<br>

<v-clicks>

Dans un monde où l'IA peut **générer du code**, la compétence durable est de **définir ce que le logiciel doit faire** — et de le **vérifier**.

Les tests sont la meilleure façon de formaliser cette intention.

</v-clicks>

<v-click>

Le TDD est la **boucle idéale** de collaboration humain-IA :

```
  Humain → [Écrit le test] → IA → [Propose le code] → Test → ✅/❌
                                                          │
                                                          └─► Si ❌ : l'IA corrige
```

</v-click>

<v-click>

<br>

> L'IA peut écrire le *comment*. Le développeur qui maîtrise le TDD définit le *quoi* — et le **vérifie**.

</v-click>

<!--
Ce n'est pas un cours sur l'IA. Mais cette boucle montre pourquoi le TDD prend de la valeur.
L'humain définit le QUOI (le test), la machine propose le COMMENT (l'implémentation).
Le développeur qui sait écrire de bons tests peut vérifier N'IMPORTE QUEL code.
-->
