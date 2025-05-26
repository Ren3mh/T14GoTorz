#!/bin/bash

set -e  # Exit on first failure

# Configuration
MSSQL_USER="gotorz"
MSSQL_PASSWORD="P8#tM3@kL7nZ5&"

echo "🚀 Running MSSQL initialization container..."
docker compose -f compose.init.yml up --abort-on-container-exit --exit-code-from mssql_init

echo "🧹 Removing MSSQL init container..."
docker rm mssql_init || true

echo "✅ Init complete. Starting MSSQL Exporter with credentials..."

# Run the exporter with injected env vars
MSSQL_USER=$MSSQL_USER MSSQL_PASSWORD=$MSSQL_PASSWORD docker compose -f compose.exporter.yml up -d

echo "🎉 All services are running. Exporter is live at http://localhost:4000/metrics"