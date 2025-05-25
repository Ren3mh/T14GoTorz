#!/bin/bash

# Exit on any error
set -e

# Check for required IP argument
if [ -z "$1" ]; then
  echo "❌ Usage: $0 <HOST_IP>"
  exit 1
fi

HOST_IP="$1"
TODAYS_DATE=$(date +"%Y-%m-%d")

echo "🧹 Clean up old containers if they exist"
docker rm -f loadb prom 2>/dev/null || true

echo "📦 Building the Docker image for NGINX..."
docker build --tag "load-balancer:$TODAYS_DATE" -f Dockerfile.loadb .

echo "🚀 Starting the NGINX container..."
docker run -d --name loadb -p 80:80 -p 8080:8080 "load-balancer:$TODAYS_DATE"

echo "📈 Starting the NGINX Prometheus Exporter container..."
docker run -d \
  --name prom \
  -p 9113:9113 \
  nginx/nginx-prometheus-exporter:1.4.2 \
  --nginx.scrape-uri="http://$HOST_IP:8080/stub_status"

echo "🔍 Checking if containers are running..."
docker ps

echo "✅ All services are running."
echo "📊 Exporter metrics available at: http://$HOST_IP:9113/metrics"