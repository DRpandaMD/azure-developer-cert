# Notes for Kubernetes

* AKS is a fully managed Kubernets Container Orchestrator
* You will need to know how to use the AZ CLI 
* The source code for the Kubernetes app is found:

<https://github.com/Azure-Samples/azure-voting-app-redis>


* here are the instructions for working with K8s found in `create-aks.sh`


```bash
export RESOURCE_GROUP=aksdemo
export AKS_CLUSTER=demo-cluster

# Create resource group
az group create -n $RESOURCE_GROUP -l centralus

# Create AKS Cluster
az aks create -g $RESOURCE_GROUP -n $AKS_CLUSTER --node-count 1 --generate-ssh-keys --enable-addons monitoring

# Get the credentials for the cluster
az aks get-credentials -g $RESOURCE_GROUP -n $AKS_CLUSTER

# this out put will look like 'Merged "demo-cluster" as current context in /Users/USER_PROFILE/.kube/config

# now get the nodes from k8s
kubectl get nodes

# apply the deployment
kubectl apply -f azure-vote.yml

# watch the service 
kubectl get service azure-vote-front --watch


# clean up
az group delete -n $RESOURCE_GROUP
```


# Exam Tips
* Be familiar with the az aks create command paramters

[Home](https://github.com/DRpandaMD/azure-developer-cert)