using System;
using System.IO;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Collections.ObjectModel;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            _client = new DocumentClient(new Uri(_endpiont), _key );
            
            // Create the Database if its not there
            await _client.CreateDatabaseIfNotExistAsync(new Database { IDisposable = _databaseId});

            // create the document collection 
            await _client.CreateDocumentCollectionIfNotExistAsync(
                UriFactory.CreateDatabaseUri(_databaseId),
                new DocumentCollection {
                    Id = _collectionId,
                    PartitionKey = new ParitionKeyDefinition() {
                        Paths = new Collection<string>(new [] {"/id"})
                    }
                }
            ); // END _client.CreateDocumentCollectionIfNotExistAsync();

            // Create the family objects
            var family1 = JObject.Parse(File.ReadAllText("../Data/andersen.json"));
            var family2 = JObject.Parse(File.ReadAllText("../Data/wakefield.json"));

        }

    }
}
