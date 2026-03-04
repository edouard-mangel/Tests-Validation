# Caractéristiques souhaitées d'un logiciel

<br>

<v-clicks depth="2">

| Caractéristique | Axes concernés |
|----------------|----------------|
| **Documentation** | Maintenabilité (+ tous indirectement) |
| **Pratiques de code** | Fiabilité, Sécurité, Efficacité, Maintenabilité |
| **Complexité** | Fiabilité, Maintenabilité (+ Sécurité) |
| **Standards d'Architecture** | Fiabilité, Sécurité, Efficacité, Maintenabilité |
| **Taille** | Maintenabilité |

</v-clicks>

<!--
Vue d'ensemble des 5 caractéristiques et leur impact sur les axes CISQ.
On va détailler chacune.
-->

---

# Documentation

<br>

<v-click>

La documentation ne se limite pas aux **documents externes** (specs, manuels, cahiers de tests).

</v-click>

<v-click>

<br>

**Le code lui-même est une forme de documentation :**

</v-click>

<v-clicks depth="2">

- Le nommage des classes, membres et fonctions
- L'indentation et la mise en forme
- L'organisation du code en dossiers/modules

</v-clicks>

<v-click>

<br>

> Un code bien nommé se lit comme de la prose — il documente son intention sans commentaires superflus.

</v-click>

<!--
Insister sur ce point clé : le code EST de la documentation.
Un bon nommage est souvent plus utile qu'un commentaire qui explique ce que fait le code (et qui peut devenir obsolète).
-->

---

# Pratiques de code

<br>

<v-clicks depth="2">

- **Application des principes de la POO** — encapsulation, héritage, polymorphisme, abstraction
- **Gestion d'erreurs** — codes de retour vs exceptions : choisir et s'y tenir
- **Protection des accès** — visibilité des membres, assainissement des paramètres
- **Programmation défensive** — ne jamais faire confiance aux entrées

</v-clicks>

<v-click>

<br>

**Axes concernés :** Fiabilité · Sécurité · Efficacité · Maintenabilité

</v-click>

<!--
Les pratiques de code sont les habitudes quotidiennes du développeur.
La programmation défensive : valider les entrées, gérer les cas limites, ne pas supposer que le code appelant est correct.
-->

---

# Complexité

<br>

<v-clicks depth="2">

- **Complexité cyclomatique** — nombre de chemins d'exécution dans une fonction
- **Niveaux d'indentation** — un indicateur visuel de la complexité
- **Algorithmie** — notation Big O : O(n), O(n²), O(log n)...
- **Pratiques de programmation** — if/else vs polymorphisme vs composition
- **Code mort** et code non testé

</v-clicks>

<v-click>

<br>

**Axes concernés :** Fiabilité · Maintenabilité · Sécurité (indirectement)

</v-click>

<!--
La complexité cyclomatique sera détaillée plus loin.
Règle pratique : une méthode avec une complexité > 10 doit être refactorisée.
Le code mort est dangereux : il crée de la confusion et des risques de sécurité.
-->

---

# Standards d'Architecture

<br>

<v-clicks depth="2">

- **Architecture découplée** — en couches, MVC, hexagonale...
- **Gestion de la persistance** — patterns d'accès aux données (Repository, ORM)
- **Ratios de couplage** entre les modules
- **Réutilisation** et généricité des modules

</v-clicks>

<v-click>

<br>

**Axes concernés :** Fiabilité · Sécurité · Efficacité · Maintenabilité

</v-click>

<!--
Une architecture bien pensée protège contre toutes les catégories de problèmes de qualité.
L'architecture hexagonale (ports & adapters) est particulièrement intéressante pour la testabilité.
-->

---

# Taille du code

<br>

<v-clicks depth="2">

- **Nombre de lignes de code** (NLOC) — métrique historique, à prendre avec recul
- **Taille des fichiers binaires** générés
- **Taille des images Docker**

</v-clicks>

<v-click>

<br>

La taille n'est pas un problème en soi, mais elle **amplifie** tous les autres problèmes de qualité.

<br>

**Axe concerné :** Maintenabilité

</v-click>

<!--
Une grande base de code n'est pas un problème si elle est bien structurée.
Mais combinée à une mauvaise maintenabilité, la taille rend le projet ingérable.
-->
