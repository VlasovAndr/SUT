#!/usr/bin/env sh

set -e
set -x

project="int_tests"

cd "$(dirname "${0}")/.."

export COMPOSE_HTTP_TIMEOUT=200

docker compose -p "$project" build
docker compose -p "$project" up ea_int_test

exit_code=$(docker inspect ea_int_test --format='{{.State.ExitCode}}')

if [ $exit_code -eq 0 ]; then
    exit $exit_code
else 
    echo "Integration tests failed"
    exit 1
fi
