# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Service/Ordering/Ordering.api/Ordering.API.csproj", "Service/Ordering/Ordering.api/"]
COPY ["Infrastructure/Common.Logging/Common.Logging.csproj", "Infrastructure/Common.Logging/"]
COPY ["Service/Ordering/Ordering.Application/Ordering.Application.csproj", "Service/Ordering/Ordering.Application/"]
COPY ["Infrastructure/EventBus.Messages/EventBus.Messages.csproj", "Infrastructure/EventBus.Messages/"]
COPY ["Service/Ordering/Ordering.Core/Ordering.Core.csproj", "Service/Ordering/Ordering.Core/"]
COPY ["Service/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Service/Ordering/Ordering.Infrastructure/"]
RUN dotnet restore "./Service/Ordering/Ordering.api/Ordering.API.csproj"
COPY . .
WORKDIR "/src/Service/Ordering/Ordering.api"
RUN dotnet build "./Ordering.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ordering.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]