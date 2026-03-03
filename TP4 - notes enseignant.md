# TP4 - Notes enseignant

## Intention pedagogique

Ce TP fait passer les etudiants des tests unitaires aux tests d'integration. L'objectif principal est de leur montrer **ce que les tests unitaires ne peuvent pas attraper** : problemes de serialisation, contraintes de base de donnees, comportement du pipeline HTTP complet.

Chaque exercice cible un niveau d'integration different :
- Exercice 1 : repository + base de donnees (InMemory vs SQLite)
- Exercice 2 : API complète (HTTP + routing + serialisation + base)
- Exercice 3 : property-based testing (approche complementaire)
- Exercice 4 : contract testing (integration entre services)

## Leçons cles a faire retenir

1. **InMemory n'est pas une vraie base de donnees** — il ne verifie pas les contraintes NOT NULL, les cles uniques, ni les types SQL. C'est pratique pour le developpement rapide mais insuffisant pour les tests d'integration serieux.

2. **SQLite in-memory est un bon compromis** — plus fidele qu'InMemory, aussi rapide (pas de disque), et jetable. C'est le choix recommande pour les tests d'integration de repositories.

3. **WebApplicationFactory teste le vrai pipeline** — routing, middleware, serialisation JSON, codes HTTP. Un test unitaire du controller ne teste rien de tout ca.

4. **Les bugs de serialisation sont invisibles aux tests unitaires** — le bug de timezone (exercice 2) est un exemple classique. Il n'apparait que quand on serialize/deserialize via HTTP.

5. **Le property-based testing trouve les cas que vous n'avez pas imagines** — FsCheck genere des valeurs extremes que personne n'ecrirait dans un `[InlineData]`.

6. **Le contract testing protege contre les ruptures d'API** — quand deux equipes travaillent sur des services differents, le fichier `.pact` est le contrat qui les lie.

## Deroulement conseille

| Duree | Activite |
|-------|----------|
| 10 min | Mise en place, exploration du projet TodoApi |
| 35 min | Exercice 1 — tests du repository |
| 35 min | Exercice 2 — tests d'API avec WebApplicationFactory |
| 25 min | Exercice 3 — property-based testing |
| 15 min | Exercice 4 — contract testing (guide) |
| 10 min | Reflexion de fin + debriefing |

## Points d'attention par exercice

### Exercice 1 — Repository

Le moment cle est quand un test passe avec InMemory mais echoue avec SQLite (ou l'inverse). Exemples typiques :

- Inserer un `Todo` avec `Title = null` alors que le schema a `[Required]` sur Title. InMemory l'accepte, SQLite leve une exception.
- Inserer deux `Todo` avec le meme `Id`. InMemory ecrase silencieusement, SQLite leve une exception de contrainte unique.

Si les etudiants ne trouvent pas de difference, les orienter vers les contraintes NOT NULL.

**Isolation** : chaque test doit utiliser un `Guid.NewGuid().ToString()` comme nom de base InMemory, ou une connexion SQLite separee. Sans ca, les tests interferent entre eux et les echecs sont non-deterministes.

### Exercice 2 — API

Le `CustomWebApplicationFactory` est le passage le plus technique du TP. Les etudiants doivent :
1. Comprendre qu'on remplace le `DbContext` reel par un InMemory
2. Utiliser `PostAsJsonAsync` / `GetFromJsonAsync`
3. Verifier les codes HTTP ET le contenu

Le **bug de serialisation de dates** est le moment "aha" de l'exercice. Un `DateTime` local (ex: `2025-12-31T00:00:00`) devient `2025-12-30T23:00:00Z` apres serialisation JSON si le serveur est en UTC. Le `Assert.Equal` sur la date echoue. Ce bug est **invisible** dans un test unitaire car il n'y a pas de serialisation.

Si les etudiants ne trouvent pas le bug : leur demander de creer un Todo avec `DueDate = new DateTime(2025, 12, 31)`, le relire via GET, et comparer les dates.

### Exercice 3 — FsCheck

C'est souvent la premiere rencontre des etudiants avec le property-based testing. Les points de confusion :

- **"Quel est le resultat attendu ?"** — il n'y en a pas ! On verifie une **propriete** (ex: "le total est toujours >= 0"), pas un resultat precis.
- **Le shrinking** — quand FsCheck trouve un echec, il reduit l'entree au cas minimal. C'est magique la premiere fois. Montrer un exemple au tableau.
- **Les types des parametres** — FsCheck genere les parametres du test automatiquement. `decimal[]` genere des tableaux de tailles et valeurs variees. Attention : `decimal.MaxValue` peut causer des overflows.

### Exercice 4 — Pact

C'est un exercice guide car le concept est nouveau. L'essentiel est que les etudiants comprennent le **flux** :
1. Le consommateur (TodoApi) ecrit un test qui decrit ce qu'il attend du fournisseur
2. Pact genere un fichier `.pact` (contrat)
3. Le fournisseur (InventoryService) execute ce contrat pour verifier qu'il le respecte

La question de fin ("que se passe-t-il si `quantity` est renomme en `stock` ?") est cruciale : le test du consommateur continue de passer (il teste contre un mock), mais le contrat echoue cote fournisseur.

## FAQ anticipee

### "Pourquoi pas toujours SQLite au lieu d'InMemory ?"
SQLite a ses propres limites : pas de support des colonnes JSON, syntaxe SQL simplifiee, pas de procedures stockees. Pour tester avec SQL Server en production, il faudrait Testcontainers (hors scope de ce TP).

### "WebApplicationFactory lance vraiment un serveur ?"
Oui et non. Il cree un `TestServer` en memoire — pas de port reseau ouvert, mais tout le pipeline ASP.NET est la (middleware, routing, DI, serialisation). C'est plus rapide qu'un vrai serveur et aussi fidele.

### "FsCheck trouve un bug avec `decimal.MaxValue`. C'est un vrai bug ?"
Ca depend. Si le service est cense gerer n'importe quel `decimal`, oui. Si les prix sont toujours entre 0 et 10000, on peut contraindre le generateur. Mais la question "quelle est la plage valide ?" est deja une victoire — le test a force la reflexion.

### "Le fichier `.pact` sert a quoi concretement ?"
En CI/CD, le consommateur publie son `.pact` sur un Pact Broker. Le fournisseur le telecharge et l'execute dans ses propres tests. Si le contrat est casse, le build du fournisseur echoue **avant** le deploiement.

### "La `<details>` ne fonctionne pas dans mon PDF."
Les `<details>` sont prevus pour une version web ou interactive. En PDF, ils sont affiches ouverts. Si les indices sont trop visibles, demander aux etudiants de ne pas regarder les indices avant d'avoir essaye.

### "Mon test d'API retourne 500 au lieu de 404."
Verifier que le `CustomWebApplicationFactory` remplace bien le `DbContext`. Si la base reelle est configuree (connection string dans `appsettings.json`), le test essaie de se connecter a une vraie base et echoue.

## Pieges courants

- **Oublier `connection.Open()` pour SQLite in-memory** — la base disparait si la connexion est fermee. L'erreur est cryptique ("no such table").
- **Ne pas appeler `context.Database.EnsureCreated()` avec SQLite** — les tables n'existent pas et les requetes echouent.
- **Reutiliser le meme nom de base InMemory entre tests** — les tests partagent l'etat et echouent de facon non-deterministe. Toujours utiliser `Guid.NewGuid()`.
- **Oublier le `using` sur le DbContext** — fuite de connexion, surtout visible avec SQLite.
- **Le `Program` n'est pas accessible** — si le projet TodoApi n'expose pas `Program` comme public, `WebApplicationFactory<Program>` ne compile pas. Ajouter `public partial class Program { }` dans `Program.cs` ou utiliser `[assembly: InternalsVisibleTo("TodoApi.Tests")]`.
