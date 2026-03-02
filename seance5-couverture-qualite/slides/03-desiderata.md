# Les Test Desiderata de Kent Beck

<br>

Au-delà de FIRST, Kent Beck a identifié **12 propriétés** que les tests devraient posséder.

Aucun test ne les a toutes. L'intérêt est de **savoir quels compromis on fait**.

<!--
Les Test Desiderata sont une évolution de FIRST — plus riches, plus nuancées.
On les a en séance 2 sous forme simplifiée (FIRST). Maintenant on passe au modèle complet.
Source : Kent Beck, "Test Desiderata" (2019), https://kentbeck.github.io/TestDesiderata/
-->

---
zoom: 0.85
---

# Les 12 propriétés

<br>

<v-clicks>

| Propriété | Signification |
|-----------|---------------|
| **Isolated** | Chaque test est indépendant des autres |
| **Composable** | On peut tester des dimensions séparément et les combiner |
| **Deterministic** | Même résultat à chaque exécution |
| **Fast** | S'exécute rapidement |
| **Writable** | Facile à écrire |
| **Readable** | Facile à lire et comprendre |
| **Behavioral** | Sensible aux changements de *comportement* |
| **Structure-insensitive** | Insensible aux changements de *structure* |
| **Automated** | S'exécute sans intervention humaine |
| **Specific** | Quand un test échoue, la cause est évidente |
| **Predictive** | Si les tests passent, le code fonctionne en production |
| **Inspiring** | Les tests verts donnent confiance |

</v-clicks>

<!--
Beck insiste : aucun test n'aura les 12. C'est un espace de compromis.
Un test unitaire sera fast, isolated, deterministic, mais peut-être pas predictive (il ne garantit pas que ça marche en prod).
Un test e2e sera predictive et inspiring, mais ni fast ni isolated.
-->

---

# Behavioral vs Structure-insensitive

<br>

C'est la paire la plus importante — et la plus subtile :

<v-clicks>

**Behavioral** = le test doit **casser** quand le comportement change

```csharp
// ✅ Behavioral : si le calcul du prix change, ce test casse
Assert.Equal(80m, order.FinalTotal);
```

**Structure-insensitive** = le test ne doit **pas casser** quand on refactore l'intérieur

```csharp
// ❌ Structure-sensitive : si on renomme une méthode interne, ce test casse
mockService.Received(1).ApplyDiscount(Arg.Any<Order>());
```

</v-clicks>

<v-click>

<br>

> Un bon test est **sensible au quoi** (comportement) et **insensible au comment** (implémentation).

C'est exactement la différence entre tester le **résultat** et tester les **interactions** qu'on a vue en séance 3.

</v-click>

<!--
C'est le même message que "tester le comportement, pas l'implémentation" — mais formulé de façon plus précise.
Les mocks rendent les tests structure-sensitive. Les assertions sur les résultats les rendent behavioral.
Beck considère ces deux propriétés comme les plus en tension l'une avec l'autre.
-->

---

# Les compromis en pratique

<br>

<v-clicks>

Chaque **type de test** fait des compromis différents sur les desiderata :

| Propriété | Test unitaire | Test d'intégration | Test e2e |
|-----------|:---:|:---:|:---:|
| **Fast** | ✅✅ | ✅ | ❌ |
| **Isolated** | ✅✅ | ✅ | ❌ |
| **Deterministic** | ✅✅ | ✅ | ⚠️ |
| **Predictive** | ⚠️ | ✅ | ✅✅ |
| **Inspiring** | ⚠️ | ✅ | ✅✅ |
| **Structure-insensitive** | ⚠️ | ✅ | ✅✅ |
| **Specific** | ✅✅ | ✅ | ❌ |

</v-clicks>

<v-click>

<br>

Aucun type de test ne "gagne" partout. C'est pour ça qu'on a besoin de **plusieurs niveaux**.

</v-click>

<!--
Ce tableau synthétise tout ce qu'on a vu depuis la séance 2 : pourquoi on a besoin de tests unitaires ET d'intégration ET e2e.
Les desiderata rendent explicites les compromis qu'on fait en choisissant un niveau de test.
Relier à la pyramide des tests vue en séance 4.
-->

---

# FIRST → Desiderata : la progression

<br>

<v-clicks>

En séance 2, on a vu **FIRST** — 5 propriétés pour démarrer :

| FIRST | Desiderata correspondants |
|-------|---------------------------|
| **F**ast | Fast |
| **I**solated | Isolated |
| **R**epeatable | Deterministic |
| **S**elf-validating | Automated + Specific |
| **T**imely | *(pas de correspondance directe)* |

Les desiderata ajoutent des dimensions que FIRST ne couvre pas :

- **Behavioral** / **Structure-insensitive** → la qualité du *quoi* on teste
- **Readable** / **Writable** → l'ergonomie des tests
- **Predictive** / **Inspiring** → la *confiance* que les tests apportent
- **Composable** → la capacité à combiner des dimensions de test

</v-clicks>

<!--
FIRST est un bon point de départ pour les débutants. Les desiderata sont le modèle complet.
La transition FIRST → desiderata reflète la progression du module : on commence simple, on affine.
L'important n'est pas de mémoriser les 12 propriétés, mais de savoir que chaque test est un compromis.
-->
