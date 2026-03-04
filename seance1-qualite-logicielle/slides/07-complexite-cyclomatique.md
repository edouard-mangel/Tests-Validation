---
layout: section
---

# Complexité cyclomatique

---
layout: two-cols-header
---

# Complexité cyclomatique — Les graphes

::left::

### Graphe connexe

<img src="/images/graphe-connexe.png" class="h-48 mx-auto mt-4" />

<v-click>

Tous les nœuds sont reliés — il existe un chemin entre n'importe quelle paire de nœuds.

</v-click>

::right::

<v-click>

### Graphe non connexe

<img src="/images/graphe-non-connexe.png" class="h-48 mx-auto mt-4" />

Composé de **sous-graphes** distincts (composantes connexes).

</v-click>

<!--
Les graphes connexes représentent du code où tous les chemins sont accessibles.
Un graphe non connexe peut indiquer du code mort (composantes inaccessibles).
-->

---

# Complexité cyclomatique — Formule

<br>

<v-click>

La complexité cyclomatique **M** d'un programme est :

<br>

$$M = E - N + 2P$$

</v-click>

<v-clicks>

Où :

- **M** = complexité cyclomatique
- **E** = nombre d'**arêtes** du graphe (transitions entre blocs)
- **N** = nombre de **nœuds** du graphe (blocs de code)
- **P** = nombre de **composantes connexes** du graphe

</v-clicks>

<!--
En pratique, la complexité cyclomatique correspond au nombre de chemins indépendants dans le code.
Un `if` ajoute un chemin. Un `switch` avec 5 cas ajoute 5 chemins. Une boucle ajoute un chemin.
-->

---

# Complexité cyclomatique — Ce qu'il faut retenir

<br>

<v-clicks>

- Plus on a de **chemins algorithmiques**, plus la complexité augmente
- Pour garantir un code de qualité, il faut **diminuer les tests conditionnels**

<br>

| Complexité | Risque |
|-----------|--------|
| 1 – 10 | ✅ Code simple, bien structuré |
| 11 – 20 | ⚠️ Code modérément complexe |
| 21 – 50 | ❌ Code complexe, risqué |
| > 50 | 🚨 Code non testable, à refactorer |

<br>

> Chaque `if`, `else`, `for`, `while`, `catch` ajoute +1 à la complexité.

</v-clicks>

<!--
Règle pratique : une méthode avec une complexité > 10 devrait être découpée.
Lien avec les tests : une complexité de N signifie qu'il faut au minimum N tests pour couvrir tous les chemins.
C'est pour ça que réduire la complexité facilite les tests.
-->
