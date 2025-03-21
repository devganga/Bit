# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Bit/Directory.Packages.props", "Bit/"]
COPY ["Bit/Directory.Build.props", "Bit/"]
COPY ["Bit/src/Web/Web.csproj", "Bit/src/Web/"]
COPY ["Bit/src/Application/Application.csproj", "Bit/src/Application/"]
COPY ["Bit/src/Domain/Domain.csproj", "Bit/src/Domain/"]
COPY ["Bit/src/Infrastructure/Infrastructure.csproj", "Bit/src/Infrastructure/"]
COPY ["Bit/src/ServiceDefaults/ServiceDefaults.csproj", "Bit/src/ServiceDefaults/"]
RUN dotnet restore "./Bit/src/Web/Web.csproj"
COPY . .
WORKDIR "/src/Bit/src/Web"
RUN dotnet build "./Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bit.Web.dll"]