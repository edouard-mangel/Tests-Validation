# Tests de mutation

<br>

<v-clicks>

Si la couverture mesure "le code est-il exécuté ?", les tests de mutation mesurent :

> **"Si je casse le code, les tests le détectent-ils ?"**

Le principe :

1. On **modifie** le code source (mutation)
2. On **relance** les tests
3. Si un test **échoue** → le mutant est **tué** ✅ (le test détecte le bug)
4. Si tous les tests **passent** → le mutant **survit** ❌ (les tests sont insuffisants)

</v-clicks>

<!--
Les tests de mutation sont le meilleur indicateur de la qualité des tests.
Un test qui ne détecte pas une modification du code est un test qui ne sert à rien.
-->

---

# Exemples de mutations

<br>

<v-clicks>

| Code original | Mutation | Type |
|--------------|----------|------|
| `if (age >= 18)` | `if (age > 18)` | Changement d'opérateur |
| `return price * 0.8m` | `return price * 1.2m` | Changement de constante |
| `if (user != null)` | `if (true)` | Suppression de condition |
| `list.Add(item)` | *(supprimé)* | Suppression d'instruction |
| `return a + b` | `return a - b` | Changement d'opérateur |

</v-clicks>

<v-click>

<br>

Chaque mutation simule un **bug réel** que les tests devraient attraper.

</v-click>

<!--
Les mutations sont des modifications automatiques du code.
Chaque mutation correspond à un type de bug courant.
Si un test ne détecte pas la mutation, ça veut dire qu'on pourrait introduire ce bug sans que personne ne le remarque.
-->

---

# Stryker.NET — Tests de mutation pour .NET

<br>

```bash
# Installation
dotnet tool install -g dotnet-stryker

# Exécution
dotnet stryker
```

<v-click>

Résultat typique :

```
All mutants have been tested, and your mutation score has been calculated.
┌─────────────────┬──────────┬──────────┬───────────┬──────────────┐
│ File             │ Mutants  │ Killed   │ Survived  │ Score        │
├─────────────────┼──────────┼──────────┼───────────┼──────────────┤
│ Calculator.cs    │ 12       │ 11       │ 1         │ 91.7%        │
│ OrderService.cs  │ 28       │ 18       │ 10        │ 64.3%        │
│ Validator.cs     │ 8        │ 8        │ 0         │ 100.0%       │
├─────────────────┼──────────┼──────────┼───────────┼──────────────┤
│ Total            │ 48       │ 37       │ 11        │ 77.1%        │
└─────────────────┴──────────┴──────────┴───────────┴──────────────┘
```

</v-click>

<v-click>

**Mutation score** = pourcentage de mutants tués. Plus c'est haut, meilleure est la qualité des tests.

</v-click>

<!--
Stryker est l'outil de référence pour les tests de mutation en .NET.
Il modifie le code, relance les tests, et produit un rapport.
Un mutation score de 77% signifie que 23% des mutations ne sont pas détectées → les tests ont des trous.
-->

---

# Couverture vs Mutation

<br>

<v-clicks>

| | Couverture | Tests de mutation |
|---|-----------|-------------------|
| **Mesure** | Code exécuté | Bugs détectés |
| **Question** | "Mon code est-il traversé ?" | "Mes tests détectent-ils les bugs ?" |
| **Faux positifs** | Oui (couvert ≠ testé) | Très peu |
| **Vitesse** | Rapide | Lent (relance N fois les tests) |
| **Quand l'utiliser** | À chaque commit (CI) | Ponctuellement (revue de qualité) |

</v-clicks>

<v-click>

<br>

### L'angle IA

Une couverture élevée seule ne veut rien dire — ce qui compte, c'est que les tests **détectent les vrais bugs**, y compris ceux introduits par l'IA.

Les tests de mutation sont la meilleure façon de le vérifier.

</v-click>

<!--
La couverture est un outil quotidien, les tests de mutation sont un outil de diagnostic.
Pour le projet de groupe, on regardera la couverture ET le mutation score.
-->
