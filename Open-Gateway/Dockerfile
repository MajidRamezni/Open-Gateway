FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY . .

RUN dotnet restore "Open-Gateway.csproj"
RUN dotnet publish "Open-Gateway.csproj" -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /publish .

ENTRYPOINT ["dotnet", "OpenGateway.dll"]