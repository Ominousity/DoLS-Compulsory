#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Subtraction/SubtractionAPI/SubtractionAPI.csproj", "Subtraction/SubtractionAPI/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "./Subtraction/SubtractionAPI/SubtractionAPI.csproj"
COPY . .
WORKDIR "/src/Subtraction/SubtractionAPI"
RUN dotnet build "./SubtractionAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SubtractionAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SubtractionAPI.dll"]