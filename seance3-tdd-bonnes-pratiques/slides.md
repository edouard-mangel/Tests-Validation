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

1. **Quand écrire les tests ?** — Les trois approches
2. **Test-Driven Development** — Le cycle Rouge/Vert/Refactor
3. **Les principes FIRST** — Ce que doit être un bon test
4. **Bonnes pratiques** — Cas limites, testing positif/négatif
5. **Le problème des dépendances** — Tester du code couplé
6. **Injection de dépendances** — La clé de la testabilité
7. **Les doublures de test** — Dummy, Stub, Fake, Mock, Spy
8. **NSubstitute** — Mocking framework pour .NET
9. **Test Fixtures** — Partager le setup entre tests
10. **Approval Testing** — Vérifier par comparaison
11. **TP** — Doublures, fixtures et Test Data Builders

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
