# Le problème des contrats entre services

<br>

<v-clicks>

**Scénario :** deux équipes, deux services.

1. L'équipe A modifie le format de réponse de son API (`lastName` → `familyName`)
2. L'équipe B a des tests d'intégration... mais avec des **stubs**
3. Les tests de B passent ✅ — les stubs n'ont pas changé
4. En production : 💥 — le vrai service A renvoie un format différent

</v-clicks>

<v-click>

<br>

### Le problème

Les **doublures ne suivent pas l'évolution** du service qu'elles remplacent.

Il y a un **écart de contrat** entre ce que le consommateur attend et ce que le fournisseur produit.

</v-click>

<!--
C'est un problème classique en microservices.
Les tests unitaires et d'intégration ne détectent pas ce type de rupture car ils utilisent des doublures.
Il faut un mécanisme qui lie le consommateur et le fournisseur : le contract testing.
-->

---

# Contract Testing avec Pact

<br>

<v-clicks>

**Pact** : un framework de contract testing.

Le **consommateur** définit ses attentes → un fichier **pact** est généré.
Le **fournisseur** vérifie qu'il respecte le pact.

```
┌──────────────┐     Pact file      ┌──────────────┐
│  Consumer    │ ──────────────────► │  Provider    │
│  (Équipe B)  │   "Je m'attends    │  (Équipe A)  │
│              │    à ce format"     │              │
│  Génère le   │                     │  Vérifie le  │
│  contrat     │                     │  contrat     │
└──────────────┘                     └──────────────┘
```

</v-clicks>

<v-click>

```bash
dotnet add package PactNet
```

</v-click>

<!--
Pact est le standard de facto pour le contract testing.
Le fichier pact est un JSON qui décrit les interactions attendues (requête + réponse).
Il peut être partagé via un Pact Broker ou simplement via le système de fichiers.
-->

---

# Pact — Workflow

<br>

<v-clicks>

| Étape | Qui | Quoi |
|-------|-----|------|
| **1. Consumer test** | Équipe B | Écrit un test qui définit les interactions attendues → génère un fichier `.pact` |
| **2. Partage** | CI/CD | Le fichier pact est publié sur un **Pact Broker** (ou partagé via Git) |
| **3. Provider verification** | Équipe A | Exécute le pact contre son API réelle → vérifie la compatibilité |

</v-clicks>

<v-click>

### Quand l'utiliser ?

- **Microservices** avec des équipes indépendantes
- APIs consommées par des **clients que vous ne contrôlez pas**
- Besoin de **déployer indépendamment** sans tout tester ensemble

</v-click>

<v-click>

### Quand NE PAS l'utiliser ?

- Monolithe ou équipe unique — les tests d'intégration suffisent
- API interne avec un seul consommateur — la communication directe suffit

</v-click>

<!--
Le contract testing ne remplace pas les tests d'intégration.
Il comble un angle mort spécifique : la dérive des contrats entre services indépendants.
Dans un monolithe, les tests d'intégration détectent déjà ce type de problème car tout est compilé ensemble.
-->
