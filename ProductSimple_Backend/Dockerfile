FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ProductSimple_Backend/ProductSimple_Backend.csproj", "ProductSimple_Backend/"]
RUN dotnet restore "ProductSimple_Backend/ProductSimple_Backend.csproj"
COPY . .
WORKDIR "/src/ProductSimple_Backend"
RUN dotnet build "ProductSimple_Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductSimple_Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductSimple_Backend.dll"]
