---
theme: seriph
title: "Tests & Validation - Séance 5"
info: |
  ## Tester du vrai code — Doublures de test et IA
  Module Tests & Validation - CNAM
class: text-center
author: Edouard Mangel
keywords: doublures, test doubles, mocks, stubs, injection de dépendances, IA, TDD
drawings:
  persist: false
transition: slide-left
mdc: true
duration: 90min
---

# Tests & Validation

## Séance 5 — Tester du vrai code

<br>

*Doublures de test, injection de dépendances et tests assistés par IA.*

<!--
Objectif de la séance : apprendre à tester du code qui a des dépendances (BDD, API, emails...) grâce à l'injection de dépendances et aux doublures de test (stubs, mocks, fakes). On termine par le rôle des tests dans le développement assisté par IA.

Pré-requis : TP2 (tests unitaires), Séance 3 (TDD, FIRST), Séance 4 (types de tests, pyramide).

Durée prévue : 1h30 de cours + 1h30 de TP
-->

---

# Plan de la séance

<v-clicks>

1. **Le problème** — Pourquoi le vrai code est difficile à tester
2. **L'injection de dépendances** — Rendre le code testable
3. **Les doublures de test** — Stubs, mocks, fakes
4. **Quoi tester ?** — Choisir ses batailles
5. **Tests et IA** — Le test comme spécification pour l'IA

</v-clicks>

<!--
On part du constat : le code que vous avez testé en TP était simple, sans dépendances externes.
En entreprise, le code dépend de bases de données, d'APIs, d'emails...
Aujourd'hui on apprend à gérer ça.
-->

---
src: ./slides/01-le-probleme.md
---

---
src: ./slides/02-injection-dependances.md
---

---
src: ./slides/03-doublures-de-test.md
---

---
src: ./slides/04-quoi-tester.md
---

---
src: ./slides/05-tests-et-ia.md
---
