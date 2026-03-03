# Les tests unitaires ne suffisent pas

<br>

<v-clicks>

Imaginez : **tous les tests unitaires passent**, mais...

- L'API retourne des erreurs 500 en production
- Les données ne sont pas sauvées en base
- La sérialisation JSON casse les dates
- La configuration de l'injection de dépendances est incorrecte

</v-clicks>

<v-click>

<br>

### Pourquoi ?

Les tests unitaires testent chaque composant **en isolation**.

Ils ne vérifient pas que les composants **fonctionnent ensemble**.

</v-click>

<!--
C'est le problème classique de l'intégration.
Chaque unité fonctionne parfaitement seule, mais le câblage entre les unités est cassé.
Les doublures (stubs, fakes) qu'on a appris en séance 3 masquent ces problèmes d'intégration.
-->

---

# L'analogie des engrenages

<br>

```
Tests unitaires :                    Tests d'intégration :
Chaque engrenage tourne bien seul    Les engrenages s'emboîtent-ils ?

  ⚙️ ✅  ⚙️ ✅  ⚙️ ✅               ⚙️──⚙️──⚙️ ✅ ?
```

<v-clicks>

Les tests unitaires vérifient que chaque pièce est correcte.

Les tests d'intégration vérifient que les pièces **s'assemblent correctement**.

Les deux sont nécessaires. Aucun ne remplace l'autre.

</v-clicks>

<!--
Métaphore simple mais efficace.
On peut avoir 100 engrenages parfaits individuellement, si les dents ne s'emboîtent pas, la machine ne tourne pas.
-->

---

# Que testent les tests d'intégration ?

<br>

<v-clicks>

La **plomberie** entre composants réels :

| Intégration | Ce qui peut casser |
|-------------|-------------------|
| **Repository ↔ Base de données** | Requêtes SQL incorrectes, mapping ORM, migrations |
| **Service ↔ API externe** | Sérialisation, headers, codes d'erreur |
| **Controller ↔ Middleware** | Routing, authentification, validation |
| **Configuration DI** | Dépendances manquantes, scoping incorrect |
| **Sérialisation JSON** | Formats de date, enums, propriétés nullable |

</v-clicks>

<!--
Ce sont tous des problèmes que les tests unitaires ne peuvent pas attraper car on utilise des doublures.
Un fake repository ne vérifie pas que votre requête LINQ se traduit correctement en SQL.
Un stub d'API ne vérifie pas que la sérialisation est correcte.
-->
