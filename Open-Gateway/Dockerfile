FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN ls -R

RUN dotnet restore Open-Gateway/Open-Gateway.csproj
RUN dotnet publish Open-Gateway/Open-Gateway.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "OpenGateway.dll"]