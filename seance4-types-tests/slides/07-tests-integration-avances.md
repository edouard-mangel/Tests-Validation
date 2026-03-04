# Tests d'intégration — Repository

<br>

Tester qu'un repository fonctionne avec une **vraie** base de données (ou une base en mémoire) :

```csharp
public class UserRepositoryTests : IDisposable
{
    private readonly AppDbContext _context;

    public UserRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new AppDbContext(options);
    }

    [Fact]
    public void Add_SavesUserToDatabase()
    {
        var repo = new UserRepository(_context);

        repo.Add(new User { Name = "Alice", Email = "alice@test.com" });

        var saved = _context.Users.Single();
        Assert.Equal("Alice", saved.Name);
    }

    public void Dispose() => _context.Dispose();
}
```

<!--
UseInMemoryDatabase crée une base EF Core en mémoire — pas besoin de serveur SQL.
Guid.NewGuid() pour le nom de la base → chaque test a sa propre base → isolation.
IDisposable pour nettoyer la base après chaque test.
-->

---

# Tests d'intégration — API avec WebApplicationFactory

<br>

`WebApplicationFactory` démarre l'application **en mémoire** pour les tests :

```csharp
public class UsersApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UsersApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetUsers_ReturnsOkAndUserList()
    {
        var response = await _client.GetAsync("/api/users");

        response.EnsureSuccessStatusCode();
        var users = await response.Content
            .ReadFromJsonAsync<List<UserDto>>();
        Assert.NotNull(users);
    }
}
```

<v-click>

On teste le **vrai pipeline** : routing, middleware, controllers, sérialisation.

Pas besoin de démarrer un serveur externe — tout tourne dans le process de test.

</v-click>

<!--
WebApplicationFactory est l'outil phare pour les tests d'intégration ASP.NET.
Il démarre l'application complète en mémoire, avec le vrai pipeline HTTP.
On peut personnaliser les services (remplacer la BDD par une in-memory, etc.).
-->

---

# Personnaliser l'environnement de test

<br>

On peut **remplacer des services** pour le test (ex: base de données) :

```csharp
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remplacer la vraie BDD par une base en mémoire
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (descriptor != null) services.Remove(descriptor);

            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));
        });
    }
}
```

<v-click>

On garde le **vrai pipeline** mais avec une **infrastructure de test**.

</v-click>

<!--
C'est le pattern standard pour les tests d'intégration ASP.NET.
On garde tout le vrai code (controllers, services, validation) mais on remplace l'infrastructure (BDD, etc.).
-->

---
layout: two-cols-header
---

# Tests d'intégration — Bilan

::left::

### ✅ Ce qu'ils vérifient

<v-clicks>

- Le câblage entre composants fonctionne
- Les requêtes SQL/LINQ sont correctes
- La sérialisation/désérialisation marche
- Le routing et le middleware fonctionnent
- La configuration DI est valide

</v-clicks>

::right::

### ❌ Limites

<v-clicks>

- Plus **lents** que les tests unitaires
- Plus **complexes** à mettre en place
- InMemoryDatabase ≠ vraie BDD (comportements différents)
- Ne testent pas la logique métier (c'est le rôle des TU)

</v-clicks>

<!--
Les tests d'intégration complètent les tests unitaires, ils ne les remplacent pas.
InMemoryDatabase a des limites : pas de contraintes FK, pas de transactions, pas de SQL brut.
Pour des cas critiques, on peut utiliser testcontainers (vraie BDD dans Docker).
-->
