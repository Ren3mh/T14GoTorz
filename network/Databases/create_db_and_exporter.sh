#!/bin/bash

set -e  # Exit on any error

echo "🔄 Starting MSSQL container stack..."
docker compose -f compose.mssql.yml up -d

echo "⏳ Waiting 120 seconds for MSSQL to initialize..."
sleep 120

echo "🚀 Running init container..."
docker compose -f compose.init.yml up --abort-on-container-exit --exit-code-from mssql_init

echo "✅ Init complete. Bringing up the exporter..."
docker compose -f compose.exporter.yml up -d

echo "🎉 All services are running."