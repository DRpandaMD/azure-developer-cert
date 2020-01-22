using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace blob
{
    public class Blobs
    {
        public static string _connectionString = "";

        public static async Task RunAsync ()
        {   
            // we need to set up the cloud storage account from the connection string
            var storageAccount = CloudStorageAccount.Parse(_connectionString);
            // next with the storage Account Object create the Blob client
            var cloudBlobClient = storageAccount.CreateCloudBlobClient();
            // now with the blobClient object we need
            var cloudBlobContainer = cloudBlobClient.GetContainerReference("testcontainer");
            // now create the container
            await cloudBlobContainer.CreateAsync();


            // now we are going to configure the Blob much like S3 Buckets for public access
            var permissions = new BlobContainerPermissions 
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            // now to set the permission setting we just configured
            await cloudBlobContainer.SetPermissionsAsync(permissions);

            /*
            * Here we will start with a really basic program flow of creating a file
            * and then uploading it to the Blob storage
            */

            var localFileName = "Blob.txt";
            File.WriteAllText(localFileName, "Hello World! From AZ API and C#");

            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);
            await cloudBlockBlob.UploadFromFileAsync(localFileName);

            //list blobs in the container
            Console.WriteLine("Listing blobs in the container .... ");
            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var results = await cloudBlobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);
                blobContinuationToken = results.ContinuationToken;

                foreach (var item in results.Results)
                {
                    Console.WriteLine(item.Uri);
                }
            }
            while (blobContinuationToken != null);

            // Now lets try uploading a file

            var destinationFile = localFileName.Replace(".txt", "_DOWNLOADED.txt");
            await cloudBlockBlob.DownloadToFileAsync(destinationFile, FileMode.Create);

            var leaseId = Guid.NewGuid().ToString();
            File.WriteAllText(localFileName, "New Content");
            cloudBlockBlob.AcquireLease(TimeSpan.FromSeconds(30), leaseId);

            try
            {
                await cloudBlockBlob.UploadFromFileAsync(localFileName);
            
            }
            catch(StorageException ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }

            //wait a bit longer
            await Task.Delay(TimeSpan.FromSeconds(5));
            // upload it
            await cloudBlockBlob.UploadFromFileAsync(localFileName);
            // or release it
            await cloudBlockBlob.ReleaseLeaseAsync(new AccessCondition(){
                LeaseId = leaseId
            });

            //now clean up the container
            await cloudBlobContainer.DeleteIfExistsAsync();
        }
    }
}