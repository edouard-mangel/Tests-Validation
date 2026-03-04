---
theme: ../theme
title: "Tests & Validation - Séance 2"
info: |
  ## Tests unitaires avec xUnit
  Module Tests & Validation - CNAM
class: text-center
author: Edouard Mangel
keywords: tests unitaires, xUnit, assertions, AAA, InlineData, Theory, Fact, ITestOutputHelper
drawings:
  persist: false
transition: slide-left
mdc: true
duration: 90min
---

# Tests & Validation

## Séance 2 — Tests unitaires avec xUnit

<br>

*De la théorie à la pratique : écrire, organiser et paramétrer ses premiers tests.*

<!--
Objectif de la séance : savoir créer un projet de tests xUnit, écrire des tests avec le pattern AAA, utiliser les assertions, organiser les tests et les paramétrer avec InlineData/ClassData.

Pré-requis : Séance 1 (qualité logicielle, métriques).
Durée prévue : 1h30 de cours + 2h de TP
-->

---

# Plan de la séance

<v-clicks>

1. **Notions de base** — Boîte blanche, boîte noire
2. **Mise en place** — Créer un projet xUnit dans Visual Studio
3. **Le pattern AAA** — Arrange / Act / Assert
4. **Les assertions** — Tout ce qu'on peut vérifier avec xUnit
5. **Organiser ses tests** — Traits, groupes, tests ignorés
6. **Output et diagnostic** — Afficher du texte dans les tests
7. **Tests paramétriques** — InlineData, MemberData, ClassData

</v-clicks>

<!--
Présenter le déroulé. On commence par les concepts, puis on passe directement à la pratique.
La séance est très orientée "hands-on" : les étudiants ont Visual Studio ouvert en parallèle.
-->

---
layout: section
---

# Notions de base

---
src: ./slides/01-notions.md
---

---
layout: section
---

# Mise en place

---
src: ./slides/02-mise-en-place.md
---

---
layout: section
---

# Le pattern Arrange / Act / Assert

---
src: ./slides/03-aaa.md
---

---
layout: section
---

# Les assertions

---
src: ./slides/04-assertions.md
---

---
layout: section
---

# Organiser ses tests

---
src: ./slides/05-organisation.md
---

---
layout: section
---

# Output et diagnostic

---
src: ./slides/06-output.md
---

---
layout: section
---

# Tests paramétriques

---
src: ./slides/07-parametric.md
---

---
layout: center
class: text-center
---

# À vous de jouer !

<br>

**TP2 — Tests unitaires : crédit immobilier**

<br>

*Écrire des tests unitaires pour un calculateur de crédit immobilier*
