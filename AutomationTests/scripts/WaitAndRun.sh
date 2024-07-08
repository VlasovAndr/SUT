#!/usr/bin/env sh

set -e
set -x

until [ ]; do
    dotnet "$1" && break
    sleep 1
done 

until [ ]; do
    sleep 30
    curl -f "http://selenium-hub:4444/wd/hub/status" && break
done 

dotnet test --logger "console;verbosity=detailed"
dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI
export PATH="$PATH:/root/.dotnet/tools"
echo "Starting Specflow living doc report generation"
cd /src/AutomationTests/TestProjectBDD/bin/Debug/net7.0/
livingdoc test-assembly "/src/AutomationTests/TestProjectBDD/bin/Debug/net7.0/TestProjectBDD.dll"
ls -l /src/AutomationTests/TestProjectBDD/bin/Debug/net7.0/