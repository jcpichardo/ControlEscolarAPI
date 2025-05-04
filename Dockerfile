# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar toda la solución
COPY . ./

# Restaurar dependencias
RUN dotnet restore

# Construir
RUN dotnet build -c Release -o out

# Publicar
RUN dotnet publish -c Release -o out

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "ControlEstudiantesAPI.dll"]