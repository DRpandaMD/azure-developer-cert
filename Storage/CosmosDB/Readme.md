# CosmosDB Readme

## Key References

[Consitency Model](https://docs.microsoft.com/en-us/azure/cosmos-db/consistency-levels)

[Blog on Constiency Models](https://blog.jeremylikness.com/blog/2018-03-23_getting-behind-the-9ball-cosmosdb-consistency-levels/)

[Wiki on CAP Theorem ](https://en.wikipedia.org/wiki/CAP_theorem)

[Sample Queries ](https://docs.microsoft.com/en-us/azure/cosmos-db/sql-query-getting-started)

## General information

Azure Cosmos DB is a globally distrtibutied database service that's designed to provide low latency, elastic scalabilyt of throughput,
well-defind sematics for data consistency and high availability

The exam will test your understand of the consistency modles and knowledge that unstructured JSON
data can be stored and queired using the SQL API surface

[Home](/)


## Set up 

* You can use the commands in ```cloud-config.sh``` to get started

* To see the output of Azure locations use

```bash
az account list-locations -o table
```

* to work with a file with the grep'd output of names you can try

```bash
az account list-locations | grep "name"  > uslocations.txt
```

* When you are ready to make your own console app

```bash
dotnet new console -o ${App_name}
cd ${App_name}
dotnet build
```

## Additional NuGet Packages

```dotnet add package Microsoft.Azure.DocumentDB.Core --version 2.9.2```


[Home](/)


## SQL API

Azure CosmosDB with the SQL API supports storing and querying JSON documents using SQL dialect.


## Exam Topics

### AZ CLI -- az cosmosdb create


* You will most likely be asked about consistncy model, but in terms like: "you must ensure reads reflect the most recent write".  Therefore, answer will be strong consistnecy.

* The exam only focues on SQL API, so when asked to selec an API surface, its SQL!

* There should not be any C# but the CLI command to create DB


Create CosmosDB AZ CLI
```Bash
az cosmosdb create -g $RESOURCE_GROUP_NAME \
    --name $ACCOUNT_NAME \
    --kind GlobalDocumentDB \  
    --locations regionName=centralus isZoneRedundant=True \
    --locations regionName=eastus isZoneRedundant=False \
    --default-consistency-level Strong \
    --enable-multiple-write-locations true \
    --enable-automatic-failover true
```

Create Database AZ CLI
```Bash
az cosmosdb database create -g $RESOURCE_GROUP_NAME --name $ACCOUNT_NAME --db-name $DATABASE_NAME
```

[Home](/)

