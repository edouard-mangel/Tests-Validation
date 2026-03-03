# TP5 - Notes enseignant

## Intention pedagogique

Ce TP developpe le **sens critique** des etudiants face aux tests. Apres 3 TPs ou ils ont appris a ecrire des tests, celui-ci leur apprend a reconnaitre les **mauvais** tests — et surtout a comprendre pourquoi ils sont mauvais.

La progression est deliberee :
1. **Observer** les smells (exercice 1) — developper l'oeil critique
2. **Capturer** le comportement reel (exercice 2) — avant de toucher aux tests, comprendre le code
3. **Reecrire** avec qualite (exercice 3) — appliquer tout ce qu'ils ont appris
4. **Mesurer** avec Stryker (exercice 4) — avoir une preuve objective que les nouveaux tests sont meilleurs

## Leçons cles a faire retenir

1. **Un test qui passe ne prouve rien s'il ne peut pas echouer** — `Assert.True(true)` et `Assert.NotNull(result.ToString())` passent toujours. Ce ne sont pas des tests.

2. **La logique dans le test est dangereuse** — si le test recalcule la formule (`prices.Sum() * (1 - 10m / 100m) * 1.08m`), il peut contenir le meme bug que le code de production. La valeur attendue doit etre une constante calculee a la main.

3. **Les characterization tests capturent le comportement, pas la specification** — meme si le resultat semble faux (TVA a 8% au lieu de 20%), on l'ecrit tel quel. Le characterization test est un **filet de securite**, pas une validation metier.

4. **Le mutation score est une meilleure metrique que le code coverage** — un test peut couvrir une ligne sans verifier son resultat. Stryker modifie le code et verifie que le test detecte le changement.

5. **Nommer un test, c'est documenter un comportement** — `Test1` ne dit rien. `CalculateTotal_PremiumCustomer_Applies10PercentDiscount` est une specification executable.

## Deroulement conseille

| Duree | Activite |
|-------|----------|
| 5 min | Lecture du code LegacyPricingService + suite de tests |
| 20 min | Exercice 1 — identifier les smells |
| 10 min | Debrief collectif sur les smells trouves |
| 30 min | Exercice 2 — characterization tests |
| 25 min | Exercice 3 — reecriture des tests |
| 15 min | Exercice 4 — Stryker avant/apres |
| 5 min | Debrief Stryker |
| 30 min | Formation des groupes pour le projet |

## Inventaire des smells dans la suite de tests

| Test | Smell | Explication |
|------|-------|-------------|
| `Test1` | Assertion tautologique + nom vague | `Assert.True(true)` passe toujours. Le nom ne dit rien. |
| `Test2` | Assertion tautologique + nom vague | `Assert.NotNull(result.ToString())` — un decimal n'est jamais null apres `.ToString()`. |
| `TestCalculation` | Logique dans le test | Le test recalcule la formule — s'il y a un bug dans la formule, le test le reproduit. |
| `TestCalculation2` | Test redondant | Identique a `TestCalculation`. |
| `TestCalculation3` | Test redondant | Quasi-identique (int au lieu de decimal pour le discount, mais le comportement est le meme). |
| `TestPremium` | Assertion trop faible | `Assert.True(result > 0)` ne verifie pas la valeur. N'importe quel nombre positif passe. |
| `TestDependsOnPrevious` | Tests interdependants | Depend de l'etat laisse par `TestPremium`. Si l'ordre d'execution change, le test echoue. |
| `TestInternalMethod` | Test via reflection | Appelle une methode publique via reflection — complexite inutile. Si la methode etait privee, ce serait un test de methode privee. |
| `Test_PricesArePositive` | Assertion sur les donnees de test | Teste que les prix de test sont positifs, pas que le service fonctionne. |
| `TestDiscount_ChecksMockCallOrder` | Test d'implementation | Verifie la coherence de l'etat interne (`GetLastTotal()`), pas le resultat du calcul. |
| `TestEmpty` / `TestNull` / `TestSingleItem` | Corrects | Ce sont les seuls tests potables. Les noms sont un peu vagues mais le contenu est bon. |

Les etudiants doivent trouver **au moins 5** de ces smells. Si un etudiant en trouve 8+, c'est excellent.

## Points d'attention par exercice

### Exercice 2 — Characterization tests

Le piege principal : les etudiants veulent ecrire le resultat "correct" au lieu du resultat **reel**. Par exemple, pour un montant de 800 avec 0% de remise :
- Resultat reel : `800 * 1.08 = 864` (taxe a 8%)
- Resultat "correct" : `800 * 1.20 = 960` (TVA a 20%)

Insister : le characterization test capture le **comportement actuel**, pas le comportement souhaite. Le commentaire `// bug probable : TVA a 8% au lieu de 20%` est la pour documenter le doute, pas pour le corriger.

Les combinaisons a couvrir absolument :
- Seuil de taxe : montant final <= 1000 (8%) vs > 1000 (20%) — c'est probablement un bug
- Remise fidelite : 0-1 an (0%), 2-4 ans (2%), 5+ ans (5%)
- Remise premium : 10%
- Remise volume : < 5 items (0%), 5-9 items (8%), 10+ items (15%)

### Exercice 3 — Reecriture

Le critere le plus difficile a faire respecter : **pas de logique dans les tests**. Les etudiants ont tendance a ecrire `Assert.Equal(100m * 0.90m * 1.08m, result)` au lieu de `Assert.Equal(97.20m, result)`. Insister : la valeur attendue est une constante. Si le calcul est complexe, le faire sur papier ou a la calculatrice.

### Exercice 4 — Stryker

L'installation de Stryker peut prendre quelques minutes. Prevoir ce temps.

Le mutation score attendu :
- Tests originaux : ~30-50% — beaucoup de mutants survivent car les assertions sont trop faibles
- Tests reecrits : ~80-90% — les mutants restants sont souvent sur les arrondis ou les cas limites exacts

Mutants survivants typiques :
- Changer `>=` en `>` dans `loyaltyYears >= 5` — survit si aucun test n'utilise exactement 5 ans
- Changer `0.95m` en `0.96m` — survit si le test verifie `result > 0` au lieu d'une valeur exacte
- Supprimer `Math.Round(total, 2)` — survit si toutes les valeurs de test tombent juste

## FAQ anticipee

### "Le bug de TVA a 8%, on le corrige ?"
Pas dans les exercices 1 et 2 — le code de production ne doit pas etre modifie. A l'exercice 3, les etudiants ecrivent des tests qui **documentent** le comportement actuel. Si on veut corriger le bug ensuite, les characterization tests servent de filet de securite.

### "Mon mutation score est a 100%, c'est possible ?"
Tres improbable avec ce code. Verifier que Stryker analyse bien le bon projet et que les mutants sont generes. Si c'est vrai, bravo — mais en general ca signifie que le projet de mutation n'est pas configure correctement.

### "Stryker est tres lent."
Stryker execute les tests pour chaque mutant. Avec 50 mutants et 15 tests, ca fait 750 executions. C'est normal que ca prenne 1-2 minutes. Pour les gros projets, on configure Stryker pour ne muter qu'un sous-ensemble de fichiers.

### "C'est quoi un mutant equivalent ?"
Un mutant equivalent est une modification du code qui ne change pas le comportement observable. Par exemple, remplacer `i < n` par `i != n` dans une boucle qui va de 0 a n-1. Aucun test ne peut le tuer car le comportement est identique. Ces mutants sont rares mais existent.

### "Les characterization tests et les tests reecrits doivent-ils coexister ?"
Pendant l'exercice, oui — les characterization tests sont le filet de securite. En production, une fois qu'on est confiant dans les nouveaux tests, on peut retirer les characterization tests (ou les garder comme documentation).

## Pieges courants

- **Etat partage mutable** — la suite originale utilise `static LegacyPricingService _service = new()`. Les etudiants qui ne le remarquent pas reproduisent le meme probleme dans leur reecriture. Chaque test doit creer sa propre instance.
- **Confondre "le test passe" et "le test est bon"** — c'est le theme central du TP. Repeter si necessaire.
- **Ne pas installer Stryker en avance** — `dotnet tool install -g dotnet-stryker` telecharge ~50 Mo. Si tout le monde le fait en meme temps sur le wifi de l'amphi, ca peut prendre du temps. Prevenir les etudiants de l'installer pendant la pause.
- **Stryker ne trouve pas le projet** — il faut lancer `dotnet stryker` depuis le dossier du projet de test, pas depuis la racine de la solution. Ou utiliser `--project` pour specifier le chemin.
