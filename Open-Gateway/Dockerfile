FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY . .

RUN dotnet restore OpenGateway.csproj
RUN dotnet publish OpenGateway.csproj -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /publish .

ENTRYPOINT ["dotnet", "OpenGateway.dll"]