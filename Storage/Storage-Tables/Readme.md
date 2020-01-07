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

### Define Table Entity

* Defines an entity by deriving from the Table Entity Class
* Properties with public get / set both map to the table
* Needs a public parameterless constructor

```C#
public class Gamer :TableEntity
{
    public string Region    { get {return this.PartitionKey; }}
    public string Email     { get {return this.RowKey;}}
    public string Name      { get; set; }
    public string Phone     { get; set; }

    public Gamer () {}

    public Gamer (string email, string region, string name, string phone = null)
    {
        this.PartionKey = region;
        this.RowKey = email;
        this.Name = name;
        this.Phone = phone;
    }

    public override string ToString()
    {
        return $"Region(pk):{Region}  \n Email(rk):{Email}  \n Name:{Name} \n Phone:{Phone}";
    }
}
```

### Get Storage Account

* Get a storage account object

```C#
var storageAccount = CloudStorageAccount.Parse(_connectionString);
```

### Get Cloud Table Client

* The class to talk with the table service REST API

```C#
var tableClient = storageAccount.CreateCloudTableClient ();
```

### Get Table Reference

* Get a reference to the table
* Create the table if it does not exist

```C#
var gamersTable = tableClient.GetTableReference("Gmers");
await gamersTable.CreateIfNotExistsAsync();
```

### Insert Single Entity

* Delcare a new entity object, specify PK / RK
* Create a new Insert TableOperation
* have the table execute the insert table operation

```C#
var gamer1 = new Gamer("blue@example.net", "France", "Bleu");
await AddAsync(gamersTable, gamer1);

public static async Task AddAsync<T> (CloudTable table, T entity) where T : TableEntity
{
    var insertOperation = TableOperation.Insert(entity);
    await table.ExecuteAsysnc(insertOperation);
}
```

### Insert Batch

* Insert multiple entities in a single REST call
* Much more effecient for more than one entity

```C#
var gamers = new List<Gamer> 
{
    new Gamer("mike@games.net", "US", "Mike", "555-1110"),
    new Gamer("mike@thegame.com", "US", "Mike", "555-1220")
};

await AddBatchAsync(gamersTable, gamers);

public static async Task AddBatchAsync<T>(CloudTable table, IEnumerable<T> entities) where T : TableEntity
{
    var batchoperation = new TableBatchOperation();
    foreach (var entity in entities)
    {
        batchOperation.Insert(entity);
    }
    await table.ExecuteBatchAsync(batchOperation);
}

```

### Get Object by PK / RK

* Create a Retrieve TableOperation, specifying the primary and row key
* Execute the retrive on the Table
* TypeCast the result to the desired Object type

```C#

var blue = GetAsync<Gamer>(gamersTable, "France", "bleu@game.net");
Console.WriteLine(blue)

public static async Task<T> GetAsync<T>(CloudTable table, string pk, string rk) where T :TableEntity
{
    var retrieve = TableOperation.Retrieve<Gamer>(pk, rk)
    var result = await table.ExecuteAsync(retrieve);
    return (T)result.Result();
}
```

### Query Entity -- This one is imporatnt for the exam.

* Use TableQuery.GenerateFiltercondition to specify a codition of a column and a value
* Then pass that condition to a TableQuery.Where function call
* And have the table ExecuteQuerySegmentedAsync
* Cast the result to a collection to execute the query

```C#
gamers = await FindGamersByNameAsync(gamersTable, "Mike");
gamers.Foreach(Console.WriteLine);

public static async Task<List<Gamer>>FindGamerByNameAsync(CloudTable table, string name)
{
    var filterCondition = TableQuery.GnerateFilterCondition("Name", QueryComparisons.Equal, name);
    var query = new TableQuery<Gamer>().Where(filterCondition);
    var results = await table.ExecuteQuerySegmentedAsync(query, null);
    return results.ToList();
}

```

### Delete Entity


* Delete is perfomred by passing the tableEntity object to a TableOperation.Delete executying that operation on the table


```C#
var gamer1 = new Gamer("blue@game.net", "France", "Blue");
await DeleteAsync(gamersTable, gamer1);

public static async Task DeleteAsync<T>(CloudTable table, T entity) where T : TableEntity
{
    var retrieve = TableOperation.Delete(entity);
    await table.ExecuteAsync(retrieve);
}
```


## Exam Essentials

* The exam will focus on perfomring a TableQuery.
