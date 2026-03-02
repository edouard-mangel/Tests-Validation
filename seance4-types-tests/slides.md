---
theme: seriph
title: "Tests & Validation - Séance 4"
info: |
  ## Les types de tests utiles au développeur
  Module Tests & Validation - CNAM
class: text-center
author: Edouard Mangel
keywords: tests, unitaires, intégration, e2e, pyramide des tests
drawings:
  persist: false
transition: slide-left
mdc: true
duration: 90min
---

# Tests & Validation

## Séance 4 — Les types de tests utiles au développeur

<br>

*Quels tests existent et à quoi servent-ils ?*

<!--
Objectif de la séance : comprendre les différents niveaux de tests (e2e, intégration, unitaire), savoir quand utiliser chacun, et comprendre la différence entre tester l'implémentation et tester le comportement.

Pré-requis : TP2 (tests unitaires) et Séance 3 (TDD, FIRST). On élargit maintenant la perspective au-delà des tests unitaires.

Durée prévue : 1h30 de cours + 1h30 de TP
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
7. **TP** — Écrire ses premiers tests

</v-clicks>

<!--
Présenter le déroulé. On commence par le "pourquoi", puis on descend la pyramide du plus macro au plus micro.
La séance se termine sur les tests unitaires — que vous maîtrisez déjà grâce au TP2 et à la séance sur le TDD.
-->

---
src: ./slides/01-definitions.md
---

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
