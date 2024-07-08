#!/usr/bin/env sh

set -e
set -x

project="e2etest"

cd "$(dirname "${0}")/.."

export COMPOSE_HTTP_TIMEOUT=200

docker-compose -p "$project" build

mkdir -m 777 reports
docker-compose -p "$project" up -d ea_api ea_webapp db chrome firefox selenium-hub
docker-compose -p "$project" up --no-deps ea_test

docker cp ea_test:/src/AutomationTests/TestProjectBDD/bin/Debug/net7.0/LivingDoc.html ./reports
echo "Specflow LivingDoc report is copied to ./reports"
docker cp ea_test:/src/AutomationTests/TestProjectBDD/bin/Debug/net7.0/allure-results ./reports
echo "Allure results is copied to ./reports"
ls -l ./reports

exit_code=$(docker inspect ea_test --format='{{.State.ExitCode}}')

if [ $exit_code -eq 0 ]; then
    exit $exit_code
else 
    echo "Test failed"
fi