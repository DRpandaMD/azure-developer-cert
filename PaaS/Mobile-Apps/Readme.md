# Azure App Services with Mobile Apps

**NOTE :: This app will require the FULL ASP.NET Framework**  

* I do not have this working as I do not use fullblown Visual Studio.

## Exam tips

* The only thing that will be covered will be the pattern for implementing offline data sync 

```Bash
PushAsync Because this method is a member of IMobileServicesSyncContext, changes across all tables are pushed to the backend. Only records with local changes are sent to the server.


PullAsync A pull is started from a IMobileServiceSyncTable. When there are tracked changes in the table, an implicit push is run to make sure that all tables in the local store along with relationships remain consistent. The pushOtherTables parameter controls whether other tables in the context are pushed in an implicit push. The query parameter takes an IMobileServiceTableQuery<T> or OData query string to filter the returned data. The queryId parameter is used to define incremental sync. For more information, see Offline Data Sync in Azure Mobile Apps.

PurgeAsync Your app should periodically call this method to purge stale data from the local store. Use the force parameter when you need to purge any changes that have not yet been synced.
```

### Sequence

* Push

* Pull

* Occationally Purge

## References

* [About Mobile Apps in Azure App Service](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-value-prop)
* [Create a Windows App](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-windows-store-dotnet-get-started)
* [Enable offline syc for your Windows App](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-windows-store-dotnet-get-started-offline-data)