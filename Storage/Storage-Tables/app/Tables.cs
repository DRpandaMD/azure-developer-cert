using System;
using System.IO;
using System.Linq;
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
            // now if the table doesn't exist yet create it
            await gamersTable.CreateIfNotExistsAsync();
            //now delete all the gamers in the table (table reset)
            await DeleteAllGamersAsync(gamersTable);


        }

    }
}