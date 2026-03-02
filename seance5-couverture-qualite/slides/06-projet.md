# Projet de groupe — Séances 6, 7 et 8

<br>

<v-clicks>

### Format

- Groupes de **3-4 étudiants**
- Construire une application **from scratch** en TDD
- 3 séances de travail encadré

### Exemples de sujets (vous pouvez proposer le vôtre)

- Un système de gestion de bibliothèque (API)
- Un système de réservation
- Un moteur de panier e-commerce + checkout
- Un gestionnaire de tâches en CLI avec persistance

</v-clicks>

<!--
Les étudiants choisissent leur sujet. L'important n'est pas la complexité de l'application mais la rigueur du TDD.
Un projet simple mais bien testé vaut mieux qu'un projet ambitieux sans tests.
-->

---

# Déroulement des séances

<br>

<v-clicks>

### Séance 6 — Kickoff et premiers cycles TDD

- Définir le domaine, écrire les premiers tests
- À la fin : la logique métier de base est sous test

### Séance 7 — Construction et revue de mi-parcours

- Continuer le TDD, ajouter les tests d'intégration
- **Peer review** : les groupes échangent leurs repos et relisent les tests
- Feedback de l'instructeur sur la discipline TDD

### Séance 8 — Finalisation et évaluation

- Dernière ligne droite
- **Présentation** (~15 min par groupe) : démo, walkthrough des tests, historique de commits

</v-clicks>

<!--
La peer review de la séance 7 est importante : elle force à écrire des tests lisibles par d'autres.
-->

---

# Critères d'évaluation

<br>

| Critère | Poids | Ce qu'on regarde |
|---------|-------|------------------|
| **Discipline TDD** | 30% | L'historique de commits montre Red→Green→Refactor, petits incréments |
| **Qualité des tests** | 25% | Tests lisibles, testent le comportement (pas l'implémentation), bons noms |
| **Couverture et stratégie** | 20% | Mix pertinent unit/intégration, couverture significative (pas juste élevée) |
| **Design et testabilité** | 15% | Architecture propre, injection de dépendances, principes SOLID |
| **Présentation** | 10% | Capacité à expliquer *pourquoi* on a testé ce qu'on a testé, compromis |

<!--
Les critères reflètent tout ce qu'on a vu dans le module.
L'historique de commits est la preuve de la discipline TDD — c'est pour ça qu'on insiste sur les commits à chaque test vert.
-->

---

# Ce qui est attendu

<br>

<v-clicks>

### ✅ Attendu

- Historique de commits montrant le cycle TDD
- Tests unitaires pour la logique métier
- Au moins quelques tests d'intégration
- Code avec injection de dépendances
- Tests lisibles qui documentent le comportement

### ❌ À éviter

- Un seul gros commit à la fin ("ajout de tous les tests")
- Des tests qui ne testent rien (assertions tautologiques)
- Du code couplé et impossible à tester
- Des mocks partout qui testent l'implémentation

</v-clicks>

---
layout: center
class: text-center
---

# TP — Qualité des tests + Formation des groupes

<br>

**Exercice 1** — Identifier les test smells dans un codebase fourni

**Exercice 2** — Réécrire les tests pour les rendre pertinents

**Exercice 3** — Lancer Stryker et comparer avant/après

<br>

*Puis : formation des groupes et choix des sujets de projet*

<!--
Le TP en deux temps :
1. Exercices sur les anti-patterns et Stryker (1h30)
2. Formation des groupes, choix du sujet, et début de réflexion sur le domaine (30min)
-->
