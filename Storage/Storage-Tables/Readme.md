# Storage Tables for Azure Table Storage

* Azure Table Storage is a highly scalable, semi-structured, NoSQL key-value store.
* Access is via Rest and OData, with SDKs available for multiple languages and Platforms
* The exam focuses its question soley upon query functions.


# Set up
```bash
dotnet new console -o app
dotnet add package Microsoft.Azure.Storage.Common --version 11.1.1
dotnet add package Microsoft.Azure.Cosmos --version 3.5.1
dotnet add package Microsoft.Azure.CosmosDB.Table --version 2.1.2
```


## CRUD and Query Operations

