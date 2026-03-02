# Le test comme spécification

Un test bien écrit **est** une spécification. Lisible par un humain, exécutable par une machine.

```csharp
[Fact]
public void Adulte_PeutSinscrirePourVoter()
{
    // Arrange
    var service = new InscriptionService();
    var electeur = new Citoyen { Nom = "Dupont", Age = 18 };

    // Act
    service.Inscrire(electeur);

    // Assert
    var inscrits = service.ObtenirListeElectorale();
    Assert.Contains(electeur, inscrits);
}
```

<v-click>

Ce test dit **exactement** ce que le code doit faire — pas comment il le fait.

C'est une **spécification exécutable**.

</v-click>

<!--
Rappel de la Séance 4 (école par scénario métier).
Un test bien nommé décrit un comportement attendu. N'importe qui peut lire ce test et comprendre la règle métier.
C'est important car ça va devenir le pont vers l'IA : si un test est une spécification claire, alors une IA peut l'utiliser comme instruction.
-->

---

# TDD + IA : le test est votre prompt

<br>

Le workflow en 3 étapes :

<v-clicks>

1. 🔴 **Vous** écrivez le test qui échoue — c'est votre **spécification**
2. 🟢 **L'IA** écrit l'implémentation qui fait passer le test
3. 🔵 **Ensemble**, vous refactorez le code

</v-clicks>

<br>

<v-click>

> *Le test contraint la sortie de l'IA. Sans test, l'IA improvise. Avec un test, elle a un objectif mesurable.*

</v-click>

<!--
Le test est le meilleur "prompt" qu'on puisse donner à une IA.
Pourquoi ? Parce qu'il est précis, vérifiable et automatisable.
Dire "fais une fonction de calcul de remise" est ambigu.
Écrire un test qui vérifie que la remise pour 100€ avec 10% donne 90€, c'est non-ambigu.
L'IA peut itérer : générer du code, lancer le test, corriger si ça échoue.
-->

---

# Exemple concret

````md magic-move
```csharp
// 🔴 Étape 1 — Vous écrivez le test
[Fact]
public void Calculer_Remise10Pourcent_Sur100Euros()
{
    var calculateur = new CalculateurRemise();

    var prix = calculateur.AppliquerRemise(montant: 100m, remise: 0.10m);

    Assert.Equal(90m, prix);
}
```

```csharp
// 🟢 Étape 2 — L'IA génère l'implémentation
public class CalculateurRemise
{
    public decimal AppliquerRemise(decimal montant, decimal remise)
    {
        if (remise < 0 || remise > 1)
            throw new ArgumentOutOfRangeException(nameof(remise));

        return montant * (1 - remise);
    }
}
```

```csharp
// 🔵 Étape 3 — Les tests passent ✅
// On ajoute d'autres cas pour renforcer la spécification
[Theory]
[InlineData(100, 0.10, 90)]
[InlineData(200, 0.25, 150)]
[InlineData(50, 0, 50)]
[InlineData(100, 1, 0)]
public void AppliquerRemise_CasMultiples(
    decimal montant, decimal remise, decimal attendu)
{
    var calc = new CalculateurRemise();
    Assert.Equal(attendu, calc.AppliquerRemise(montant, remise));
}
// Puis on demande à l'IA de gérer les cas limites...
```
````

<!--
Dérouler le magic-move étape par étape.
1. On écrit le test : c'est notre spécification.
2. On demande à l'IA "fais passer ce test". Elle génère le code.
3. On ajoute des tests pour renforcer la couverture. L'IA adapte le code.
Le cycle est le même que TDD, mais l'IA accélère l'étape verte.
-->

---

# Les tests comme filet de sécurité

L'IA génère du code **vite**, mais fait des **erreurs**.

<br>

<v-clicks>

- ❌ **Sans tests** → Vous relisez chaque ligne générée → lent et peu fiable
- ✅ **Avec tests** → Vous lancez les tests → feedback en secondes

</v-clicks>

<br>

<v-click>

L'IA est un **outil puissant** mais qui a besoin d'être **contraint** :

- Le test définit le **quoi** (le comportement attendu)
- L'IA propose le **comment** (l'implémentation)
- Le test **vérifie** que le comment respecte le quoi

</v-click>

<!--
Point clé : l'IA ne remplace pas les tests, elle les rend encore plus importants.
Plus on utilise l'IA pour générer du code, plus on a besoin de tests pour vérifier ce code.
C'est un changement de rôle : le développeur passe de "celui qui écrit le code" à "celui qui spécifie et vérifie".
Les tests sont le langage de communication entre le développeur et l'IA.
-->

---
layout: center
class: text-center
---

# Les tests ne vérifient pas votre code — ils le spécifient.

<br>

<v-clicks>

✅ L'injection de dépendances rend le code testable

✅ Les doublures remplacent les dépendances dans les tests

✅ On teste ce qui a de la valeur (ROI)

✅ Les tests sont le meilleur prompt pour l'IA

</v-clicks>

<br>

<v-click>

## TP — À vous de jouer ! 🧪

</v-click>

<!--
Conclusion de la séance. La phrase clé à retenir : les tests spécifient, ils ne vérifient pas a posteriori.
Transition vers le TP où les étudiants vont pratiquer l'injection de dépendances et les doublures.
-->
