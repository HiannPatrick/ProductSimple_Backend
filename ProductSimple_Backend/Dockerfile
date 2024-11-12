FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProductSimple_Backend/ProductSimple_Backend.csproj", "ProductSimple_Backend/"]
RUN dotnet restore "./ProductSimple_Backend/ProductSimple_Backend.csproj"
COPY . .
WORKDIR "/src/ProductSimple_Backend"
RUN dotnet build "./ProductSimple_Backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProductSimple_Backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductSimple_Backend.dll"]