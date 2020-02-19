# Azure Search Documentation 

## Setup 

Set up the free tier search

```PowerShell
$rg = "search"
$location = "westus"
$serviceName = "laaz203search"

az group create -n $rg -l $location

az search service create `
 --name $serviceName `
 -g $rg `
 --sku free
```

For the C# API Code we need the ADMIN API Key, This will retrieve it:

```Powershell
az search admin-key show `
 --service-name $serviceName `
 -g $rg `
 --query "primaryKey"

```

Get the  QUERY API  Key :

```Powershell
az search query-key list `
 --service-name $serviceName `
 -g $rg `
 --query "[0].key"
```

Clean Up:

```PowerShell
az group delete -n $rg --yes
```