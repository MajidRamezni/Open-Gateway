# مرحله Build
FROM mcr-mirror.liara.ir/dotnet/sdk:10.0 AS build
WORKDIR /src

# کپی فایل‌های csproj برای استفاده از قابلیت Cache داکر
COPY ["Open-Gateway/Open-Gateway.csproj", "API/"]
COPY ["nuget.config", "."]
COPY ["Open-Gateway.slnx", "."]
# ریستور کردن پکیج‌ها
RUN dotnet restore "Open-Gateway/Open-Gateway.csproj"

# کپی کردن کل کدهای پروژه
COPY . .

# ورود به پوشه API و انتشار (Publish)
WORKDIR "/src/Open-Gateway"
RUN dotnet publish "Open-Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

# مرحله نهایی (Runtime)
FROM mcr-mirror.liara.ir/dotnet/aspnet:latest AS base
WORKDIR /app
EXPOSE 8080

# کپی فایل‌های منتشر شده از مرحله قبل
COPY --from=build /app/publish .

# تنظیم کاربر غیر روت برای امنیت بیشتر
USER $APP_UID

ENTRYPOINT ["dotnet", "Open-Gateway.dll"]
