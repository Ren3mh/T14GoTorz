#!/bin/bash

# Set today's date in YYYY-MM-DD format
TODAY=$(date +%F)
NETWORK_NAME="monitoring"
IMAGE_NAME="prom"

echo "📅 Building and tagging image with today's date: $TODAY"
docker build -f Dockerfile.prometheus -t $IMAGE_NAME:$TODAY .

echo "🛑 Stopping and removing existing container named 'prom' (if any)..."
docker rm -f prom 2>/dev/null && echo "✅ Removed old container." || echo "ℹ️  No existing container to remove."

if ! docker network ls | grep -q "$NETWORK_NAME"; then
  echo "🔌 Creating Docker network: $NETWORK_NAME"
  docker network create "$NETWORK_NAME"
else
  echo "🔌 Docker network '$NETWORK_NAME' already exists."
fi

echo "🚀 Running new Prometheus container..."
docker run -d --name prom \
  --network "$NETWORK_NAME" \
  -p 9090:9090 \
  -v prometheus_data:/prometheus \
  $IMAGE_NAME:$TODAY

# ✅ Summary
echo "✅ Prometheus is now running!"
echo "🌐 Access Prometheus at: http://localhost:9090"
echo "🔗 Connected to Docker network: $NETWORK_NAME"
echo "📆 Retention: 30 days (configured in Dockerfile)"