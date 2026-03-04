---
layout: image-right
image: /images/typologie-tests.png
backgroundSize: contain
---

# Les différents types de tests


<v-click>

## Rassurez-vous, on ne va pas tous les faire !
</v-click>


<!--
Laisser les étudiants observer l'étendue de la typologie.
Source : gearsoftesting.org/test-typology.html
Il existe des dizaines de types de tests — mais on va se concentrer sur 3.
-->

---

# Les différents types de tests

<br>

Il existe des dizaines de types de tests (performance, sécurité, accessibilité, charge...).

<v-click>

En tant que développeur, **trois types** nous concernent directement :

<br>

| Type | Quoi ? | Qui ? |
|------|--------|-------|
| **End-to-end (e2e)** | L'application complète, du point de vue utilisateur | Dev / QA |
| **Intégration** | La communication entre composants | Dev |
| **Unitaire** | La logique métier isolée | Dev |

</v-click>

<!--
On ne va pas parler de TOUS les types de tests. On se concentre sur ceux que le développeur écrit et maintient.
-->

---
layout: two-cols-header
---

# La pyramide des tests

::left::

### La Rache®

<img src="/images/pyramide-la-rache.png" class="h-60 mx-auto" />

<v-click>

Beaucoup de tests manuels et e2e, peu de tests unitaires → **lent, fragile, cher**

</v-click>

::right::

<v-click>

### Méthode Agile

<img src="/images/pyramide-agile.png" class="h-60 mx-auto" />

</v-click>

<v-click>

Beaucoup de tests unitaires, peu de e2e → **rapide, stable, économique**

</v-click>

<!--
La pyramide "La Rache" c'est quand on n'a que des tests manuels ou des tests Selenium partout.
La pyramide agile inverse le ratio : la base large ce sont les tests unitaires (rapides, stables).
Les tests e2e restent utiles mais en petit nombre, pour les parcours critiques.
La pyramide n'est pas un dogme, mais une direction à suivre.
-->
