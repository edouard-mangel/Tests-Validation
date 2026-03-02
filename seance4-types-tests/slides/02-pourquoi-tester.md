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

Plus un bug est détecté tard, plus il coûte cher à corriger.

Les tests automatisés permettent de détecter les bugs **au plus tôt**.

</v-click>

<!--
Ce graphique est un classique (issu des études IBM/NIST).
Le ratio 1x/10x/50x/100x est approximatif mais l'ordre de grandeur est correct.
Un bug trouvé en production coûte ~100x plus cher qu'un bug trouvé pendant le développement.
-->
