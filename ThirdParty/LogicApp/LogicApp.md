# Create an App Service Logic App

Azure Logic Apps implement event- driven, serverless, potentiall long-running, workflows.
They can represent Complex processes instead of rquiring quick and simple execution like Azure.

## Exam Essentials

* you'll be asked to drag-drop activieties onto a workflow picking the correct activites to builud an app similar to the xample.

* Retry plicies and JSON are likely to be on the exam.  This will be in reference to talking to an external appliction and ask you to specify the type, interval, and retry count. 

* Be aple to slect between the following :

  * Flow: it is not on the exam

  * Logic Apps: they are good for event driving processes that integrate with other declrative instead of code-first.

  * Functions: are light-weight and code-first

  * For Processing Qeueue Messages: pick Functions

  * Webjobs, they are kinda dated and are superseded by Azure Functions, but keep in mind that they are able to run in the same Azure DevOps environment as WebApps.