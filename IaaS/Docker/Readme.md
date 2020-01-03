# Notes for Docker

* For this exerice we will be building a .NETCore App and putting it into a docker container 

* Commands
```Bash
mkdir webapp
cd webapp
dotnet new mvc
dontnet build
dotnet run
```

* For information on how to build a dockerfile from Microsoft

* <https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-3.1>

```Bash
FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY aspnetapp/*.csproj ./aspnetapp/
RUN dotnet restore

# copy everything else and build app
COPY aspnetapp/. ./aspnetapp/
WORKDIR /app/aspnetapp
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /app/aspnetapp/out ./
ENTRYPOINT ["dotnet", "aspnetapp.dll"]
```


* To easily use this app I added a `docker-compose.yaml`
```Bash
docker-compose up --build -d
```

* navigate to `localhost:8080` 
* to clean up  run : `docker-compose down`