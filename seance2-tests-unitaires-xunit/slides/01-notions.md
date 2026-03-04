---
layout: two-cols-header
---

# Boîte blanche vs Boîte noire

::left::

### Boîte blanche *(White Box)*

<v-clicks>

On **connaît** le fonctionnement interne du système testé.

On utilise le **code source** comme base pour concevoir les tests.

→ Tests unitaires, tests d'intégration écrits par le développeur.

</v-clicks>

::right::

<v-clicks>

### Boîte noire *(Black Box)*

On **ne connaît pas** (ou n'utilise pas) le code source.

On teste à partir des **entrées/sorties** observables.

→ Tests e2e, tests de non-régression, tests QA.

</v-clicks>

<!--
Distinction fondamentale en théorie des systèmes.
La boîte blanche = le développeur connaît l'implémentation.
La boîte noire = on teste le comportement, pas l'implémentation.

Nota Bene du slide original : considérer les tests de non-régression comme des boîtes noires pour les découpler de l'implémentation technique. C'est exactement ce qu'on verra en séance 3 avec les deux écoles de TU.
-->

---

# Un point de vue important

<br>

<v-click>

> *"Il peut être intéressant de considérer tous les tests de non-régression comme des tests de **boîte noire**, pour découpler le comportement souhaité de leur implémentation technique."*

</v-click>

<v-clicks>

<br>

Même en écrivant des tests unitaires (boîte blanche), on peut **adopter une posture boîte noire** :

- Tester **ce que fait** la fonction, pas **comment** elle le fait
- Résister à la tentation de tester les détails d'implémentation
- Écrire des tests qui survivent aux refactorings

</v-clicks>

<!--
Ce point sera développé en séance 3 avec les "deux écoles" de tests unitaires.
Anticiper la notion : un bon test unitaire teste un comportement, pas une ligne de code.
-->
