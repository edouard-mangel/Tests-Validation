# Approval Testing — Vérifier par comparaison

<br>

<v-clicks>

Au lieu d'écrire des assertions une par une sur un objet complexe...

```csharp
// ❌ Verbeux et fragile pour des objets complexes
Assert.Equal("Alice", user.Name);
Assert.Equal("alice@test.com", user.Email);
Assert.Equal(3, user.Orders.Count);
Assert.Equal("Premium", user.Tier);
// ... et 20 autres propriétés
```

...on capture la sortie complète et on la compare à une **baseline approuvée**.

</v-clicks>

<v-click>

### Quand l'utiliser ?

- Objets complexes avec beaucoup de propriétés
- Sorties sérialisées (JSON, XML, HTML)
- Rapports, exports, réponses d'API complexes
- **Regression testing** sur du code legacy

</v-click>

<!--
L'approval testing est complémentaire aux assertions classiques.
On l'utilise quand écrire des assertions manuelles serait trop verbeux ou fragile.
-->

---
layout: two-cols-header
zoom: 0.85
---

# Verify — Approval Testing pour .NET

::left::

```bash
dotnet add package Verify.Xunit
```

```csharp
[UsesVerify]
public class UserServiceTests
{
    [Fact]
    public Task GetUserProfile_ReturnsExpectedShape()
    {
        var service = new UserService(new FakeUserRepository());

        var profile = service.GetProfile(userId: 1);

        return Verify(profile); // Sérialise et compare avec le fichier .verified.txt
    }
}
```

::right::

<v-click>

Fichier `GetUserProfile_ReturnsExpectedShape.verified.txt` généré :

```json
{
  Name: Alice,
  Email: alice@test.com,
  Tier: Premium,
  Orders: [ { Id: 1, Total: 42.00 } ]
}
```

> Premier run → le fichier `.received.txt` est créé → on le review et renomme en `.verified.txt`.
> Runs suivants → diff automatique entre la sortie et le fichier approuvé.

</v-click>

<!--
Verify est une bibliothèque puissante pour l'approval testing en .NET.
Le workflow : 1) Exécuter le test, 2) Vérifier le .received.txt, 3) Approuver en le renommant en .verified.txt.
Très utile en régression : si la sortie change, le test échoue et montre le diff.
Ce n'est PAS un remplacement des tests comportementaux — c'est un complément pour les sorties complexes.
-->

---
layout: center
class: text-center
---

# TP — Doublures de test

<br>

**Exercice 1** — Tester `NotificationService` avec des doublures manuelles

**Exercice 2** — Refactorer du code couplé pour le rendre testable

**Exercice 3** — Test Data Builders et `IClassFixture`

<br>

[📄 Télécharger le sujet du TP](/tp3.pdf)

<!--
TP3 — Doublures de test.
-->
