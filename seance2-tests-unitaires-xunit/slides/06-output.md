# Afficher du texte dans les tests

<br>

<v-click>

Les tests ne sont pas des applications console. **`Console.WriteLine`** n'affiche rien dans le Test Explorer.

</v-click>

<v-clicks depth="2">

<br>

xUnit fournit `ITestOutputHelper` pour envoyer du texte dans la sortie du test :

```bash
# Visible dans la sortie de test dans Visual Studio et en CI
dotnet test --logger "console;verbosity=detailed"
```

</v-clicks>

<!--
Console.WriteLine fonctionne en théorie, mais xUnit capture la sortie standard et ne l'affiche pas par défaut.
ITestOutputHelper est l'outil officiel.
-->

---
layout: two-cols-header
zoom: 0.85
---

# ITestOutputHelper — Mise en place

::left::

**Étape 1 :** déclarer le champ

<img src="/images/output-helper-1.png" class="h-32 mx-auto my-2" />

**Étape 2 :** injecter via le constructeur

<img src="/images/output-helper-2.png" class="h-32 mx-auto my-2" />

::right::

<v-clicks depth="2">

```csharp
public class CalculatorTests
{
    private readonly ITestOutputHelper _output;

    // xUnit injecte automatiquement ITestOutputHelper
    public CalculatorTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Plus_ReturnsSum()
    {
        int result = Calculator.Plus(2, 3);
        _output.WriteLine($"2 + 3 = {result}");
        Assert.Equal(5, result);
    }
}
```

</v-clicks>

<!--
C'est notre premier exemple d'injection de dépendances !
xUnit crée une instance de ITestOutputHelper et l'injecte dans le constructeur.
On verra ce mécanisme en détail en séance 3.
-->

---
layout: two-cols-header
zoom: 0.9
---

# ITestOutputHelper — En pratique

::left::

<img src="/images/output-helper-3.png" class="h-56 mx-auto" />

::right::

<v-clicks depth="2">

Le texte s'affiche dans la sortie du test dans Visual Studio.

Utile pour **diagnostiquer** des tests complexes :

```csharp
[Fact]
public void ProcessOrder_CalculatesCorrectTotal()
{
    var order = CreateTestOrder();
    _output.WriteLine($"Items: {order.Items.Count}");
    _output.WriteLine($"Subtotal: {order.Subtotal}");
    _output.WriteLine($"Discount: {order.Discount}");

    var total = service.ProcessOrder(order);
    _output.WriteLine($"Final total: {total}");

    Assert.Equal(89.99m, total);
}
```

</v-clicks>

<!--
Insérer un objet dans le constructeur via injection de dépendances — on anticipe la séance 3.
ITestOutputHelper est un exemple simple de l'injection de dépendances : xUnit s'en charge automatiquement.
-->
