#!/bin/bash

set -e  # Exit on first failure

echo "🔄 Starting Redis container"
docker compose -f compose.redis.yml up -d

echo "🎉 All services are running."

#echo "✅ Redis complete. Starting Redis Exporter"

#echo "🎉 All services are running. Exporter is live at http://localhost:0000/metrics"