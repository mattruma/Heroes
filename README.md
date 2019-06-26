# Introduction

Sample project demonstrating how to use a .NET Core Web API and Azure Functions for inserting, fetching, updating and deleting records.

## Setup

Execute the following commands in your Azure subscription at shell.azure.com:

```bash
prefix="YOUR PREFIX"

az group create --location eastus2 --name "${prefix}-heroes-rg"

az cosmosdb create --name "${prefix}-heroes-cosdbs" --resource-group "${prefix}-heroes-rg"

az cosmosdb database delete --db-name "heroes" --name "${prefix}-heroes-cosdbs" --resource-group-name "${prefix}-heroes-rg" --throughput 400

az cosmosdb collection create --collection-name "heroes" --db-name "heroes" --name "${prefix}-heroes-cosdbs" --resource-group-name "${prefix}-heroes-rg" --partition-key-path "/hero_id"
```

Execute the following script to get the Cosmos DB Connection String:

```bash
az cosmosdb list-connection-strings --name "${prefix}-heroes-cosdbs" --resource-group "${prefix}-heroes-rg"
```

### Heroes.FuncApp

Add a setting to the `local.settings.json` file called `AzureCosmosDocumentStoreOptions:ConnectionString` and replace it's value with the Cosmos DB Primary or Seconday Connection String.

Should look like the below:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "AzureCosmosDocumentStoreOptions:ConnectionString": "YOUR PRIMARY CONNECTION STRING"
  }
}
```

### Heroes.WebApi

Add a setting to the `appsettings.Development.json` file called `AzureCosmosDocumentStoreOptions:ConnectionString` and replace it's value with the Cosmos DB Primary or Seconday Connection String.

Should look like the below:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
  "AzureCosmosDocumentStoreOptions": {
    "ConnectionString": "YOUR PRIMARY CONNECTION STRING"
  }
}
```

To test the APIs, use PostMan and import the `Heroes API.postman_collection.json` collection.
