#!/usr/bin/env sh

set -e
set -x

project="e2etest"

cd "$(dirname "${0}")/.."

export COMPOSE_HTTP_TIMEOUT=200

docker-compose -p "$project" build

docker-compose -p "$project" up -d ea_api ea_webapp db chrome firefox selenium-hub
docker-compose -p "$project" up --no-deps ea_test

exit_code=$(docker inspect ea_test --format='{{.State.ExitCode}}')

if [ $exit_code -eq 0 ]; then
    exit $exit_code
else 
    echo "Test failed"
fi