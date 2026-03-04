---
layout: section
---

# Code Smells

---
layout: two-cols-header
---

# Code Smells — Définition

::left::

<v-clicks depth="2">

Un **code smell** est une caractéristique qui indique la **probabilité d'un problème plus important**.

Ce n'est pas forcément un bug — c'est un **signal d'alarme**.

Défini par **Kent Beck** dans les années 90, popularisé par **Martin Fowler** dans *Refactoring*.

</v-clicks>

::right::

<v-clicks depth="2">

<img src="/images/fowler-refactoring.jpg" class="h-40 mx-auto mb-4" />

<img src="/images/refactoring-guru.jpg" class="h-20 mx-auto" />

*refactoring.guru — catalogue en ligne*

</v-clicks>

<!--
Le terme "smell" est intentionnel : comme une odeur, un code smell ne prouve pas qu'il y a un problème, mais ça sent mauvais.
Martin Fowler a documenté 22 types de code smells dans son livre. Refactoring.guru les catalogue en ligne gratuitement.
-->

---

# Code Smells — Les Bloaters

*Gonflent les classes et méthodes*

<br>

<v-clicks depth="2">

- **Long Method** — toute méthode faisant plus de 30 lignes de code
- **Large Class** — toute classe contenant trop de champs, méthodes, etc.
- **Long Parameter List** — méthode prenant plus de 3 arguments

</v-clicks>

<v-click>

<br>

> Une méthode longue cache souvent plusieurs responsabilités.
> Une classe large manque de cohésion.

</v-click>

<!--
La règle des 30 lignes est indicative. L'important : une méthode = une seule chose.
Long parameter list : souvent le signe qu'un objet manque (ces 4 paramètres forment-ils un concept métier ?)
-->

---

# Code Smells — Les Change Preventers

*Empêchent la modification facile*

<br>

<v-clicks depth="2">

- **Large Switch Statement** — `if/else` avec trop de conditions (à remplacer par du polymorphisme)
- **Shotgun Surgery** — tout changement dans A doit se répercuter dans de nombreuses autres classes
- **Parallel Inheritance Hierarchy** — créer une sous-classe de A oblige à créer une sous-classe de B

</v-clicks>

<v-click>

<br>

> Ces smells indiquent un **couplage fort** ou une **mauvaise abstraction**.

</v-click>

<!--
Le Shotgun Surgery est particulièrement douloureux : modifier une règle métier oblige à toucher 15 fichiers.
La solution : centraliser la logique dans une seule classe bien cohésive.
-->

---

# Code Smells — Les Dispensables

*Code qu'on pourrait supprimer*

<br>

<v-clicks depth="2">

- **Commentaires inutiles** — tout commentaire qui "explique" ce que fait le code
  *(si le code a besoin d'être expliqué, il faut le réécrire)*
- **Code mort** — code jamais appelé, ou commenté
- **Speculative Generality** — code écrit "pour plus tard" qui ne sert à rien aujourd'hui

</v-clicks>

<v-click>

<br>

> *"You Ain't Gonna Need It"* — YAGNI

</v-click>

<!--
Le code mort est dangereux car il crée de la confusion et peut masquer de vraies failles.
Les commentaires qui restent "en cas de besoin" ou "// TODO" accumulés sont une forme de dette technique.
YAGNI : ne pas anticiper des besoins hypothétiques. C'est une des causes principales de sur-ingénierie.
-->

---

# Code Smells — Les Couplers

*Code qui génère du couplage*

<br>

<v-clicks depth="2">

- **Feature Envy** — un traitement sur des données d'une classe A est régulièrement fait dans une classe B
  *(le code "envie" d'être ailleurs)*
- **Message Chains** — `a.getB().getC().getD().getExample()`
  *(violation de la Loi de Déméter)*
- **Middle Man** — classe qui ne fait que déléguer tous ses appels à une autre classe

</v-clicks>

<v-click>

<br>

> Ces smells indiquent que les responsabilités sont **mal réparties**.

</v-click>

<!--
La Loi de Déméter : "ne parlez qu'à vos amis immédiats". Une longue chaîne de messages crée une dépendance à toute la chaîne.
Le Middle Man : si une classe ne fait que déléguer, pourquoi existe-t-elle ? Soit elle devrait disparaître, soit elle devrait avoir plus de responsabilités.
Feature Envy : si B manipule souvent les données de A, peut-être que cette logique devrait être dans A.
-->

---

# Les 4 familles de code smells — Résumé

<br>

| Famille | Symptôme | Solution |
|---------|----------|----------|
| **Bloaters** | Trop grand | Découper, extraire |
| **Change Preventers** | Difficile à modifier | Polymorphisme, centraliser |
| **Dispensables** | Code inutile | Supprimer |
| **Couplers** | Trop dépendant | Réorganiser les responsabilités |

<v-click>

<br>

> Les code smells ne nécessitent pas toujours d'être corrigés immédiatement.
> Mais ils **doivent être connus** pour être surveillés.

</v-click>

<!--
Conclusion sur les code smells.
L'outil SonarQube détecte automatiquement la plupart de ces smells. On en parlera dans les séances suivantes.
-->
