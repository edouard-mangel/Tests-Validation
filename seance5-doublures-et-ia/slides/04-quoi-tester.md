---
layout: two-cols-header
---

# Tout tester = tester rien

::left::

### ✅ À tester

<v-clicks>

- Logique métier (calculs, règles, validations)
- Cas limites et edge cases
- Bug fixes (un test par bug corrigé)
- Code complexe avec plusieurs chemins
- Algorithmes et transformations de données

</v-clicks>

::right::

### ❌ À éviter

<v-clicks>

- Getters / setters triviaux
- Code de "glue" du framework
- Disposition de l'UI
- Configuration et mapping simple
- Code généré automatiquement

</v-clicks>

<!--
Tous les tests n'ont pas la même valeur. Tester un getter qui retourne un champ est du temps perdu.
Tester une règle métier complexe, c'est un investissement qui rapporte.
La question à se poser : "Si ce code a un bug, quelles sont les conséquences ?"
-->

---

# La règle du ROI

Un test a de la valeur s'il protège contre un bug **probable** et **coûteux**.

<br>

<v-clicks>

- 🎯 **Probabilité de bug** — Le code est-il complexe ? A-t-il plusieurs chemins ?
- 💥 **Coût d'un bug** — Un bug ici, c'est un inconfort ou une perte financière ?
- 🧩 **Complexité du test** — Le test est-il simple à écrire et à maintenir ?

</v-clicks>

<br>

<v-click>

| | Facile à tester | Difficile à tester |
|---|---|---|
| **Fort impact** | ✅ Tester en priorité | ⚠️ Tester si possible |
| **Faible impact** | 🤷 Tester si rapide | ❌ Ne pas tester |

</v-click>

<!--
Le ROI (Return On Investment) : le test doit rapporter plus qu'il ne coûte.
Un test qui prend 2h à écrire pour protéger un getter trivial → mauvais ROI.
Un test qui prend 10min pour protéger un calcul de tarification → excellent ROI.
Montrer le tableau et demander aux étudiants de donner des exemples pour chaque case.
-->

---

# Résumé : la stratégie complète

<br>

| Séance | Concept | Apport |
|--------|---------|--------|
| **S3** — TDD | Le cycle Rouge/Vert/Refactor | **Quand** écrire les tests |
| **S3** — FIRST | Fast, Isolated, Repeatable, Self-validating, Timely | **Comment** écrire de bons tests |
| **S4** — Pyramide | Unitaires > Intégration > E2E | **Combien** de chaque type |
| **S5** — Doublures | Stubs, mocks, fakes + DI | **Comment** tester le vrai code |
| **S5** — ROI | Probabilité × Coût × Complexité | **Quoi** tester en priorité |

<v-click>

> *Vous avez maintenant tous les outils. Reste à les mettre en pratique.*

</v-click>

<!--
Slide de synthèse qui relie toutes les séances.
Chaque séance a apporté une pièce du puzzle.
TDD = le rythme, FIRST = la qualité, pyramide = l'équilibre, doublures = l'isolation, ROI = le focus.
Transition vers la dernière partie : les tests et l'IA.
-->
