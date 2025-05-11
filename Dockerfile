# Usa la imagen oficial de .NET 8 para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia los archivos del proyecto y restaura las dependencias
COPY ./NoticiasAPI.sln ./
COPY ./NoticiasAPI/*.csproj ./NoticiasAPI/
WORKDIR /app/NoticiasAPI
RUN dotnet restore

# Copia el resto de los archivos del proyecto y compila la aplicación
COPY ./NoticiasAPI/. ./
RUN dotnet publish -c Release -o /app/publish

# Usa una imagen más ligera para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expone el puerto en el que la aplicación escucha
EXPOSE 80

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "NoticiasAPI.dll"]