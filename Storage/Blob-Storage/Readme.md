# Azure Blob Storage with C# 

## Set up

```Bash
dotnet new console -o blob
dotnet build
dotnet run
dotnet add package Microsoft.Azure.Storage.Blob
dotnet add package Microsoft.Azure.Storage.Common
```

# Exam Essentials

* The exam tends to ask questions about controlling acces to blbos in a concurrency pattern.  this inovled using leavse to control access.
There are other mentions in documenttion about ETags, but those are more of a versionign scenario and lease are more correct for the exams.
* There should not be any C# Coding for blobs

Checkout this bash set up `blob-cloud-config.sh`

```bash
# blob storage config shell script

export RESOURCE_GROUP=blobs
export LOCATION=centralus
export ACCOUNT_NAME=zarateblobs

az group create -n $RESOURCE_GROUP -l $LOCATION

az storage account create \
    -g $RESOURCE_GROUP \
    -n $ACCOUNT_NAME \
    -l $LOCATION \
    --sku Standard_LRS


az storage account show-connection-string \
    -n $ACCOUNT_NAME \
    --query "connectionString"
```

# References 

* [Azure Quick Start](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet?tabs=linux)

* [Blob Consistency](https://azure.microsoft.com/en-us/blog/managing-concurrency-in-microsoft-azure-storage-2/)