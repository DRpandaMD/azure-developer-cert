# Create an App Service Logic App

Azure Logic Apps implement event- driven, serverless, potentiall long-running, workflows.
They can represent Complex processes instead of rquiring quick and simple execution like Azure.


## Exception Handeling

* default :: 4 retries exponentially increaseing intervals scale by 7.5 seconds capped at 5 and 45 seconds

* exponetial: waits a random interval selction from exponetial growing  range before sending the next request

* Fixed: waits the specified interval before sending the next request

* None:  don't sent


## Exam Essentials

* you'll be asked to drag-drop activieties onto a workflow picking the correct activites to builud an app similar to the xample.

* Retry plicies and JSON are likely to be on the exam.  This will be in reference to talking to an external appliction and ask you to specify the type, interval, and retry count. 

* Be able to slect between the following :

  * Flow: it is not on the exam

  * Logic Apps: they are good for event driving processes that integrate with other declrative instead of code-first.

  * Functions: are light-weight and code-first

  * For Processing Qeueue Messages: pick Functions

  * WebJobs, they are kinda dated and are superseded by Azure Functions, but keep in mind that they are able to run in the same Azure DevOps environment as WebApps.

## Refereneces

* [What is Azure Logic Apps](https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-overview)

* [Azure Serverless Overview](https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-serverless-overview)

* [Logic App Error Handeling](https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-exception-handling)

* [Chose the right integration and automation services](https://docs.microsoft.com/en-us/azure/azure-functions/functions-compare-logic-apps-ms-flow-webjobs)