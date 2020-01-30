export RESOURCE_GROUP = "webapps"
export $PLAN_NAME = "githubdeployasp"
export $APP_NAME = "laaz203githubdeploy"
export $REPO_URL = "https://github.com/Azure-Samples/php-docs-hello-world"

az group create \
 -n $RESOURCE_GROUP -l westus

az appservice plan create \
 -n $PLAN_NAME \
 -g $RESOURCE_GROUP \
 --sku FREE 

az webapp create \
 -n $APP_NAME \
 -g $RESOURCE_GROUP \
 --plan $PLAN_NAME 

az webapp deployment source config \
 -n $APP_NAME \
 -g $RESOURCE_GROUP \
 --repo-url $REPO_URL \
 --branch master \
 --manual-integration

az webapp deployment source show \
 -n $APP_NAME \
 -g $RESOURCE_GROUP

az webapp show \
 -n $APP_NAME \
 -g $RESOURCE_GROUP
 
az webapp show \
 -n $APP_NAME \
 -g $RESOURCE_GROUP \
 --query "defaultHostName" -o tsv

az webapp deployment source sync -n $APP_NAME -g $RESOURCE_GROUP
az group delete -n $RESOURCE_GROUP --yes