FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# کپی csproj
COPY Open-Gateway/Open-Gateway.csproj ./Open-Gateway/
WORKDIR /src/Open-Gateway

RUN dotnet restore

# کپی کل سورس
COPY Open-Gateway/. .

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "OpenGateway.dll"]