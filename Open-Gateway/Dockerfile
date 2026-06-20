FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Open-Gateway/Open-Gateway.csproj Open-Gateway/
COPY Open-Gateway/ Open-Gateway/

WORKDIR /src/Open-Gateway

RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "OpenGateway.dll"]