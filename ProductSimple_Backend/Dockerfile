# Usa a imagem base do ASP.NET Core Runtime para a execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8000

# Usa o SDK do .NET para a fase de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia o arquivo .sln e o .csproj
#COPY "../ProductSimple_Backend.sln" ./
COPY ./*.csproj ./

# Restaura as dependências
RUN dotnet restore "./ProductSimple_Backend.csproj"

# Instala o dotnet-ef para migrações
RUN dotnet tool install --global dotnet-ef --version 8.0.8
ENV PATH="$PATH:/root/.dotnet/tools"

# Copia todos os arquivos restantes
COPY . .

# Constrói o projeto
RUN dotnet build "./ProductSimple_Backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publica o projeto
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProductSimple_Backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Configuração final para execução
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ProductSimple_Backend.dll"]
