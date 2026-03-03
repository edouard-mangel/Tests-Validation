# TP3 - Notes enseignant

## Intention pedagogique

Ce TP construit une conviction par l'experience : **les doublures manuelles (fake, spy, stub) sont superieures aux frameworks de mocking dans la majorite des cas**. Ce n'est pas un dogme impose — les etudiants le constatent eux-memes en ecrivant les deux versions et en observant ce qui casse.

L'exercice 2 (ReportGenerator) attaque un probleme reel : heriter de code non testable et le rendre testable. C'est la situation la plus frequente en entreprise.

## Leçons cles a faire retenir

1. **Fake vs Spy vs Stub** — trois roles distincts, pas trois noms pour la meme chose.
   - Fake = implementation simplifiee (FakeUserRepository avec une List)
   - Spy = enregistre les appels pour verification apres coup (SpyEmailSender)
   - Stub = retourne des valeurs fixes (StubClock)

2. **Le spy capture des donnees, `.Received()` verifie des appels** — la difference est fondamentale. Le spy permet d'inspecter librement (`Assert.Contains`, `Assert.Equal` sur n'importe quel champ). `.Received()` est binaire : l'appel a eu lieu ou non, avec ces arguments exacts ou non.

3. **`Arg.Any<>()` est un piege** — il rend le test moins fragile en apparence, mais il peut masquer des regressions reelles. Les etudiants le decouvrent a l'etape 5b quand le spy attrape la regression et NSubstitute la rate.

4. **L'injection de dependances rend le code testable** — l'exercice 2 le montre par la transformation d'un code couple en code testable.

5. **Le Test Data Builder reduit le bruit** — il met en avant ce qui compte dans le test et cache les valeurs par defaut.

## Deroulement conseille

| Duree | Activite |
|-------|----------|
| 5 min | Mise en place, lecture du code NotificationService |
| 30 min | Exercice 1 — etapes 1 a 4 (doublures manuelles) |
| 15 min | Exercice 1 — etape 5 (NSubstitute + decouverte des limites) |
| 5 min | Debrief collectif sur spy vs mock |
| 25 min | Exercice 2 — ReportGenerator (refactoring + tests) |
| 15 min | Exercice 3 — Test Data Builder |
| 15 min | Exercice 4 — IClassFixture |
| 10 min | Debriefing final |

## Points d'attention pour l'etape 5

### 5a — Refactoring du format du sujet

Les etudiants changent le format du sujet dans `NotifyUser`. Ce qui se passe :

- **Tests spy** : survivent en general, car les assertions portent sur `emailSpy.SentEmails[0].Body` (le body contient le message), pas sur le subject. Sauf si un etudiant a ecrit une assertion sur le subject — auquel cas son test casse aussi, et c'est un bon moment pour discuter de quoi asserter.
- **Tests NSubstitute** : si l'etudiant a ecrit `.Received(1).Send("alice@test.com", "Notification du 15/06/2025", "Bienvenue !")`, le test casse car le sujet a change. Meme si le **comportement** (envoyer un email avec le bon message) n'a pas change.

Message cle : le test NSubstitute verifie **comment** le service appelle sa dependance. Le spy verifie **ce qui a ete envoye**. La distinction est subtile mais cruciale.

### 5b — Evolution avec formule de politesse

C'est la partie la plus importante du TP. Les etudiants pensent faire une evolution normale (ajouter une salutation au body). Le code fourni perd le `message` :

```csharp
var body = $"Bonjour {user.Name},\n\nCordialement,\nL'equipe";
```

Le `message` passe en parametre a disparu. C'est une regression classique (oubli lors d'un refactoring).

- **Le spy l'attrape** : `Assert.Contains("Bienvenue !", emailSpy.SentEmails[0].Body)` echoue car le body ne contient plus le message.
- **NSubstitute avec `Arg.Any<string>()` la rate** : le test passe car `Arg.Any<string>()` accepte n'importe quel body, y compris un body sans le message.

**Ne pas reveler l'astuce a l'avance.** Laisser les etudiants executer le code et constater la difference. La question "Un des deux suites detecte un probleme. Lequel, et pourquoi l'autre ne le detecte pas ?" est volontairement ouverte.

Si un etudiant a utilise des arguments exacts dans `.Received()` au lieu de `Arg.Any<>()`, son test NSubstitute cassera aussi — c'est bien, mais pour la mauvaise raison (il verifie le body exact, pas le contenu semantique). Faire la distinction.

## FAQ anticipee

### "Pourquoi ne pas toujours utiliser NSubstitute ? C'est moins de code."
C'est vrai que NSubstitute demande moins de classes a ecrire. Mais les doublures manuelles sont reutilisables, lisibles, et testent le **resultat** plutot que l'**implementation**. En pratique, un `FakeUserRepository` s'ecrit une fois et sert dans des dizaines de tests. Le gain de NSubstitute est illusoire.

### "Le spy et le mock font la meme chose, non ?"
Non. Le spy **capture** les donnees et laisse le test decider quoi verifier. Le mock **verifie** les appels au moment ou ils sont faits. C'est la difference entre prendre une photo et installer une camera de surveillance.

### "Pourquoi ecrire un StubClock au lieu de mocker DateTime ?"
Parce que `DateTime.Now` est une methode statique — on ne peut pas la mocker sans framework special. L'interface `IClock` rend la dependance explicite et injectable. C'est un pattern fondamental pour rendre le code testable.

### "Dans l'exercice 2, pourquoi le code original est-il dangereux ?"
La requete SQL utilise l'interpolation de chaine : `$"SELECT * FROM ... WHERE Name = '{reportName}'"`. C'est une **injection SQL**. Si un etudiant ne le remarque pas, le signaler au debrief — ca montre que le refactoring pour la testabilite ameliore aussi la securite.

### "IClassFixture partage l'instance entre les tests. Ca ne pose pas de probleme ?"
Si ! C'est exactement le piege de l'etape 4. Si un test ajoute un utilisateur au `FakeUserRepository`, le test suivant voit cet utilisateur. Les tests deviennent interdependants. Solutions : soit creer un nouveau repo dans chaque test, soit vider le repo dans un `Dispose()`. Laisser les etudiants tomber dans le piege avant de donner la solution.

### "Quand est-ce qu'on utilise NSubstitute en vrai ?"
NSubstitute est utile pour les dependances qu'on ne controle pas (interfaces de bibliotheques tierces) ou pour les cas ou ecrire un fake serait trop complexe. Mais pour les interfaces du domaine metier, les doublures manuelles sont presque toujours preferables.

## Pieges courants

- **Oublier de creer `SpySmsSender` et `StubClock`** — le premier test ne compile pas sans eux. Bien lire l'enonce.
- **Ecrire des assertions trop larges avec NSubstitute** — `emailSender.Received(1).Send(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())` ne verifie rien d'utile. Faire remarquer.
- **Ecrire des assertions trop strictes avec NSubstitute** — verifier les 3 arguments en dur rend le test fragile. Il n'y a pas de juste milieu facile avec `.Received()`, c'est ca le probleme.
- **Ne pas voir la regression de l'etape 5b** — si un etudiant a utilise `Arg.Any<>()` partout dans les tests spy aussi (via des assertions trop laxistes), le spy ne detectera pas non plus la regression. Verifier que les assertions spy sont precises sur le body.
- **Confondre IClassFixture et le constructeur** — `IClassFixture<T>` cree UNE instance pour toute la classe. Le constructeur est appele avant chaque test. Si on met le setup dans le constructeur, on recree tout a chaque test (ce qui est souvent ce qu'on veut pour l'isolation).
