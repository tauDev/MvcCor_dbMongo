FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY MvcCor_dbMongo.sln ./
COPY MvcCor_dbMongo/MvcCor_dbMongo.csproj MvcCor_dbMongo/
RUN dotnet restore -nowarn:msb3202,nu1503
#RUN dotnet restore
COPY . .
WORKDIR /src/MvcCor_dbMongo
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MvcCor_dbMongo.dll"]
