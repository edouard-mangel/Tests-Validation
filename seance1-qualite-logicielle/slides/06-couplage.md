---
layout: section
---

# Couplage

---

# Couplage — Définition

<br>

<v-click>

> Le **couplage** représente une relation entre deux éléments, classes ou modules.
>
> Il peut être **fort** ou **faible**.
>
> Son degré influe sur les répercussions des modifications d'un élément sur l'autre.

</v-click>

<v-clicks>

<br>

- **Couplage fort** — un changement dans A nécessite un changement dans B
- **Couplage faible** — A et B sont indépendants, A peut changer sans impacter B

</v-clicks>

<!--
Exemple concret : si la classe CommandeService connaît directement la classe MySqlRepository, changer la base de données oblige à modifier CommandeService. C'est du couplage fort.
Avec une interface IRepository, CommandeService ne sait pas quelle implémentation est utilisée. C'est du couplage faible.
-->

---
layout: image
image: images/couplage.png
backgroundSize: contain
---

<!--
Schéma illustrant le couplage fort vs faible entre composants.
-->

---

# Couplage — Ce qu'il faut retenir

<br>

<v-click>

> Plus le couplage entre les composants est **fort**, plus le logiciel est difficile à **maintenir**.
>
> On favorise toujours un **couplage faible**.

</v-click>

<v-clicks>

<br>

**Comment réduire le couplage ?**

- Passer par des **interfaces** plutôt que des implémentations concrètes
- Utiliser l'**injection de dépendances**
- Respecter le principe de **séparation des préoccupations** (SoC)

</v-clicks>

<!--
L'injection de dépendances sera le sujet de la séance 3.
Anticiper : le couplage fort est aussi ce qui rend le code difficile à tester — on ne peut pas remplacer une dépendance par une doublure si elle est créée directement dans la classe.
-->
