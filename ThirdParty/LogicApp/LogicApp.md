# Create an App Service Logic App

Azure Logic Apps implement event- driven, serverless, potentially long-running, workflows.
They can represent Complex processes instead of requiring quick and simple execution like Azure.

## Exception Handling

* default :: 4 retries exponentially increasing intervals scale by 7.5 seconds capped at 5 and 45 seconds

* exponential: waits a random interval selection from exponential growing  range before sending the next request

* Fixed: waits the specified interval before sending the next request

* None:  don't sent


## Exam Essentials

* you'll be asked to drag-drop activities onto a workflow picking the correct activities to build an app similar to the example.

* Retry policies and JSON are likely to be on the exam.  This will be in reference to talking to an external application and ask you to specify the type, interval, and retry count.

* Be able to select between the following :

  * Flow: it is not on the exam

  * Logic Apps: they are good for event driving processes that integrate with other declarative instead of code-first.

  * Functions: are light-weight and code-first

  * For Processing Queue Messages: pick Functions

  * WebJobs, they are kinda dated and are superseded by Azure Functions, but keep in mind that they are able to run in the same Azure DevOps environment as WebApps.

## Refereneces

* [What is Azure Logic Apps](https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-overview)

* [Azure Serverless Overview](https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-serverless-overview)

* [Logic App Error Handling](https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-exception-handling)

* [Chose the right integration and automation services](https://docs.microsoft.com/en-us/azure/azure-functions/functions-compare-logic-apps-ms-flow-webjobs)