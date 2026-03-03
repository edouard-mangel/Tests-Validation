---
theme: seriph
title: "Tests & Validation - Séance 3"
info: |
  ## Doublures de test et isolation
  Module Tests & Validation - CNAM
class: text-center
author: Edouard Mangel
keywords: test doubles, mock, stub, fake, spy, dependency injection, NSubstitute
drawings:
  persist: false
transition: slide-left
mdc: true
duration: 90min
---

# Tests & Validation

## Séance 3 — Doublures de test et isolation

<br>

*Comment tester du code qui dépend de bases de données, d'APIs ou de l'horloge système ?*

<!--
Objectif de la séance : comprendre pourquoi l'isolation est nécessaire, maîtriser l'injection de dépendances, connaître les différents types de doublures de test, et savoir utiliser NSubstitute.

Pré-requis : Séances 1-2 (qualité logicielle, premiers tests et TDD).

Durée prévue : 1h30 de cours + 2h de TP
-->

---

# Plan de la séance

<v-clicks>

1. **Le problème** — Tester du code avec des dépendances externes
2. **L'injection de dépendances** — La clé de la testabilité
3. **Les types de doublures** — Dummy, Stub, Fake, Mock, Spy
4. **NSubstitute** — Un framework de mocking pour .NET
5. **Fixtures** — Partager le setup entre tests
6. **Approval Testing** — Vérifier par comparaison avec Verify
7. **TP** — Doublures, fixtures et Test Data Builders

</v-clicks>

<!--
Jusqu'ici on a testé des fonctions pures avec le TDD.
Aujourd'hui on s'attaque au problème réel : comment tester du code qui parle à des systèmes externes ?
-->

---
src: ./slides/01-probleme-dependances.md
---

---
src: ./slides/02-injection-dependances.md
---

---
src: ./slides/03-types-doublures.md
---

---
src: ./slides/04-nsubstitute.md
---

---
src: ./slides/05-test-fixtures.md
---

---
src: ./slides/06-approval-testing.md
---
