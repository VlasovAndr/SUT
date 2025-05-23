#!/usr/bin/env sh

set -e
set -x

project="e2etest"

cd "$(dirname "${0}")/.."

export COMPOSE_HTTP_TIMEOUT=200

docker compose -p "$project" build

mkdir -m 777 reports
# docker compose -p "$project" up --no-deps ea_int_test
# exit_code_inttests=$(docker inspect ea_int_test --format='{{.State.ExitCode}}')

# if [ $exit_code_inttests -eq 0 ]; then
#     exit $exit_code_inttests
# else 
#     echo "Integration tests failed"
#     exit 1
# fi

docker compose -p "$project" up -d ea_api ea_webapp db chrome firefox selenium-hub
docker compose -p "$project" up --no-deps ea_test

docker cp ea_test:/src/AutomationTests/TestProjectBDD/bin/Debug/net8.0/allure-results ./reports
echo "Allure results is copied to ./reports"
ls -l ./reports

exit_code_uitests=$(docker inspect ea_test --format='{{.State.ExitCode}}')

if [ $exit_code_uitests -eq 0 ]; then
    exit $exit_code_uitests
else 
    echo "BDD tests failed"
    exit 1
fi