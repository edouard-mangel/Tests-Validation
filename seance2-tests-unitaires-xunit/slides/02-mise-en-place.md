# Notre premier exemple

<br>

<v-click>

On va tester la fonction la plus simple qui soit :

```csharp
public static class Calculator
{
    public static int Plus(int a, int b) => a + b;
}
```

</v-click>

<v-click>

<br>

**Règle d'or :** le code de production et le code de tests sont dans des **projets séparés**.

```
MonApp/
├── Calculator/          ← projet de production
│   └── Calculator.cs
└── Calculator.Tests/    ← projet de tests xUnit
    └── CalculatorTests.cs
```

</v-click>

<!--
Partir d'un exemple aussi simple que possible pour se concentrer sur la mécanique des tests.
La séparation en deux projets est une bonne pratique : les tests ne doivent pas être déployés en production.
-->

---
layout: two-cols-header
zoom: 0.9
---

# Créer le projet de production

::left::

<img src="/images/setup-projet-production.png" class="h-72 mx-auto" />

::right::

<v-clicks>

Dans Visual Studio :

1. **Nouveau projet** → *Class Library*
2. Nommer le projet `Calculator`
3. Ajouter la classe `Calculator.cs`

```csharp
public static class Calculator
{
    public static int Plus(int a, int b) => a + b;
}
```

</v-clicks>

<!--
Montrer les étapes dans l'IDE. Les étudiants suivent sur leur propre machine.
-->

---
layout: two-cols-header
zoom: 0.9
---

# Créer le projet de tests

::left::

<img src="/images/setup-projet-tests-1.png" class="h-60 mx-auto" />

<img src="/images/setup-projet-tests-2.png" class="h-40 mx-auto mt-2" />

::right::

<v-clicks>

Dans la même solution :

1. **Ajouter un projet** → *xUnit Test Project*
2. Nommer `Calculator.Tests`
3. xUnit génère automatiquement une classe de test

</v-clicks>

<!--
xUnit Test Project génère le code de bootstrap automatiquement.
-->

---
layout: two-cols-header
zoom: 0.9
---

# Code auto-généré et référence de projet

::left::

<img src="/images/code-autogenere.png" class="h-48 mx-auto" />

<img src="/images/ajout-reference.png" class="h-48 mx-auto mt-2" />

::right::

<v-clicks>

xUnit génère une classe avec l'attribut `[Fact]` :

```csharp
public class UnitTest1
{
    [Fact]
    public void Test1()
    {
    }
}
```

**Étape importante :** ajouter une référence de projet vers `Calculator` dans `Calculator.Tests`.

*Clic droit → Ajouter → Référence de projet*

</v-clicks>

<!--
Sans la référence de projet, le projet de tests ne peut pas utiliser les classes de production.
-->
