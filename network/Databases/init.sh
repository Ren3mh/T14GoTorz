#!/bin/bash

MSSQL_HOST="mssql"
MSSQL_PORT="1433"
SA_USER="sa"
SA_PASSWORD="YourStrongPassword123"
NEW_USER="sander"
NEW_PASSWORD="P8#tM3@kL7\$nZ5&"  # Escaped for shell

MAX_RETRIES=60
RETRY_INTERVAL=5
ATTEMPT=1

echo "⏳ Waiting for MSSQL to accept '${NEW_USER}' login (max ${MAX_RETRIES} attempts)..."

while (( ATTEMPT <= MAX_RETRIES )); do
  echo "🔁 Attempt $ATTEMPT: Trying to log in as '${NEW_USER}'..."

  if /opt/mssql-tools/bin/sqlcmd -S ${MSSQL_HOST},${MSSQL_PORT} -U ${NEW_USER} -P "${NEW_PASSWORD}" -Q "SELECT 1" &> /dev/null; then
    echo "✅ User '${NEW_USER}' can log in. Initialization complete."
    exit 0
  fi

  echo "❌ Login failed. Attempting to run init SQL as SA..."

  /opt/mssql-tools/bin/sqlcmd -S ${MSSQL_HOST},${MSSQL_PORT} -U ${SA_USER} -P "${SA_PASSWORD}" -d master -Q "
    IF NOT EXISTS (SELECT * FROM sys.sql_logins WHERE name = '${NEW_USER}')
    BEGIN
      CREATE LOGIN [${NEW_USER}] 
      WITH PASSWORD = '${NEW_PASSWORD}', 
           DEFAULT_DATABASE = [master], 
           DEFAULT_LANGUAGE = [us_english], 
           CHECK_EXPIRATION = OFF, 
           CHECK_POLICY = ON;
      
      ALTER SERVER ROLE [sysadmin] ADD MEMBER [${NEW_USER}];
    END
  " || echo "⚠️ SQL execution failed."

  echo "🕐 Sleeping ${RETRY_INTERVAL}s before next attempt..."
  sleep $RETRY_INTERVAL
  ((ATTEMPT++))
done

echo "❌ Timeout: Failed to confirm login for user '${NEW_USER}' after ${MAX_RETRIES} attempts."
exit 1