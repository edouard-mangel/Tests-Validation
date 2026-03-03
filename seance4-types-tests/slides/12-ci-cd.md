# Les tests dans la CI/CD

<br>

<v-clicks>

Les tests automatisés prennent toute leur valeur dans un pipeline **CI/CD** :

```
  Push     →    Build    →    Tests     →    Tests        →    Deploy
  (git)         (compile)     unitaires      intégration       (prod)
                              ⚡ rapide       🔌 complet
```

Rien ne passe en production si les tests échouent = **quality gate**.

</v-clicks>

<v-click>

```yaml
# .github/workflows/tests.yml
name: Tests
on: [push, pull_request]
jobs:
  test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with: { dotnet-version: '8.0.x' }
      - run: dotnet test --configuration Release
```

</v-click>

<!--
La CI (Continuous Integration) exécute les tests automatiquement à chaque push.
Si un test échoue, le pipeline s'arrête et le développeur est notifié.
C'est la boucle de feedback la plus importante après les tests en local.
-->

---

# Stratégie de test en CI

<br>

<v-clicks>

### Boucle de feedback rapide

| Événement | Tests exécutés | Durée cible |
|-----------|---------------|-------------|
| **Push** | Tests unitaires | < 2 min |
| **Pull Request** | Unitaires + intégration | < 10 min |
| **Merge sur main** | Unitaires + intégration + e2e | < 20 min |

</v-clicks>

<v-click>

### Bonnes pratiques

- **Paralléliser** les tests : `dotnet test --parallel`
- **Séparer** les suites : `[Trait("Category", "Integration")]` + filtre en CI
- **Publier** les résultats : rapport JUnit, couverture dans la PR
- **Fail fast** : arrêter le pipeline dès le premier échec

</v-click>

<!--
L'objectif est d'avoir le feedback le plus rapide possible.
Les tests unitaires sont exécutés à chaque push car ils sont rapides.
Les tests d'intégration et e2e sont réservés aux PR et aux merges car ils sont plus lents.
-->

---

# Les tests flaky — L'ennemi de la CI

<br>

<v-clicks>

Un test **flaky** = un test qui **parfois passe, parfois échoue** sans changement de code.

### Causes fréquentes

| Cause | Exemple |
|-------|---------|
| **Dépendance au temps** | `Assert.Equal(DateTime.Now, ...)` |
| **État partagé** | Deux tests écrivent dans la même table |
| **Async / race condition** | `Task.Delay` + timing variable |
| **Réseau** | Appel HTTP à un service externe |
| **Ordre d'exécution** | Test A setup → Test B en dépend |

</v-clicks>

<v-click>

### Solutions

- **Isoler l'état** : chaque test crée et nettoie ses données
- **Horloge déterministe** : injecter `IClock` (vu en séance 3)
- **Retry avec marquage** : `[Trait("Flaky", "true")]` — visible, pas ignoré
- **Quarantaine** : un test flaky non corrigé est déplacé dans une suite à part

</v-click>

<v-click>

> Un test flaky qui reste flaky **érode la confiance** de toute l'équipe dans la suite de tests.

</v-click>

<!--
Les tests flaky sont le problème n°1 des suites de tests en CI.
Ils créent un "cry wolf" effect : l'équipe finit par ignorer les échecs de CI.
La règle : corriger ou quarantiner rapidement. Ne jamais laisser un test flaky polluer le pipeline.
-->
