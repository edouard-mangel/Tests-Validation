# Choisir le bon niveau de test

<br>

La pyramide des tests comme **outil de décision** :

<v-clicks>

| Je veux tester... | Niveau | Pourquoi |
|-------------------|--------|----------|
| Une règle de calcul | **Unitaire** | Pure logique, pas de dépendance |
| Une validation métier | **Unitaire** | Isolable avec des doublures |
| Le mapping ORM / les requêtes SQL | **Intégration** | Besoin d'une vraie BDD |
| Le routing et la sérialisation API | **Intégration** | Besoin du pipeline HTTP |
| Le parcours de login utilisateur | **E2E** | Besoin du navigateur et du serveur |
| Que l'application démarre correctement | **E2E** (smoke) | Vérifie l'assemblage complet |

</v-clicks>

<!--
La question à se poser : "Quel est le niveau MINIMUM qui me permet de vérifier ce comportement ?"
Plus on monte dans la pyramide, plus c'est coûteux. On ne monte que quand c'est nécessaire.
-->

---

# La pyramide comme guide

<br>

```
         /  e2e  \          5-10 tests : parcours critiques
        /─────────\
       / intégration \      20-50 tests : repos, API, config
      /───────────────\
     /    unitaires     \   200+ tests : logique métier, algorithmes
    /─────────────────────\
```

<v-clicks>

### Proportions indicatives (pas un dogme !)

- **~70%** tests unitaires — rapides, stables, nombreux
- **~20%** tests d'intégration — câblage, requêtes, pipeline
- **~10%** tests e2e — parcours critiques

</v-clicks>

<v-click>

> La pyramide n'est pas un objectif à atteindre mais une **direction** à suivre.

</v-click>

<!--
Les pourcentages varient selon le type de projet. Une API CRUD aura plus de tests d'intégration.
Un moteur de calcul aura plus de tests unitaires.
L'important : ne pas avoir une pyramide inversée (ice cream cone).
-->

---

# L'angle IA — Le jugement humain

<br>

<v-clicks>

Choisir le **bon niveau de test** est un jugement que l'IA ne peut pas faire à votre place.

Pourquoi ?

- Le choix dépend du **contexte projet** (criticité, budget, équipe)
- Il dépend de la **confiance** qu'on veut atteindre
- Il dépend des **compromis** coût/valeur acceptables
- Il change au fil du temps (un projet qui mûrit teste différemment)

<br>

> Savoir *quoi* tester et à *quel niveau* est le vrai savoir-faire du développeur testeur.

</v-clicks>

<!--
L'IA peut générer des tests, mais elle ne sait pas si un test d'intégration serait plus pertinent qu'un test unitaire pour un cas donné.
C'est le développeur qui comprend le contexte, les risques, et les compromis.
-->

---

# En résumé

<br>

<v-clicks>

| | Unitaire | Intégration | E2E |
|---|---------|-------------|-----|
| **Teste** | Logique isolée | Câblage entre composants | Parcours utilisateur |
| **Vitesse** | 🚀 ms | 🚶 secondes | 🐌 minutes |
| **Fidélité** | Basse (doublures) | Moyenne | Haute (tout est réel) |
| **Quantité** | Beaucoup (~70%) | Quelques (~20%) | Peu (~10%) |
| **Quand** | Toujours | Repos, API, config | Parcours critiques |

</v-clicks>

<v-click>

<br>

Les trois niveaux sont **complémentaires**. Aucun ne remplace les autres.

</v-click>

---
layout: center
class: text-center
---

# TP — Tests d'intégration

<br>

**Exercice 1** — Tests d'intégration pour un repository (InMemoryDatabase / SQLite)

**Exercice 2** — Tests d'API avec `WebApplicationFactory`

**Exercice 3** — Property-Based Testing avec FsCheck

**Exercice 4** — Contract Testing avec Pact

<br>

*Qu'est-ce que le test d'intégration a attrapé que le test unitaire n'aurait pas vu ?*

<!--
Le TP utilise une petite Web API ASP.NET fournie (TodoApi).
1. Tests de repository avec InMemory puis SQLite — comparer les comportements
2. Tests d'API avec WebApplicationFactory — découvrir un bug de sérialisation
3. Property-Based Testing avec FsCheck — propriétés roundtrip et invariant
4. Contract Testing avec Pact — écrire un test consommateur (guidé car concept nouveau)
Question de réflexion à la fin : tableau bug → niveau de test qui l'a détecté.
-->
