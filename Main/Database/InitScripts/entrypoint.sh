#!/bin/bash

SCRIPT_DIR=$(dirname "$(readlink -f "$0")")

echo "Starting SQL Server"
/opt/mssql/bin/sqlservr & sleep 10
echo "SQL Server started"

echo "Setting up database"
$SCRIPT_DIR/setup_database.sh
echo "Database setup complete"

tail -f /dev/null

