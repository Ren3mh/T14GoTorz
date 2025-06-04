#!/bin/bash

NETWORK_NAME="redis_nw"

set -e  # Exit on first failure

if ! docker network ls | grep -q "$NETWORK_NAME"; then
  echo "🔌 Creating Docker network: $NETWORK_NAME"
  docker network create "$NETWORK_NAME"
else
  echo "🔌 Docker network '$NETWORK_NAME' already exists."
fi


echo "🔄 Starting Redis container"
docker compose -f compose.redis.yml up -d

echo "🎉 All services are running."

#echo "✅ Redis complete. Starting Redis Exporter"

#echo "🎉 All services are running. Exporter is live at http://localhost:0000/metrics"