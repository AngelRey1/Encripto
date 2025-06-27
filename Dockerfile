# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia solo el .csproj y restaura dependencias
COPY ./CesarEncriptador/*.csproj ./CesarEncriptador/
RUN dotnet restore ./CesarEncriptador/CesarEncriptador.csproj

# Copia el resto del código y publica
COPY . .
WORKDIR /src/CesarEncriptador
RUN dotnet publish -c Release -o /app

# Imagen final
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "CesarEncriptador.dll"]