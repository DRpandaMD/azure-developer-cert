FROM mcr.microsoft.com/dotnet/core/sdk:3.0 as build-env
WORKDIR /app

# copy in the .csproj you want to build
COPY ./dotnet-mvc-app/*.csproj ./
RUN dotnet restore

# copy everything else and build
COPY ./dotnet-mvc-app ./
RUN dotnet publish -c Release -o out


# Now to build the runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/out ./
ENTRYPOINT ["dotnet", "dotnet-mvc-app.dll"]