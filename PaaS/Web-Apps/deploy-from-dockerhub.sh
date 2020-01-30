export RESOURCE_GROUP = "webapps"
export PLAN_NAME = "dockerhubdeployasp"
export APP_NAME = "laaz203dockerhubdeploy"
export CONTAINER = "microsoft/dotnet-samples:aspnetapp"

az group create -n $RESOURCE_GROUP -l westus
az appservice plan create \
 -n $PLAN_NAME \
 -g $RESOURCE_GROUP \
 --sku B1 \
 --is-linux
 
az webapp create \
 -n $APP_NAME \
 -g $RESOURCE_GROUP \
 --plan $PLAN_NAME \
 --deployment-container-image-name $CONTAINER 
 
az webapp config appsettings set \
 -g $RESOURCE_GROUP \
 -n $APP_NAME \
 --settings WEBSITES_PORT=80

az webapp show -n $APP_NAME -g $RESOURCE_GROUP
az webapp show -n $APP_NAME -g $RESOURCE_GROUP --query "defaultHostName" -o tsv
az group delete -n $RESOURCE_GROUP --yes