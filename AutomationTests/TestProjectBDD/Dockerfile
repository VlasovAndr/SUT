#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AutomationTests/TestProjectBDD/TestProjectBDD.csproj", "AutomationTests/TestProjectBDD/"]
COPY ["AutomationTests/TestFramework/TestFramework.csproj", "AutomationTests/TestFramework/"]
COPY ["App/ProductAPI/ProductAPI.csproj", "App/ProductAPI/"]
RUN dotnet restore "AutomationTests/TestProjectBDD/TestProjectBDD.csproj"
COPY . .
WORKDIR "/src/AutomationTests/TestProjectBDD"
RUN dotnet build "TestProjectBDD.csproj" -c Release -o /app/build

FROM build AS TestProjectBDD
WORKDIR /src/TestProjectBDD
RUN  ["chmod", "+x", "..AutomationTests//scripts/WaitAndRun.sh"]
ENTRYPOINT [ "..AutomationTests//scripts/WaitAndRun.sh" ]
