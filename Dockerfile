# مرحلة بناء (SDK) لبناء التطبيقات
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MovieApp.csproj", "."]
RUN dotnet restore "./MovieApp.csproj"
COPY . . 
RUN dotnet publish "./MovieApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# المرحلة النهائية (Runtime فقط)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
# نسخ الملفات المنشورة
COPY --from=build /app/publish .
# نسخ مجلد الميجرشين إذا كان موجوداً (اختياري)
# COPY --from=build /src/Migrations ./Migrations
# تعيين نقطة الدخول
ENTRYPOINT ["dotnet", "MovieApp.dll"]