# Le problème

<br>

Jusqu'ici, on a testé des **fonctions pures** :

```csharp
// Entrée → Sortie, pas de dépendances
public int Add(int a, int b) => a + b;
```

<v-click>

<br>

Mais le code réel a des **dépendances externes** :

- 🗄️ Bases de données
- 🌐 APIs et services web
- 🕐 Horloge système (`DateTime.Now`)
- 📁 Système de fichiers
- 📧 Services d'email

</v-click>

<!--
La vraie difficulté des tests unitaires n'est pas la syntaxe. C'est l'isolation.
Comment tester un service qui envoie des emails sans envoyer de vrais emails ?
-->

---

# Un exemple concret

<br>

```csharp
public class NotificationService
{
    public void NotifyUser(int userId, string message)
    {
        // ❌ Dépendance directe sur la base de données
        var db = new SqlConnection("Server=prod;Database=app;...");
        var user = db.Query<User>("SELECT * FROM Users WHERE Id = @id", new { id = userId });

        // ❌ Dépendance directe sur un service d'email
        var smtp = new SmtpClient("smtp.gmail.com");
        smtp.Send(new MailMessage("noreply@app.com", user.Email, "Notification", message));

        // ❌ Dépendance directe sur l'horloge
        Console.WriteLine($"[{DateTime.Now}] Email envoyé à {user.Email}");
    }
}
```

<v-click>

Ce code est **impossible à tester unitairement** :
- Il faut une vraie base de données
- Il envoie de vrais emails
- Le résultat dépend de l'heure

</v-click>

<!--
C'est un exemple volontairement horrible. Mais c'est ce qu'on trouve dans beaucoup de projets.
La question : comment rendre ce code testable ?
-->

---

# La solution : l'inversion des dépendances

<br>

<v-clicks>

Au lieu de **créer** ses dépendances, le code les **reçoit** :

```
AVANT : NotificationService → new SqlConnection()
                             → new SmtpClient()
                             → DateTime.Now

APRÈS : NotificationService ← IUserRepository (injecté)
                             ← IEmailSender (injecté)
                             ← IClock (injecté)
```

Le code dépend d'**abstractions** (interfaces), pas d'**implémentations** concrètes.

En test, on remplace les vraies implémentations par des **doublures**.

</v-clicks>

<!--
C'est le Dependency Inversion Principle (le D de SOLID).
On y reviendra plus en détail en séance 5.
Pour l'instant, retenez : si vous voulez tester, il faut pouvoir remplacer les dépendances.
-->
