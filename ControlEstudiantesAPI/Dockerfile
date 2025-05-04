# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar csproj y restaurar dependencias
COPY ["ControlEstudiantesAPI/ControlEstudiantesAPI.csproj", "ControlEstudiantesAPI/"]
COPY ["ControlEscolarCore/ControlEscolarCore.csproj", "ControlEscolarCore/"]
RUN dotnet restore "ControlEstudiantesAPI/ControlEstudiantesAPI.csproj"

# Copiar todo y construir
COPY . .
WORKDIR "/src/ControlEstudiantesAPI"
RUN dotnet build "ControlEstudiantesAPI.csproj" -c Release -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "ControlEstudiantesAPI.csproj" -c Release -o /app/publish

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ControlEstudiantesAPI.dll"]