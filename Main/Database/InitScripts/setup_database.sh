#!/bin/bash

SCRIPT_DIR=$(dirname "$(readlink -f "$0")")

echo "Creating database"

./opt/mssql-tools18/bin/sqlcmd -S localhost -U "sa" -P "Password123" -i $SCRIPT_DIR/init-database.sql -C

echo "Database created"

