---
layout: image
image: /images/tests-e2e.png
---

<!--
Schéma d'architecture : la flèche rouge traverse TOUS les sous-systèmes, du client à la BDD.
C'est ça un test e2e : on teste la chaîne complète.
-->

---

# Tests End-to-End (e2e)

Au plus près du contexte de l'utilisateur.

Vérifient l'**ensemble des sous-systèmes** impliqués dans un cas d'usage.

<br>

<v-clicks>

### ✅ Avantages

- Testent l'application **comme un vrai utilisateur**
- Vérifient que tous les composants fonctionnent ensemble
- Outils indépendants de la technologie (Selenium, Cypress, Playwright...)

### ❌ Inconvénients

- **Très lents** à l'exécution (~30 min pour 150 tests)
- **Chers** à développer ET à maintenir
- **Fragiles** : vulnérables aux éléments extérieurs (latence réseau, modification IHM...)

</v-clicks>

<!--
Les tests e2e simulent un vrai utilisateur : ils ouvrent un navigateur, cliquent, remplissent des formulaires.
C'est puissant mais c'est lent et fragile.
Un changement de CSS peut casser un test e2e. Un serveur lent peut faire échouer un test.
On les réserve aux parcours critiques (login, paiement, inscription...).
-->

---

# Exemple de test e2e

<br>

Un test Playwright qui vérifie le parcours de connexion :

```csharp
[Test]
public async Task User_CanLogin_WithValidCredentials()
{
    // Arrange - On ouvre un vrai navigateur
    var page = await Browser.NewPageAsync();
    await page.GotoAsync("https://monapp.com/login");

    // Act - On simule les actions utilisateur
    await page.FillAsync("#email", "user@example.com");
    await page.FillAsync("#password", "P4ssword!");
    await page.ClickAsync("button[type=submit]");

    // Assert - On vérifie le résultat visible
    await Expect(page.Locator("h1")).ToHaveTextAsync("Bienvenue");
}
```

<v-click>

Le test manipule un **vrai navigateur**, un **vrai serveur**, une **vraie base de données**.

C'est réaliste, mais c'est lent et fragile.

</v-click>

<!--
Montrer que le pattern AAA (Arrange/Act/Assert) s'applique aussi aux tests e2e.
Souligner la différence avec un test unitaire : ici on a besoin de toute l'infrastructure.
Si le serveur est down, le test échoue. Si le CSS change, le sélecteur casse.
-->
