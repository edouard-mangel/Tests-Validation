---
theme: ../theme
title: "Tests & Validation - Séance 3"
info: |
  ## TDD, bonnes pratiques et doublures de test
  Module Tests & Validation - CNAM
class: text-center
author: Edouard Mangel
keywords: tdd, test-driven development, bonnes pratiques, FIRST, test doubles, mock, stub, NSubstitute
drawings:
  persist: false
transition: slide-left
mdc: true
duration: 90min
---

# Tests & Validation

## Séance 3 — TDD, bonnes pratiques et doublures de test

<br>

*Du cycle Rouge/Vert/Refactor à l'isolation par les doublures.*

<!--
Objectif de la séance : maîtriser le TDD et les principes FIRST, puis savoir isoler le code de ses dépendances avec l'injection de dépendances et les doublures de test (mocks, stubs, fakes).

Pré-requis : Séances 1-2 (qualité logicielle, premiers tests unitaires).

Durée prévue : 1h30 de cours + 2h de TP
-->

---

# Plan de la séance

<v-clicks>

1. **Correction du TP2** — Feedbacks sur les tests écrits
2. **Quand écrire les tests ?** — Les trois approches
3. **Les principes FIRST** — Ce que doit être un bon test
4. **Bonnes pratiques** — Cas limites, testing positif/négatif
5. **Le problème des dépendances** — Tester du code couplé
6. **Les doublures de test** — Dummy, Stub, Fake, Mock, Spy
7. **Approval Testing** — Vérifier par comparaison
8. **TP** — Doublures, fixtures et Test Data Builders 

</v-clicks>

<!--
On capitalise sur le TP2 : vous avez écrit vos premiers tests unitaires.
La question d'aujourd'hui est QUAND et COMMENT les écrire efficacement.
On va de l'approche la plus naïve (Test After) à la plus disciplinée (TDD).
-->

---
src: ./slides/01-quand-tester.md
---

---
src: ./slides/02-tdd.md
---

---
src: ./slides/03-first.md
---

---
src: ./slides/04-bonnes-pratiques.md
---

---
layout: section
---

# Le problème des dépendances

---
src: ./slides/05-probleme-dependances.md
---

---
src: ./slides/06-injection-dependances.md
---

---
src: ./slides/07-types-doublures.md
---

---
src: ./slides/08-nsubstitute.md
---

---
src: ./slides/09-test-fixtures.md
---

---
src: ./slides/10-approval-testing.md
---


---
layout: section
---

# Récapitulatif & TP

---
layout: center
class: text-center
---

# L'automatisation des tests au service de la qualité logicielle

<br>

Tests unitaires · Tests d'intégration · Tests e2e

TDD · FIRST · Testing positif/négatif

<br>

## Passons au TP

*Pratiquer le TDD sur un kata*

<!--
Conclusion de la séance.
On a vu les trois approches (After/First/TDD), les principes FIRST, et les bonnes pratiques.
Le TP va permettre de pratiquer le cycle TDD sur un exercice concret.
-->


---
src: ./slides/11-tp.md
---
