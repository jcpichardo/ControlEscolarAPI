# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar toda la solución primero
COPY . ./

# Listar los directorios para depuración
RUN ls -la

# Intentar construir solo el proyecto API
RUN find . -name "ControlEstudiantesAPI.csproj" -exec dotnet publish {} -c Release -o /app/out \;

# Si lo anterior falla, intentar buscar la API por nombre
RUN if [ ! -d /app/out ]; then \
    find . -name "*.csproj" | grep -i api | xargs -I {} dotnet publish {} -c Release -o /app/out; \
    fi

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
EXPOSE 443

# Buscar el nombre exacto del DLL de la API
RUN find . -name "*.dll" | grep -i api

# Entrypoint dinámico que busca el archivo API.dll
ENTRYPOINT ["sh", "-c", "dotnet $(find . -name \"*API.dll\" | head -1)"]