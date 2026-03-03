# Pourquoi automatiser les tests ?

<br>

<v-clicks>

## 🟢 À court terme

- **Sérénité** face aux nouvelles fonctionnalités (moins de bugs)
- Les tests **aident au développement** (feedback immédiat)
- Ils contribuent à la **documentation vivante** du projet

## 🟡 À moyen terme

- Temps de recette réduit, qualité des tests manuels améliorée
- Limite le nombre et l'impact des **régressions**
- Facilite l'évolution via le **découplage** induit par la testabilité

## 🔴 À long terme

- Limitation de la **dette technique**
- Diminution du **coût des bugs**
- Accélération de la vitesse de livraison

</v-clicks>

<!--
Rappel rapide — les étudiants ont vu la qualité logicielle en séance 1 et pratiqué les tests unitaires en séance 2-3.
On repose ici le cadre global avant d'élargir au-delà des tests unitaires.
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

Mais **quel type de test** détecter **quel type de bug** ? C'est toute la question de cette séance.

</v-click>

<!--
Ce graphique est un classique (issu des études IBM/NIST).
Le ratio 1x/10x/50x/100x est approximatif mais l'ordre de grandeur est correct.
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
La suite de la séance va montrer les limites des TU seuls, puis introduire les tests d'intégration et e2e.
-->
