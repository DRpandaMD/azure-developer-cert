# blob storage config shell script

export RESOURCE_GROUP=blobs
export LOCATION=centralus
export ACCOUNT_NAME=blobs

az group create -n $RESOURCE_GROUP -l $LOCATION

az storage account create \
    -g $RESOURCE_GROUP \
    -n $ACCOUNT_NAME \
    -l $LOCATION \
    --sku Standard_LRS


az storage account show-connection-string \
    -n $ACCOUNT_NAME
    --query "connectionString"