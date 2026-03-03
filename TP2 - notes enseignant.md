# TP2 - Notes enseignant

## Intention pedagogique

Ce TP est la **premiere confrontation** des etudiants avec les tests unitaires. L'objectif n'est pas seulement d'apprendre la syntaxe xUnit, mais de construire les reflexes fondamentaux :

- **Lire un message d'erreur de test** — c'est une competence a part entiere. Beaucoup d'etudiants ne lisent pas le message et cherchent le bug a l'aveugle.
- **Trouver un bug avec un test** — le StringCalculator contient un off-by-one (`i = 1` au lieu de `i = 0`). C'est volontaire : les etudiants ecrivent des tests, voient du rouge, et comprennent que le test a trouve le bug avant eux.
- **Passer de "ca marche" a "c'est prouve"** — le PasswordValidator force a penser aux cas limites et a la couverture des regles.

## Leçons cles a faire retenir

1. **Un test qui passe n'est utile que s'il peut echouer** — si on change le code et que le test passe toujours, il ne teste rien.
2. **Arrange / Act / Assert** — ce pattern structure la pensee, pas juste le code.
3. **`[Theory]` vs `[Fact]`** — `Theory` n'est pas "mieux", c'est adapte quand le comportement est le meme avec des donnees differentes. Si la logique du test change, garder des `Fact` separes.
4. **Tester les cas limites** — chaine vide, null, exactement a la frontiere (8 caracteres). C'est la que les bugs se cachent.
5. **Ecrire le test avant le code** (etape 7 du StringCalculator) — premiere introduction au TDD, sans le nommer.

## Deroulement conseille

| Duree | Activite |
|-------|----------|
| 5 min | Mise en place du projet, verification que `dotnet test` fonctionne |
| 25 min | Exercice 1 — StringCalculator (etapes 1 a 5) |
| 10 min | Exercice 1 — etapes 6 et 7 (exception + delimiteur) |
| 25 min | Exercice 2 — PasswordValidator (etapes 1 a 4) |
| 15 min | Exercice 2 — etapes 5 et 6 (ValidationResult + caractere special) |
| 10 min | Debriefing collectif |

## FAQ anticipee

### "Mon test passe alors que le code a un bug ?"
C'est le cas du test `Add_EmptyString_ReturnsZero` — la chaine vide est geree par le `if (numbers.Length == 0)` avant d'atteindre le bug de la boucle. Expliquer que c'est pour ca qu'il faut **plusieurs tests** : un seul test ne suffit pas a prouver que le code est correct.

### "Pourquoi `i = 1` est un bug ? Ca marche pour la chaine vide."
L'index de `Split` commence a 0, pas a 1. Avec `"5"`, `parts[1]` lance une `IndexOutOfRangeException`. Avec `"1,2"`, `parts[1]` vaut `"2"` et on ignore `parts[0]` qui vaut `"1"`. Faire dessiner le tableau d'indices au tableau si necessaire.

### "Est-ce que je dois tester toutes les combinaisons possibles ?"
Non. On teste les **classes d'equivalence** et les **frontieres**. Pour le PasswordValidator : un mot de passe de 7 caracteres et un de 8 suffisent pour la regle de longueur. Tester 6, 7, 8, 9, 10, 100 n'apporte rien de plus.

### "Pourquoi `Assert.Equal(expected, actual)` et pas l'inverse ?"
C'est une convention xUnit. Si on inverse, le message d'erreur dit "Expected: 3, Actual: 0" a l'envers, ce qui est confus. Insister sur l'ordre.

### "`[InlineData]` ne supporte pas les objets complexes ?"
Correct, `[InlineData]` n'accepte que des constantes de compilation (string, int, bool, etc.). Pour des objets complexes, on utilise `[MemberData]` ou `[ClassData]`, mais ce n'est pas l'objet de ce TP.

### "Mon test `null` plante avec NullReferenceException, pas avec le bon resultat."
C'est possible si l'etudiant n'a pas le `if (numbers == null)` dans sa version corrigee. Rappeler qu'il faut gerer le null **avant** d'appeler `.Length` ou `.Split()`.

### "Pourquoi le test des espaces `"1, 2, 3"` echoue ?"
`int.Parse(" 2")` fonctionne en C# (Parse ignore les espaces), mais `int.Parse(" 2 ")` aussi. Si ca echoue, c'est que l'implementation fait autre chose (ex: `Trim()` manquant sur un format different). Verifier le code de l'etudiant.

## Pieges courants

- **Oublier de creer le projet de production** — les etudiants creent le projet de test mais pas le projet contenant `StringCalculator`. S'assurer que la structure `TestingTP2` + `TestingTP2.Tests` est claire.
- **Ne pas lancer les tests** — certains etudiants ecrivent tous les tests d'un coup et les lancent a la fin. Insister sur le cycle : ecrire un test, lancer, observer, corriger.
- **Copier-coller le test de l'exemple sans le comprendre** — demander a chaque etudiant d'expliquer ce que teste son test en une phrase.
- **Confondre `Assert.True(result)` et `Assert.Equal(expected, result)`** — `Assert.True` donne un message d'erreur inutile ("Expected: True, Actual: False"). Toujours preferer `Assert.Equal` quand on compare des valeurs.
