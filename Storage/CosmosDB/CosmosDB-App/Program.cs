using System;
using System.IO;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace CosmosDB_App
{
    class Program
    {
        /*
        *  Create database connection params
        */
        
        private static DocumentClient _client;
        private const string _databaseId = "TestDB";
        private const string _collectionId = "Families";

        private const string _endpoint = 
            "";
        
        private const string _key =
            "";

        /*
        *  Define Main() and the main execution function
        */

        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        private static async Task RunAsync()
        {
            // Create the document client
            _client = new DocumentClient(new Uri(_endpoint), _key );
            
            // Create the Database if its not there
            await _client.CreateDatabaseIfNotExistsAsync(new Database { Id = _databaseId});

            // create the document collection 
            await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_databaseId),
                new DocumentCollection {
                    Id = _collectionId,
                    PartitionKey = new PartitionKeyDefinition() {
                        Paths = new Collection<string>(new [] {"/id"})
                    }
                }
            ); // END _client.CreateDocumentCollectionIfNotExistAsync();

            // Create the family objects
            var family1 = JObject.Parse(File.ReadAllText("../Data/andersen.json"));
            var family2 = JObject.Parse(File.ReadAllText("../Data/wakefield.json"));

            // Now we create the documents in CosmosDB
            await CreateDocumentIfNotExistAsync(_databaseId, _collectionId, family1["id"].ToString(), family1);
            await CreateDocumentIfNotExistAsync(_databaseId, _collectionId, family2["id"].ToString(), family2);

            // Pull document from CosmosDB
            await GetDocumentByIdAsync(_databaseId, _collectionId, "AndersenFamily");
            await GetDocumentByIdAsync(_databaseId, _collectionId, "WakefieldFamily");

            /*
            *************************************  SQL EXECUTION AREA ****************************************
            */

            //SELECT ANDERSEN FAMILY DOCUMENT
            ExecuteSQLQuery(_databaseId, _collectionId, @"SELECT * FROM Families f WHERE f.id = 'AndersenFamily'");

            // Project the family name and city where the address city and state are the same value??
            ExecuteSQLQuery(_databaseId, _collectionId, @"SELECT {''Name'':f.id, ''City'':f.address.city} AS Family
                FROM Families f
                WHERE F.address.city = f.address.state
            ");

            //Get all children names whose family id matches WakefieldFamily and order by city of residence

            ExecuteSQLQuery(_databaseId, _collectionId, @" 
                SELECT c.givenName
                JOIN c IN f.children
                WHERE f.id = 'WakefieldFamily'
                ORDER BY f.address.city ASC
                ");
        }
        /*
        * Custom Functions to interact with Azure CosmosDB
        */
         private static async Task CreateDocumentIfNotExistAsync( string databaseId, string collectionId, string documentId, JObject data)
        {
            try
            {
                await _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, documentId),
                new RequestOptions
                {PartitionKey = new PartitionKey(documentId)});

                Console.WriteLine($"Family {documentId} already exists in the database");
            } 
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), data);
                    Console.WriteLine($"Created Family {documentId}");
                }
                else
                {
                    throw;
                }
            }
        } // END CreateDocumentIfNotExistAsync();

        private static async Task<string> GetDocumentByIdAsync(string databaseId, string collectionId, string documentId)
        {
            var response = await _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, documentId),
                new RequestOptions { PartitionKey = new PartitionKey(documentId)});
            
            Console.WriteLine(response.Resource);
            return response.Resource.ToString();
        }

        private static void ExecuteSQLQuery( string databaseId, string collectionId, string sql)
        {
            // first we want to check the SQL Statement
            Console.WriteLine("SQL: " + sql );
            // then we set some query options for azure
            var queryOptions = new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true };

            // then we set up our Query
            var sqlQuery = _client.CreateDocumentQuery<JObject>(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), sql, queryOptions);

            foreach (var result in sqlQuery)
            {
                Console.WriteLine(result);
            }
            
        }

    }// END CLASS PROGRAM
}// END NAMESPACE
