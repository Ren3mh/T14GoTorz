#!/bin/bash

set -e  # Exit on first failure

echo "🔄 Starting MSSQL container stack..."
docker compose -f compose.mssql.yml up -d