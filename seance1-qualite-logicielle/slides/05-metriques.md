# Les catégories de métriques

<br>

<v-click>

Il y a **3 principales catégories** de métriques dans la qualité logicielle :

</v-click>

<v-clicks depth="2">

- **Maintenance applicative** — suivi des corrections, incidents, temps de résolution
- **Qualité applicative** — mesure des caractéristiques du code
- **Respect des processus** — couverture de tests, revues de code, intégration continue

</v-clicks>

<v-click>

<br>

Par souci de simplification, on distingue **2 types de caractéristiques mesurables** :

- Les **bugs** — comportement qui n'est pas celui attendu
- Les **défauts** — caractéristiques qui rendent l'évolution du logiciel difficile

</v-click>

<!--
Les bugs sont visibles et urgents. Les défauts sont invisibles et s'accumulent silencieusement.
C'est pourquoi la qualité de code nécessite une vigilance active — pas juste corriger les bugs.
-->

---
layout: image-right
image: images/maintenabilite.jpg
backgroundSize: contain
---

# Maintenabilité — Vue d'ensemble

<br>

<v-click>

Quelques métriques clés liées à la maintenabilité :

</v-click>

<v-clicks depth="2">

- **Nombre de lignes de code**
- **Couplage** entre modules
- **Complexité cyclomatique**
- **Cohésion** des modules
- **Densité de défauts** — les *code smells*

</v-clicks>

<!--
Ces métriques sont au cœur du reste de la séance.
On va détailler chacune d'elles avec une définition, un exemple, et ce qu'il faut en retenir.
-->
