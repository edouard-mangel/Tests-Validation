# Les 5 axes CISQ

Le **Consortium for IT Software Quality** organise l'analyse de la qualité autour de 5 axes :

<br>

<v-clicks depth="2">

| Axe | Description |
|-----|-------------|
| **Fiabilité** | Résilience et solidité structurelle |
| **Efficacité** | Performance en exploitation |
| **Sécurité** | Probabilité de faille |
| **Maintenabilité** | Adaptabilité et transférabilité |
| **Taille** | Impact sur tous les autres axes |

</v-clicks>

<!--
Le CISQ est une organisation internationale qui standardise la mesure de la qualité logicielle.
Ces 5 axes forment un cadre pratique pour évaluer un logiciel de façon objective.
-->

---

# Fiabilité

<br>

<v-click>

Un attribut de **résilience** et de **solidité structurelle**.

</v-click>

<v-clicks depth="2">

- Mesure le **niveau de risque d'échec** de l'application
- Réduire les temps d'indisponibilité et les défauts impactant les utilisateurs
- Améliore l'image du département IT au sein de l'entreprise

</v-clicks>

<v-click>

<br>

> Un logiciel fiable est un logiciel sur lequel on peut compter — même dans des conditions dégradées.

</v-click>

<!--
La fiabilité est souvent la première préoccupation des utilisateurs finaux.
Un logiciel qui "tombe" régulièrement, même s'il est fonctionnellement riche, sera perçu comme de mauvaise qualité.
-->

---

# Efficacité

<br>

<v-click>

Le **code source et l'architecture** sont ce qui garantit l'efficacité du logiciel en phase d'exploitation.

</v-click>

<v-clicks depth="2">

- Particulièrement important dans les contextes où la **rapidité est critique**
- Une analyse de l'efficacité et de la **scalabilité** du code fournit une image claire des risques business
- Risques liés aux dégradations des **temps de réponse**

</v-clicks>

<v-click>

<br>

> Un code inefficace ne se voit pas en développement — il se voit en production sous charge.

</v-click>

<!--
L'efficacité se mesure : temps de réponse, consommation mémoire, nombre de requêtes BDD.
L'optimisation prématurée est à éviter, mais un code structurellement inefficace est un problème dès la conception.
-->

---

# Sécurité

<br>

<v-click>

Une mesure de la **probabilité de faille** due à des pratiques de code ou d'architecture.

</v-click>

<v-click>

<br>

Quantifie les vulnérabilités critiques :

</v-click>

<v-clicks depth="2">

- **Perte d'informations stratégiques**
- **Fuite de données à caractère personnel** (RGPD)
- Attaques par injection, accès non autorisés, etc.

</v-clicks>

<v-click>

<br>

> La sécurité n'est pas une fonctionnalité à ajouter en fin de projet — c'est une qualité intrinsèque du code.

</v-click>

<!--
La sécurité est souvent négligée car elle n'est pas visible par l'utilisateur — jusqu'au jour où il y a une faille.
OWASP Top 10 : SQL injection, XSS, CSRF… tous sont des problèmes de code, pas seulement d'infrastructure.
-->

---

# Maintenabilité

<br>

<v-click>

Inclut les notions d'**adaptabilité**, de **portabilité** et de **transférabilité**.

</v-click>

<v-clicks depth="2">

- Essentielle pour les applications critiques pour le métier
- Quand le *time to market* est un aspect important de la compétitivité
- Garder sous contrôle les **coûts de maintenance**

</v-clicks>

<v-click>

<br>

> Un code maintenable est un code qu'un autre développeur peut comprendre, modifier et tester — sans craindre de tout casser.

</v-click>

<!--
La maintenabilité est souvent sacrifiée sous la pression des délais.
Elle se paie au moment où on veut faire évoluer le logiciel : refactoring impossible, bugs cascades, etc.
-->

---

# Taille

<br>

<v-click>

Bien que ce ne soit pas un attribut de qualité à part entière, la taille **impacte tous les autres axes**.

</v-click>

<v-clicks depth="2">

- **Taille du code source** : nombre de lignes, de fichiers, de modules
- **Taille des binaires** : fichiers générés, images Docker
- Combinée aux autres axes → estime le **volume de travail effectué**
- Permet de mesurer ou estimer la **productivité**

</v-clicks>

<v-click>

<br>

> Un code plus petit est généralement plus facile à comprendre, tester et maintenir.

</v-click>

<!--
La taille n'est pas un problème en soi, mais elle amplifie tous les autres problèmes.
Un code volumineux avec une mauvaise maintenabilité est un cauchemar à gérer.
-->
