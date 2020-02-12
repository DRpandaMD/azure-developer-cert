# Azure Functions

The exam focuses questioning on Azure Functions around several characteristics of Storage Queue triggered functions these include:

* Identifying if a function is triggered by Storage Queue messages and if the function outputs to a Storage Table.

* How the queue tirgger handles recieving / scaling message processing.

* How the queue trigger handles exceptions thrown within the function.

## Exam Essentials 

* know that functions with a [Queue Trigger] attribute porcess Storage Queue Messages.

* Know that functions with a [Table ] attribute will output to a Storage Table

* Know the default for how mnay times a function will be retried if it throws an exception.

  * It will run try 5 times.  

  * If those fails, it will put the message in a new queue with the <original_Queue>-poison

* know the defaults for batch message recipt and that those will be processed in parallel. 

  * The auzre functions runtime will recieve up to 16 messages and run functions for each in parallel.

  * when the number being processed gets down to 8, the runtime gets another batch of 16 and process those.

  * any vm processing messages in the function app will only process a maximum of 24 messages


## References 

* [Work with Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=windows)

* [Azure Table storage bindings for Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-storage-table?tabs=csharp)

* [Azure Queue storage bindings for Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-storage-queue?tabs=csharp)
