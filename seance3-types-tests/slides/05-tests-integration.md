---
layout: image
image: /images/tests-integration.png
---

<!--
Schéma d'architecture : les flèches orange montrent les connexions entre composants.
Chaque flèche = une intégration qu'on peut tester indépendamment.
On ne teste pas toute la chaîne, juste UNE connexion à la fois.
-->

---

# Tests d'intégration

Vérifient la **communication avec un sous-système extérieur** (BDD, API, système de mails...).

Environnement limité : on ne teste qu'**une connexion à la fois**.

<br>

<v-clicks>

### ✅ Avantages

- Permettent de vérifier indépendamment chaque sous-système extérieur
- Environnement limité et contrôlé
- Outils souvent les mêmes que les tests unitaires (en .NET : xUnit)

### ❌ Inconvénients

- Tests techniques, **peu de valeur métier**
- Spécifiques à chaque sous-système : changer le sous-système les rend obsolètes
- **Plus lents** que les tests unitaires (accès réseau, BDD...)

</v-clicks>

<!--
Les tests d'intégration vérifient que notre code communique correctement avec l'extérieur.
Par exemple : est-ce que mon repository arrive bien à lire et écrire dans la base ?
On ne teste PAS la logique métier ici, juste la plomberie.

Différence clé avec le e2e : on isole UN sous-système, on ne teste pas toute la chaîne.
-->

---

# Exemple de test d'intégration

<br>

Un test qui vérifie que notre repository fonctionne avec une vraie base de données :

```csharp
public class UserRepositoryTests : IDisposable
{
    private readonly AppDbContext _context;

    public UserRepositoryTests()
    {
        // Arrange - Base de données en mémoire (pas de serveur externe)
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
        _context = new AppDbContext(options);
    }

    [Fact]
    public void Add_SavesUserToDatabase()
    {
        var repository = new UserRepository(_context);
        var user = new User { Name = "Alice", Age = 25 };

        repository.Add(user);               // Act

        var saved = _context.Users.First();  // Assert
        Assert.Equal("Alice", saved.Name);
    }

    public void Dispose() => _context.Dispose();
}
```

<v-click>

On vérifie que la **plomberie fonctionne**, pas la logique métier.

</v-click>

<!--
UseInMemoryDatabase permet de ne pas avoir besoin d'un vrai serveur SQL.
C'est un compromis : on teste la couche d'accès aux données sans l'infra.
Pour un vrai test d'intégration "pur", on pourrait utiliser un container Docker avec une vraie BDD.

IDisposable permet de nettoyer la base entre chaque test.
-->
