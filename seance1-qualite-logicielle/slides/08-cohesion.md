---
layout: section
---

# Cohésion

---

# Cohésion — Définition

<br>

<v-click>

> La **cohésion** représente le degré d'accord entre les différents éléments d'un module.
>
> Elle mesure le degré d'**encapsulation** d'un module et le masquage de l'information.

</v-click>

<v-clicks>

<br>

En pratique : **est-ce que tout ce qui est dans ce module a une raison d'y être ?**

- Haute cohésion → module **focalisé**, responsabilité unique
- Faible cohésion → module **fourre-tout**, difficile à maintenir

</v-clicks>

<!--
La cohésion est le pendant du couplage : on veut couplage faible ET haute cohésion.
C'est le principe de responsabilité unique (SRP) de SOLID.
-->

---
layout: two-cols-header
zoom: 0.9
---

# Les 7 niveaux de cohésion (Pressman)

::left::

<v-clicks>

**Niveaux faibles (à éviter)**

1. **Accidentelle** — aucun lien entre les méthodes
2. **Logique** — reliées par un critère vague commun
3. **Temporelle** — appelées au même moment
4. **Procédurale** — appelées dans un ordre spécifique

</v-clicks>

::right::

<v-clicks>

**Niveaux élevés (à viser)**

5. **Communicationnelle** — manipulent les mêmes données
6. **Séquentielle** — mêmes données + ordre spécifique
7. **Fonctionnelle** ✅ — module dédié à **une seule tâche**

</v-clicks>

<!--
Le niveau fonctionnel est le Graal : une classe, une responsabilité.
En pratique, on vise le niveau communicationnel ou supérieur.
-->

---
layout: image-right
image: images/cohesion-niveaux.png
backgroundSize: contain
---

# Cohésion — Illustration

<v-click>

Du niveau le plus faible (accidentel) au plus élevé (fonctionnel).

</v-click>

<v-clicks>

<br>

**Exemples concrets :**

- `Utils.cs` avec 50 méthodes disparates → **accidentelle** ❌
- `UserService.cs` qui gère utilisateurs ET envoi d'emails → **logique** ⚠️
- `EmailService.cs` qui ne fait qu'envoyer des emails → **fonctionnelle** ✅

</v-clicks>

<!--
Le fichier Utils est l'ennemi de la cohésion. Tout ce qui "ne rentre nulle part" y atterrit.
La règle : si vous ne pouvez pas nommer un module sans utiliser "Et" ou "Manager", il manque de cohésion.
-->

---

# Cohésion — Ce qu'il faut retenir

<br>

<v-clicks>

- Le niveau **accidentel** est le plus faible, le niveau **fonctionnel** le plus élevé
- Une bonne architecture logicielle nécessite la **plus forte cohésion possible**

<br>

**Règle pratique :** une classe devrait avoir **une seule raison de changer**

*(Principe de Responsabilité Unique — SRP)*

<br>

> Couplage faible + haute cohésion = code maintenable, testable, évolutif.

</v-clicks>

<!--
SRP est le "S" de SOLID. On le retrouvera dans les séances suivantes.
Le lien avec les tests : un module très cohésif est facile à tester car il fait peu de choses.
-->
