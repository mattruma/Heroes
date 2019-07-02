# Introduction

Sample project demonstrating how to use a .NET Core Web API and Azure Functions for inserting, fetching, updating and deleting records using Entity Framework.

## Setup

Execute the following commands in your Azure subscription at shell.azure.com:

```bash
prefix="YOUR PREFIX"
sqlPassword ="YOUR SQL PASSWORD"

az group create --location eastus2 --name "${prefix}-heroes-rg"

az sql server create --admin-password $sqlPassword --admin-user "${prefix}-heroes-sqldbs" --name "${prefix}-heroes-sqldbs" --resource-group "${prefix}-heroes-rg"

az sql db create --name "${prefix}-heroes-001-sqldb" --resource-group "${prefix}-heroes-rg" --server "${prefix}-heroes-sqldbs" --tier "Basic"

```

Execute the following script to get the SQL Server Connection String:

```bash
az sql db show-connection-string -s "${prefix}-heroes-sqldbs" -n "${prefix}-heroes-001-sqldb" -c ado.net
```

You will need to replace `<username>` with the admin-user name and `<password>` with the admin-password you used when you created the SQL Server.

Run the following sql script against your database to create the `heroes` table.

```
CREATE TABLE [dbo].[heroes](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[gender] [nvarchar](max) NULL,
	[powers] [nvarchar](max) NULL,
	[affiliations] [nvarchar](max) NULL,
	[notes] [nvarchar](max) NULL,
	[created_on] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_heroes] PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
```

### Heroes.FuncApp

Add a setting to the `local.settings.json` file called `SQLServer:ConnectionString` and replace it's value with the SQL Server Connection String.

Should look like the below:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "SQLServer:ConnectionString": "YOUR SQL SERVER CONNECTION STRING"
  }
}
```

### Heroes.WebApi

Add a setting to the `appsettings.Development.json` file called `SQLServer:ConnectionString` and replace it's value with the SQL Server Connection String.

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
  "SQLServer": {
    "ConnectionString": "YOUR SQL SERVER CONNECTION STRING"
  }
}
```

To test the APIs, use PostMan and import the `Heroes API.postman_collection.json` collection.
