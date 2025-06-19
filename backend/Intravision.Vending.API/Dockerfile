#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Intravision.Vending.API/Intravision.Vending.API.csproj", "Intravision.Vending.API/"]
COPY ["Intravision.Vending.Core/Intravision.Vending.Core.csproj", "Intravision.Vending.Core/"]
COPY ["Intravision.Vending.DAL/Intravision.Vending.DAL.csproj", "Intravision.Vending.DAL/"]
RUN dotnet restore "./Intravision.Vending.API/Intravision.Vending.API.csproj"
COPY . .
WORKDIR "/src/Intravision.Vending.API"
RUN dotnet build "./Intravision.Vending.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Intravision.Vending.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Intravision.Vending.API.dll"]
