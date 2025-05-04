# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar solo los proyectos necesarios (API y Core)
COPY ControlEstudiantesAPI ./ControlEstudiantesAPI
COPY ControlEscolarCore ./ControlEscolarCore
COPY *.sln ./

# Restaurar solo el proyecto de API
RUN dotnet restore ControlEstudiantesAPI/ControlEstudiantesAPI.csproj

# Construir y publicar solo la API
RUN dotnet publish ControlEstudiantesAPI/ControlEstudiantesAPI.csproj -c Release -o out

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "ControlEstudiantesAPI.dll"]