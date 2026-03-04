# Les limites des tests par l'exemple

<br>

<v-clicks>

Les tests qu'on a écrits jusqu'ici sont des **tests par l'exemple** :

```csharp
[Theory]
[InlineData(2, 3, 5)]
[InlineData(0, 0, 0)]
[InlineData(-1, 1, 0)]
public void Add_ReturnsSum(int a, int b, int expected)
{
    Assert.Equal(expected, new Calculator().Add(a, b));
}
```

On choisit **manuellement** les cas de test. Mais...

- A-t-on pensé à `int.MaxValue` ?
- A-t-on pensé aux très grands nombres négatifs ?
- A-t-on couvert **tous** les cas intéressants ?

</v-clicks>

<v-click>

> Et si on pouvait tester une **propriété** qui doit être vraie pour **toutes** les entrées ?

</v-click>

<!--
Les tests par l'exemple sont essentiels, mais ils ne couvrent que les cas auxquels on pense.
Le property-based testing génère des centaines d'entrées aléatoires pour trouver les cas limites qu'on aurait oubliés.
-->

---

# Property-Based Testing — Le concept

<br>

<v-clicks>

Au lieu de tester des **exemples spécifiques**, on teste des **propriétés universelles**.

```csharp
// Propriété : inverser une liste deux fois redonne la liste originale
[Property]
public bool Reverse_Twice_IsIdentity(List<int> list)
{
    var reversed = list.AsEnumerable().Reverse().Reverse().ToList();
    return list.SequenceEqual(reversed);
}
```

</v-clicks>

<v-click>

FsCheck génère **des centaines d'entrées aléatoires** et vérifie que la propriété tient :

```
✅ Ok, passed 100 tests.
```

Si un cas échoue, FsCheck **réduit** l'entrée au cas le plus simple (*shrinking*).

```bash
dotnet add package FsCheck.Xunit
```

</v-click>

<!--
FsCheck est le framework de property-based testing le plus populaire en .NET.
Il génère des entrées aléatoires, vérifie la propriété, et si un cas échoue, il "shrink" l'entrée pour trouver le cas minimal qui reproduit le bug.
-->

---

# Property-Based Testing — Quand l'utiliser

<br>

<v-clicks>

### Bons candidats pour le property-based testing

| Propriété | Exemple |
|-----------|---------|
| **Aller-retour** (roundtrip) | `Deserialize(Serialize(obj)) == obj` |
| **Idempotence** | `Sort(Sort(list)) == Sort(list)` |
| **Invariant** | `Withdraw(account, amount).Balance >= 0` |
| **Commutativité** | `Add(a, b) == Add(b, a)` |
| **Relation avec un oracle** | `MySort(list) == list.OrderBy(x => x)` |

</v-clicks>

<v-click>

### Ce n'est PAS un remplacement

- Les tests par l'exemple restent indispensables pour les **scénarios métier**
- Le PBT est **complémentaire** : il trouve les cas limites qu'on n'imagine pas
- Particulièrement puissant pour les fonctions **mathématiques** et les **sérialisations**

</v-click>

<!--
Le property-based testing est un outil puissant pour trouver des edge cases.
Il ne remplace pas les tests classiques mais les complète.
Pensez-y pour tout ce qui a des propriétés mathématiques ou des invariants forts.
-->
