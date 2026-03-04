# Les outils pour les tests d'intégration

<br>

<v-clicks>

| Outil | Usage | Trade-off |
|-------|-------|-----------|
| **EF Core InMemoryDatabase** | Tester les repositories sans serveur | Rapide mais pas fidèle (pas de SQL) |
| **SQLite in-memory** | Base relationnelle légère, en mémoire | Bon compromis fidélité/vitesse |
| **Testcontainers** | Vraie BDD dans Docker (SQL Server, Postgres...) | Fidèle mais plus lent à démarrer |
| **WebApplicationFactory** | App ASP.NET complète en mémoire | Pipeline réel, configurable |
| **WireMock** | Simuler des APIs externes | Contrôle total des réponses HTTP |

</v-clicks>

<!--
Le choix dépend du besoin de fidélité vs vitesse.
InMemoryDatabase pour les cas simples, SQLite pour un bon compromis, Testcontainers quand on a besoin du comportement exact de la BDD cible.
-->

---

# Testcontainers — Exemple

<br>

```csharp
public class UserRepositoryIntegrationTests : IAsyncLifetime
{
    private readonly MsSqlContainer _container = new MsSqlBuilder().Build();

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
    }

    [Fact]
    public async Task Add_SavesUserToRealDatabase()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(_container.GetConnectionString())
            .Options;
        await using var context = new AppDbContext(options);
        await context.Database.EnsureCreatedAsync();

        var repo = new UserRepository(context);
        repo.Add(new User { Name = "Alice" });
        await context.SaveChangesAsync();

        Assert.Single(await context.Users.ToListAsync());
    }

    public async Task DisposeAsync() => await _container.DisposeAsync();
}
```

<v-click>

Une **vraie** base SQL Server dans Docker, démarrée et détruite automatiquement par le test.

</v-click>

<!--
Testcontainers est puissant mais plus lourd : il faut Docker installé, et le démarrage du container prend quelques secondes.
Pour les projets qui utilisent des fonctionnalités spécifiques de SQL Server (procédures stockées, etc.), c'est indispensable.
-->

---

# Les tests e2e — Rappel

<br>

<v-clicks>

Les tests e2e vérifient le parcours **complet** de l'utilisateur :

- Navigateur réel (Playwright, Selenium, Cypress)
- Serveur réel, base de données réelle
- Les plus **réalistes** mais les plus **coûteux**

### Quand utiliser des tests e2e ?

- **Parcours critiques** : login, paiement, inscription
- **Smoke tests** : vérifier que l'application démarre et affiche la page d'accueil
- **Scénarios de bout en bout** impossibles à tester autrement

### Quand NE PAS utiliser des tests e2e ?

- Pour tester de la **logique métier** → test unitaire
- Pour tester l'**accès aux données** → test d'intégration
- Pour chaque variation d'un formulaire → trop lent et fragile

</v-clicks>

<!--
Les tests e2e sont précieux mais rares. 5 à 20 tests e2e bien choisis valent mieux que 200 tests e2e fragiles.
Un changement de CSS peut casser un test e2e. Un serveur lent peut faire échouer un test.
-->
