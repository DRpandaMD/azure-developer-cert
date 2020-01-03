$rgName = "zarate-batch"
$stgAcctName = "zaratebatchstrgacct"
$location = "centralus"
$batchAcctName = "zaratebatchaccount"
$poolName = "zaratebatchpool"

# Start with exporting the variables and creatign the resource group
az group create  -l $location  -n $rgName

#create the storage accout
az storage account create  -g $rgName -n $stgAcctName  -l $location  --sku Standard_LRS

#create the batch account, yes they are different
az batch account create -n $batchAcctName  --storage-account $stgAcctName  -g $rgName -l $location


#create batch account login
az batch account login  -n $batchAcctName  -g $rgName  --shared-key-auth

#create batc pool
az batch pool create  --id $poolName  --vm-size Standard_A1_v2 --target-dedicated-nodes 2  --image  canonical:ubuntuserver:16.04-LTS  --node-agent-sku-id  "batch.node.ubuntu 16.04"

# check the status of our pool
az batch pool show  --pool-id $poolName  --query "allocationState"

# create the job
az batch job create  --id myjob  --pool-id $poolName

# define the job
for ($i=0; $i -lt 4; $i++) { az batch task create --task-id mytask$i --job-id myjob --command-line "/bin/bash -c 'printenv | grep AZ_BATCH; sleep 90s'" }

#show the task
az batch task show --job-id myjob --task-id mytask1

#list the files 
az batch task file list  --job-id myjob  --task-id mytask1  --output table


# send a task to batch job to download these files we just listed
az batch task file download  --job-id myjob  --task-id mytask0  --file-path stdout.txt  --destination ./stdout0.txt

#clean up
az batch pool delete --pool-id $poolName
az group delete -n $rgName