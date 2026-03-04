---
layout: two-cols-header
---

# Comment définit-on la qualité ?

::left::

<v-clicks depth="2">

### Larousse

> *"Ensemble des caractères, des propriétés qui font que quelque chose correspond bien ou mal à sa nature, à ce qu'on en attend."*

</v-clicks>

::right::

<v-clicks depth="2">

### AFNOR

> *"Un produit ou service de qualité est un produit dont les caractéristiques lui permettent de satisfaire les besoins exprimés ou implicites des consommateurs."*

</v-clicks>

<!--
Poser les deux définitions générales. Faire noter aux étudiants la notion de "besoin" : la qualité est relative à un contexte et à un utilisateur.
-->

---

# La qualité logicielle

<br>

<v-clicks depth="2">

Dans le domaine de l'ingénierie logicielle, il n'y a pas de **définition unique**.

Selon le point de vue, les attentes sont différentes :

</v-clicks>

<v-clicks depth="2">

- L'utilisateur veut un logiciel **sans bugs** et **rapide**
- Le développeur veut un code **maintenable** et **lisible**
- Le responsable sécurité veut un code **sans vulnérabilités**
- Le chef de projet veut un logiciel **livré dans les délais**

</v-clicks>

<v-clicks depth="2">

On peut toutefois distinguer différents **paramètres et axes** de la qualité logicielle.

</v-clicks>

<!--
Souligner que la qualité est multidimensionnelle. Ce qui est "de qualité" pour l'un peut ne pas l'être pour l'autre.
-->

---

# Les axes de la qualité d'un produit

<br>

<v-clicks depth="2">

- **L'adéquation** entre le besoin et la façon dont le logiciel y répond
- **L'absence de dysfonctionnement** — le logiciel fait ce qu'il est censé faire
- **La performance** — rapidité, efficacité des ressources
- **L'usage naturel** — ergonomie, facilité de prise en main
- **La transparence de fonctionnement** — comportement prévisible, traçabilité

</v-clicks>

<!--
Ces 5 axes sont généraux. On va maintenant les affiner avec le framework CISQ.
-->

---

# Comment mesure-t-on la qualité ?

<br>

<v-click>

Pour aborder une notion abstraite positive, on peut la définir par **l'absence du concept opposé**.

</v-click>

<v-click>

<br>

> *De même qu'on définit la paix comme l'absence de guerre, on peut définir la qualité par **l'absence de défauts**.*

</v-click>

<v-click>

<br>

### Qu'est-ce qu'un défaut ?

Un défaut est toute caractéristique qui n'est pas présente de façon **souhaitée ou optimale**.

Il peut se trouver dans tout aspect d'un logiciel — y compris les aspects esthétiques.

</v-click>

<!--
Cette définition par l'opposé permet de rendre la qualité concrète et mesurable.
Un défaut n'est pas forcément un bug : un code trop complexe est un défaut même s'il "fonctionne".
-->

---

# Pourquoi mesurer la qualité ?

<br>

<v-click>

> *"You can't control what you can't measure."*
>
> — Tom DeMarco

</v-click>

<v-clicks depth="2">

- Un code mal maîtrisé → **dette technique** croissante
- La surveillance des métriques → **diminution des coûts** de maintenance à moyen et long terme
- Un code homogène → **intégration facilitée** des nouveaux développeurs
- Une approche quantitative → processus **automatisable** et peu coûteux

</v-clicks>

<!--
La citation de DeMarco est la base de tout : si on ne mesure pas, on ne peut pas améliorer.
La dette technique est le coût caché d'un code de mauvaise qualité : elle s'accumule silencieusement jusqu'à rendre le projet ingérable.
-->
