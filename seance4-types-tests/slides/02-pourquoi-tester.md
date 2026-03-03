---
layout: image-right
image: /images/pourquoi-tester.png
backgroundSize: contain
---

# Qu'est-ce que ça vous évoque ? 
<!--
Laisser l'image parler. CommitStrip : "Tester c'est douter".
Question à poser : "Qui a déjà eu peur de modifier du code parce qu'il n'y avait pas de tests ?"
-->

---

# Pourquoi automatiser les tests ?

## 🟢 À court terme

<br>

<v-clicks>

- La présence de tests apporte de la **sérénité** quant à la qualité des nouvelles fonctionnalités (moins de bugs)
- Les tests automatisés **aident au développement**
- Ils explicitent l'intention derrière le code, ils contribuent à la **documentation vivante**

</v-clicks>

<!--
C'est utile dès maintenant, pas juste "pour plus tard".
Les tests ne sont pas un coût supplémentaire, ils accélèrent le développement au quotidien.
-->

---

# Pourquoi automatiser les tests ?

## 🟡 À moyen terme

<br>

<v-clicks>

- Temps de recette réduit, ou amélioration de la qualité des tests manuels
- Limite le nombre et l'impact des **régressions**
- Facilite l'implémentation de nouvelles fonctionnalités via le **découplage** induit par la testabilité du code

</v-clicks>

<!--
Les régressions sont le cauchemar des équipes sans tests.
La testabilité force à découpler le code, ce qui le rend plus maintenable.
-->

---

# Pourquoi automatiser les tests ?

## 🔴 À long terme

<br>

<v-clicks>

- Limitation de la **dette technique**
- Diminution du **coût des bugs**
- Accélération de la vitesse de production de nouvelles fonctionnalités

</v-clicks>

<!--
La dette technique est ce qui tue les projets sur la durée.
Sans tests, chaque modification est un pari. Avec des tests, on peut refactorer sereinement.
-->

---

# Le coût des bugs dans le temps

<br>

```
Coût de correction
        │
   100x │                                          ████
        │                                          ████
    50x │                              ████        ████
        │                              ████        ████
    10x │                 ████         ████        ████
        │                 ████         ████        ████
     1x │    ████         ████         ████        ████
        │    ████         ████         ████        ████
        └─────────────────────────────────────────────────
           Développement   Tests     Recette      Production
```

<v-click>

Plus un bug est détecté tard, plus il coûte cher. Les tests automatisés détectent les bugs **au plus tôt**.

Mais **quel type de test** détecte **quel type de bug** ? C'est toute la question de cette séance.

</v-click>

<!--
Ce graphique est un classique (issu des études IBM/NIST).
Le ratio 1x/10x/50x/100x est approximatif mais l'ordre de grandeur est correct.
Un bug trouvé en production coûte ~100x plus cher qu'un bug trouvé pendant le développement.
La question de transition : les tests unitaires qu'on maîtrise détectent les bugs de logique. Mais quid des bugs d'intégration ?
-->

---

# Les trois types de tests du développeur

<br>

En tant que développeur, **trois types** de tests nous concernent directement :

<v-clicks>

| Type | Teste quoi ? | Vitesse | Quantité idéale |
|------|-------------|---------|-----------------|
| **Unitaire** | Logique métier isolée | 🚀 ms | Beaucoup |
| **Intégration** | Communication entre composants | 🚶 secondes | Quelques |
| **End-to-end** | Parcours utilisateur complet | 🐌 minutes | Peu |

</v-clicks>

<v-click>

<br>

Jusqu'ici on a travaillé sur les **tests unitaires** (séances 2 et 3).

Aujourd'hui : pourquoi ils ne suffisent pas, et quand passer aux niveaux supérieurs.

</v-click>

<!--
Présentation rapide des 3 types pour poser le cadre.
Les étudiants connaissent déjà bien les tests unitaires.
La suite de la séance va montrer les limites des TU seuls, puis introduire les tests d'intégration.
-->
