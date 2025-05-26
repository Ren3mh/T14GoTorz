#!/bin/bash

ADMIN_PASSWORD='Ucl123'
NETWORK_NAME="monitoring"

echo "🧼 Removing old Grafana container (if any)..."
docker rm -f grafana 2>/dev/null && echo "✅ Removed old Grafana." || echo "ℹ️  No Grafana container to remove."

if ! docker network ls | grep -q "$NETWORK_NAME"; then
  echo "🔌 Creating Docker network: $NETWORK_NAME"
  docker network create "$NETWORK_NAME"
else
  echo "🔌 Docker network '$NETWORK_NAME' already exists."
fi

if docker image inspect grafana/grafana > /dev/null 2>&1; then
  echo "🖼️ Grafana image already exists locally, skipping pull."
else
  echo "📦 Pulling latest Grafana image..."
  docker pull grafana/grafana
fi

echo "🚀 Running Grafana on http://localhost:3000..."
docker run -d --name grafana \
  --network "$NETWORK_NAME" \
  -p 3000:3000 \
  -e "GF_SECURITY_ADMIN_PASSWORD=$ADMIN_PASSWORD" \
  grafana/grafana

echo "🎉 Grafana is running! Login with user: admin, password: $ADMIN_PASSWORD"