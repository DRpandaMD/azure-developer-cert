# Refactored from https://github.com/linuxacademy/content-az203-files/blob/master/storage/cosmosdb/config.ps1
# To be used with bash

export RESOURCE_GROUP_NAME=cosmosDB
export LOCATION=centralus
export ACCOUNT_NAME=cosmosDB
export DATABASE_NAME=TestDB


az group create -n $RESOURCE_GROUP_NAME -l $LOCATION

# Create a SQL API Cosmos DB account with session consistency and multi-master enabled
# az cosmosdb create -n myaccount -g mygroup \
# --locations regionName=eastus failoverPriority=0 isZoneRedundant=False 
# --locations regionName=uksouth failoverPriority=1 isZoneRedundant=True 
# --enable-multiple-write-locations
az cosmosdb create -g $RESOURCE_GROUP_NAME \
    --name $ACCOUNT_NAME \
    --kind GlobalDocumentDB \  
    --locations regionName=centralus isZoneRedundant=True \
    --locations regionName=eastus isZoneRedundant=False \
    --default-consistency-level Strong \
    --enable-multiple-write-locations true \
    --enable-automatic-failover true

# Create a database
az cosmosdb database create -g $RESOURCE_GROUP_NAME --name $ACCOUNT_NAME --db-name $DATABASE_NAME

# List account keys
az cosmosdb list-keys --name $ACCOUNT_NAME -g $RESOURCE_GROUP_NAME

# List account connection strings
az cosmosdb list-connection-strings --name $ACCOUNT_NAME -g $RESOURCE_GROUP_NAME

az cosmosdb show --name $ACCOUNT_NAME -g $RESOURCE_GROUP_NAME --query "documentEndpoint"

# Clean up
az group delete -y -g $RESOURCE_GROUP_NAME