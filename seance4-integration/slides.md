---
theme: seriph
title: "Tests & Validation - Séance 4"
info: |
  ## Tests d'intégration et stratégie de test
  Module Tests & Validation - CNAM
class: text-center
author: Edouard Mangel
keywords: tests intégration, WebApplicationFactory, testcontainers, stratégie de test, pyramide des tests
drawings:
  persist: false
transition: slide-left
mdc: true
duration: 90min
---

# Tests & Validation

## Séance 4 — Tests d'intégration et stratégie de test

<br>

*Les tests unitaires ne suffisent pas — comment tester la collaboration entre composants réels ?*

<!--
Objectif de la séance : replacer les tests unitaires dans le cadre global (types de tests, pyramide), comprendre les limites des TU seuls, maîtriser les tests d'intégration (repository, API), et savoir choisir le bon niveau de test.

Pré-requis : Séances 1-3 (qualité logicielle, TDD, doublures de test).

Durée prévue : 1h30 de cours + 2h de TP
-->

---

# Plan de la séance

<v-clicks>

1. **Les types de tests** — Unitaire, intégration, e2e : vue d'ensemble
2. **Les limites des tests unitaires** — Tout passe, mais le système est cassé
3. **Les tests d'intégration** — Repository, API, WebApplicationFactory
4. **Les outils** — InMemory, SQLite, testcontainers, e2e
5. **Contract Testing** — Pact et les contrats entre services
6. **Property-Based Testing** — Tester des propriétés avec FsCheck
7. **CI/CD et tests flaky** — Les tests dans le pipeline
8. **Choisir le bon niveau** — La pyramide comme outil de décision
9. **TP** — Tests d'intégration sur une Web API ASP.NET

</v-clicks>

<!--
On a passé 2 séances sur les tests unitaires et l'isolation. Aujourd'hui on élargit la perspective.
Pourquoi automatiser, quels types de tests existent, et surtout : quand utiliser chacun.
-->

---
src: ./slides/01-types-et-strategie.md
---

---
src: ./slides/02-limites-unitaires.md
---

---
src: ./slides/03-tests-integration.md
---

---
src: ./slides/04-outils.md
---

---
src: ./slides/05-contract-testing.md
---

---
src: ./slides/06-property-testing.md
---

---
src: ./slides/07-ci-cd.md
---

---
src: ./slides/08-strategie.md
---
