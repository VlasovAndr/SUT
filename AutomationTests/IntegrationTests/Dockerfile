FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AutomationTests/IntegrationTests/IntegrationTests.csproj", "AutomationTests/IntegrationTests/"]
COPY ["App/ProductAPI/ProductAPI.csproj", "App/ProductAPI/"]
RUN dotnet restore "./AutomationTests/IntegrationTests/IntegrationTests.csproj"
COPY . .
WORKDIR "/src/AutomationTests/IntegrationTests"
RUN dotnet build "./IntegrationTests.csproj" -c Release -o /app/build

FROM build AS integrationtests
WORKDIR /src/AutomationTests/IntegrationTests
CMD ["dotnet", "test", "--filter", "Category=IntegrationTests", "--no-build", "--verbosity", "detailed"]
