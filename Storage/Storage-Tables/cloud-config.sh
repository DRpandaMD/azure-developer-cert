# set up variables
export RG=tables
export LOCATION=centralus
export ACCOUNT_NAME=storagetablesdemo

# set up resource group
az group create -n $RG -l $LOCATION

# set up storage account
az storage account create -g $RG -n $ACCOUNT_NAME -l $LOCATION --sku Standard_LRS

# get connection string
az storage account show-connection-string -n $ACCOUNT_NAME --query "connectionString"

#clean up
az group delete -n $RG