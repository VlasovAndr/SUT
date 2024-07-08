#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TestProjectBDD/TestProjectBDD.csproj", "TestProjectBDD/"]
COPY ["TestFramework/TestFramework.csproj", "TestFramework/"]
COPY ["ProductAPI/ProductAPI.csproj", "ProductAPI/"]
RUN dotnet restore "TestProjectBDD/TestProjectBDD.csproj"
COPY . .
WORKDIR "/src/TestProjectBDD"
RUN dotnet build "TestProjectBDD.csproj" -c Release -o /app/build

FROM build AS TestProjectBDD
WORKDIR /src/TestProjectBDD
RUN  ["chmod", "+x", "../scripts/WaitAndRun.sh"]
ENTRYPOINT [ "../scripts/WaitAndRun.sh" ]
