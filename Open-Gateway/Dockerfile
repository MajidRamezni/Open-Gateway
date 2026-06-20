FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# فقط پروژه رو کپی کن (نه کل repo)
COPY Open-Gateway/*.csproj ./Open-Gateway/
WORKDIR /src/Open-Gateway

RUN dotnet restore

COPY Open-Gateway/. .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Open-Gateway.dll"]