#!/bin/bash

$ADMIN_PASSWORD='Ucl123'

echo "🧼 Removing old Grafana container (if any)..."
docker rm -f grafana 2>/dev/null && echo "✅ Removed old Grafana." || echo "ℹ️  No Grafana container to remove."

echo "📦 Pulling latest Grafana image..."
docker pull grafana/grafana

echo "🚀 Running Grafana on http://localhost:3000..."
docker run -d --name grafana \
  --network monitoring \
  -p 3000:3000 \
  -e "GF_SECURITY_ADMIN_PASSWORD=$ADMIN_PASSWORD" \
  grafana/grafana

echo "🎉 Grafana is running! Login with user: admin, password: $ADMIN_PASSWORD"