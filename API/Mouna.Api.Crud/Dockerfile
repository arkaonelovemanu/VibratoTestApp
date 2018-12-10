FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app

ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV ASPNETCORE_URLS=http://*:5000

EXPOSE 5000

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY Mouna.Api.Crud/Mouna.Api.Crud.csproj Mouna.Api.Crud/
RUN dotnet restore Mouna.Api.Crud/Mouna.Api.Crud.csproj
COPY . .
WORKDIR /src/Mouna.Api.Crud
RUN dotnet build Mouna.Api.Crud.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Mouna.Api.Crud.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Mouna.Api.Crud.dll"]
