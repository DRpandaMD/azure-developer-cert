using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure;

namespace app
{
    public class Tables
    {
        /*
        * You can get the connection string 
        */
        private static string _connectionString = "DefaultEndpointsProtocol=https;EndpointSuffix=core.windows.net;AccountName=storagetablesdemo;AccountKey=PomZ2XPywKfWmY+Q5bp9zJeNtzV6OZFpAcyVTdZ3albpGWgkwxwuT40u8GAVB2kGQaOSLOSI4fqRPeB5PwtHyQ==";

        public static async Task runDemoAsync()
        {
            //set up the storage account
            var storageAccount = CloudStorageAccount.Parse(_connectionString);
            // then using what we know about the storage account set up the tableClient to make actions on it
            var cloudTableClient = storageAccount.CreateCloudTableClient();
            // now lets get a reference to a Table called "Gamers"
            var gamersTable = cloudTableClient.GetTableReference("Gamers");
            //for ease of use I want to drop the table before I do anything
            DropTable(gamersTable);
            Thread.Sleep(5000);
            // now if the table doesn't exist yet create it
            await gamersTable.CreateIfNotExistsAsync();
            //now delete all the gamers in the table (table reset)
            //await DeleteAllGamersAsync(gamersTable);

           
           // add gamers to our table 
           // This is the method you would only add one at a time
           var gamer1 = new Gamer("tim@thegame.net", "EU", "Tim");
           await AddAsync(gamersTable, gamer1);

           //now lets say we want to add a bunch 
           var gamersList = new List<Gamer>
           {
                new Gamer("mike@zarate.net", "USA", "Mike", "666-6666"),
                new Gamer("mike@loser.org", "USA", "Mike", "000-0000")

           };

            await AddBatchAsync(gamersTable, gamersList);

            // now lets try pulling stuff out of the cloud
            var tim = await GetAsync<Gamer>(gamersTable, "EU", "tim@thegame.net");
            System.Console.WriteLine(tim);

            var getList = await FindGamersByNameAsync(gamersTable, "Mike");
            Console.WriteLine(getList);
        }

        /*
        * ****************** START CUSTOM FUNCTIONS ************************
        */
         public static void DropTable(CloudTable table)
        {
            //Note: DeleteIfExists returns a bool
            // true if the table dexisted in the storage service and has been deleted
            // otherwise its fale
            // I could write a while loop to try to fix the premtive flow
            bool deleteStatus = table.DeleteIfExists();
            while( deleteStatus != true)
            {
                 Console.Write("-----Trying to delete table------ \n ");
            }
            Console.WriteLine("-----Table Dropped Successfully---- \n");
        }

        public static async Task AddAsync<T> (CloudTable table, T entity) where T :TableEntity
        {
            var insertOpertion = TableOperation.Insert(entity);
            await table.ExecuteAsync(insertOpertion);
        }

        public static async Task AddBatchAsync<T> (CloudTable table, IEnumerable<T> entities) where T : TableEntity
        {
            var batchOperation = new TableBatchOperation();
            foreach (var entity in entities)
            {
                batchOperation.Insert(entity);
            }
            await table.ExecuteBatchAsync(batchOperation);
        }

        public static async Task<T> GetAsync<T> (CloudTable table, string pk, string rk) where T : TableEntity
        {
            var retrieve = TableOperation.Retrieve<Gamer>(pk, rk);
            var result = await table.ExecuteAsync(retrieve);
            return (T)result.Result;
        }

        /*
        * I will need to fix how this part works because its not
        * pulling Items off the table
        */
        public static async Task<List<Gamer>> FindGamersByNameAsync(CloudTable table, string name)
        {
            var filterCondition = TableQuery.GenerateFilterCondition("Name", QueryComparisons.Equal, name);
            var query = new TableQuery<Gamer>().Where(filterCondition);
            var result = await table.ExecuteQuerySegmentedAsync(query, null);
            return result.ToList();
        }
    }    
}