# Couverture de code

<br>

La **couverture de code** mesure quelle proportion du code est exécutée par les tests.

<v-clicks>

```bash
# Générer un rapport de couverture avec .NET
dotnet test --collect:"XPlat Code Coverage"
# ou avec coverlet
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov
```

Résultat typique :

```
+-----------------------+--------+--------+---------+
| Module                | Line   | Branch | Method  |
+-----------------------+--------+--------+---------+
| MonProjet             | 87.3%  | 72.1%  | 95.0%   |
+-----------------------+--------+--------+---------+
```

</v-clicks>

<!--
La couverture est souvent le premier indicateur qu'on regarde. Mais elle peut être trompeuse.
On va voir pourquoi.
-->

---

# Les types de couverture

<br>

<v-clicks>

| Type | Ce qu'il mesure | Exemple |
|------|----------------|---------|
| **Ligne** (Line) | Les lignes exécutées | `if (x > 0) return x;` — la ligne est couverte |
| **Branche** (Branch) | Les chemins de décision | `if/else` — les deux branches sont-elles couvertes ? |
| **Chemin** (Path) | Toutes les combinaisons de chemins | 3 `if` = 8 chemins possibles |
| **Méthode** | Les méthodes appelées | Toutes les méthodes publiques sont-elles testées ? |

</v-clicks>

<v-click>

<br>

**La couverture de branche** est plus précise que la couverture de ligne.

La couverture de chemin est théoriquement idéale mais **explose combinatoirement**.

</v-click>

<!--
En pratique, on utilise la couverture de ligne (la plus courante) et la couverture de branche.
La couverture de chemin est rarement calculée car le nombre de chemins explose.
-->

---

# Couvert ≠ Testé

<br>

```csharp
// Ce test a 100% de couverture de ligne...
[Fact]
public void Calculate_DoesNotThrow()
{
    var service = new PricingService();

    service.Calculate(new Order { Items = new[] { 10m, 20m } });

    // ✅ Aucune exception levée
    // ❌ Mais on ne vérifie RIEN sur le résultat !
}
```

<v-clicks>

Ce test **exécute** le code mais ne **vérifie** rien.

100% de couverture, 0% de confiance.

> **Couvert** signifie que le code a été **exécuté** pendant les tests.
> **Testé** signifie que le **comportement** a été **vérifié** par des assertions.

</v-clicks>

<!--
C'est LE piège de la couverture. Un test sans assertion utile donne de la couverture gratuite.
C'est pour ça qu'on ne peut pas se fier à la couverture seule comme indicateur de qualité.
-->

---

# Que faire de la couverture ?

<br>

<v-clicks>

### ✅ Utile pour...

- Identifier du **code non testé** (zones à risque)
- Détecter les **branches oubliées** (else, catch, cas null)
- Suivre l'**évolution** dans le temps (la couverture diminue-t-elle ?)

### ❌ Inutile pour...

- **Garantir** la qualité des tests
- Fixer un **seuil arbitraire** (80% ? 90% ? 100% ?)
- **Comparer** des projets entre eux

</v-clicks>

<v-click>

<br>

> La couverture est un **outil de diagnostic**, pas un **objectif**.

</v-click>

<!--
Un projet à 60% de couverture avec des tests pertinents vaut mieux qu'un projet à 95% avec des tests vides.
Utilisez la couverture pour trouver les trous, pas comme un KPI de performance.
-->
