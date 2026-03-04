---
layout: two-cols-header
zoom: 0.9
---

# Le problème du setup répété

::left::

```csharp
public class NotificationServiceTests
{
    [Fact]
    public void NotifyUser_ExistingUser_SendsEmail()
    {
        var clock = new StubClock(new DateTime(2025, 1, 1));
        var repo = new FakeUserRepository();
        repo.Add(new User { Id = 1, Email = "alice@test.com" });
        var sender = new SpyEmailSender();
        var service = new NotificationService(repo, sender, clock);

        service.NotifyUser(1, "Hello");
        Assert.Single(sender.SentEmails);
    }

    [Fact]
    public void NotifyUser_UnknownUser_DoesNotSendEmail()
    {
        var clock = new StubClock(new DateTime(2025, 1, 1));
        var repo = new FakeUserRepository();
        var sender = new SpyEmailSender();
        var service = new NotificationService(repo, sender, clock);

        service.NotifyUser(999, "Hello");
        Assert.Empty(sender.SentEmails);
    }
}
```

::right::

<v-click>

> 2 tests → 2 fois le même bloc Arrange. Et si on ajoute une dépendance au service ?

</v-click>

<!--
Le copier-coller de setup est un problème classique.
xUnit propose des mécanismes intégrés pour résoudre ça : IClassFixture et IAsyncLifetime.
-->

---
zoom: 0.9
layout: two-cols-header
---

# IClassFixture — Partager le setup entre tests

::left::

```csharp
public class DatabaseFixture : IDisposable
{
    public SqliteConnection Connection { get; }

    public DatabaseFixture()
    {
        Connection = new SqliteConnection("DataSource=:memory:");
        Connection.Open();
        // Créer le schéma, seeder les données...
    }

    public void Dispose() => Connection.Dispose();
}
```

::right::

<v-click>

```csharp
public class OrderRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public OrderRepositoryTests(DatabaseFixture fixture)
    {
        _fixture = fixture; // xUnit injecte la fixture via le constructeur
    }

    [Fact]
    public void GetById_ExistingOrder_ReturnsOrder()
    {
        var repo = new OrderRepository(_fixture.Connection);
        // ...
    }
}
```

</v-click>

<!--
xUnit crée la fixture UNE SEULE FOIS pour tous les tests de la classe, puis la dispose à la fin.
Idéal pour les setups coûteux : connexion BDD, client HTTP, chargement de fichiers.
Attention : les tests partagent la fixture → ne pas modifier l'état partagé entre tests.
-->

---
zoom: 0.9
layout: two-cols-header
---

# IAsyncLifetime — Setup/teardown asynchrone

::left::

```csharp
public class ApiIntegrationTests : IAsyncLifetime
{
    private TestServer _server = null!;
    private HttpClient _client = null!;

    public async Task InitializeAsync()
    {
        _server = await TestServer.StartAsync();
        _client = _server.CreateClient();
        await SeedTestData(_client);
    }

    public async Task DisposeAsync()
    {
        _client.Dispose();
        await _server.StopAsync();
    }

    [Fact]
    public async Task GetUsers_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/users");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
```
::right::

<v-click>

> `IAsyncLifetime` : quand le setup nécessite `await` (conteneurs, serveurs, bases de données).

</v-click>

<!--
IAsyncLifetime est particulièrement utile avec Testcontainers (démarrer un conteneur Docker).
On verra ça en détail en séance 4.
InitializeAsync est appelé avant chaque test, DisposeAsync après.
-->
