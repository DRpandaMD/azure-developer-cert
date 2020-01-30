# Azure App Services and Web Apps

## Exam Focus

* The exam focuses on question around creating web applications using App Services and with a specific focus on create app servies with the CLI


```bash
# resource group create
az group create \
 -n $RESOURCE_GROUP -l westus

# appservice plan create
az appservice plan create \
 -n $PLAN_NAME \
 -g $RESOURCE_GROUP \
 --sku FREE 


# web app create
az webapp create \
 -n $APP_NAME \
 -g $RESOURCE_GROUP \
 --plan $PLAN_NAME 

```


## References

* [ Apps Services Documention](https://docs.microsoft.com/en-us/azure/app-service/)
* [Create web app with Github and AzureCLI](https://docs.microsoft.com/en-us/azure/app-service/scripts/cli-deploy-github)
* [App Services for Containers](https://azure.microsoft.com/en-us/services/app-service/containers/)
* [Deploy a Docker/Go web app in App Services for Containers](https://docs.microsoft.com/en-us/azure/app-service/containers/quickstart-docker)