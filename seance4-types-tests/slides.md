---
theme: ../theme
title: "Tests & Validation - Séance 4"
info: |
  ## Types de tests, intégration et stratégie
  Module Tests & Validation - CNAM
class: text-center
author: Edouard Mangel
keywords: tests, unitaires, intégration, e2e, pyramide des tests, testcontainers, contract testing, CI/CD
drawings:
  persist: false
transition: slide-left
mdc: true
duration: 90min
---

# Tests & Validation

## Séance 4 — Types de tests, intégration et stratégie

<br>

*Des tests unitaires aux tests d'intégration — comment tester la collaboration entre composants réels.*

<!--
Objectif de la séance : replacer les tests unitaires dans le cadre global (types de tests, pyramide), comprendre les limites des TU seuls, maîtriser les tests d'intégration (repository, API), et savoir choisir le bon niveau de test.

Pré-requis : Séances 1-3 (qualité logicielle, TDD, doublures de test).

Durée prévue : 1h30 de cours + 2h de TP
-->

---

# Plan de la séance

<v-clicks>

1. **Qu'est-ce que tester ?** — Définitions de base
2. **Pourquoi automatiser les tests ?** — Le retour sur investissement
3. **La pyramide des tests** — Vue d'ensemble
4. **Les tests end-to-end** — Au plus près de l'utilisateur
5. **Les tests d'intégration** — Vérifier les connexions
6. **Les tests unitaires** — Le cœur de la stratégie
7. **Les limites des tests unitaires** — Ce qu'ils ne voient pas
8. **Tests d'intégration avancés** — Repository, API, WebApplicationFactory
9. **Les outils** — InMemory, SQLite, Testcontainers
10. **Contract Testing** — Pact et les contrats entre services
11. **Property-Based Testing** — Tester des propriétés avec FsCheck
12. **CI/CD et tests flaky** — Les tests dans le pipeline
13. **Choisir le bon niveau** — La pyramide comme outil de décision
14. **TP** — Tests d'intégration sur une Web API ASP.NET

</v-clicks>

<!--
Présenter le déroulé. On commence par le "pourquoi", puis on descend la pyramide du plus macro au plus micro.
La séance se termine sur les tests unitaires — que vous maîtrisez déjà grâce au TP2 et à la séance sur le TDD.
-->


---
src: ./slides/02-pourquoi-tester.md
---

---
src: ./slides/03-pyramide.md
---

---
src: ./slides/04-tests-e2e.md
---

---
src: ./slides/05-tests-integration.md
---

---
src: ./slides/06-tests-unitaires.md
---

---
src: ./slides/07-limites-unitaires.md
---

---
src: ./slides/08-tests-integration.md
---

---
src: ./slides/09-outils.md
---

---
src: ./slides/10-contract-testing.md
---

---
src: ./slides/11-property-testing.md
---

---
src: ./slides/12-ci-cd.md
---

---
src: ./slides/13-strategie.md
---
