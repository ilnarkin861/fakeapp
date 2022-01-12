#!/bin/bash


read -p "Enter migration name : " migrationName

if [[ -z "$migrationName" ]]; then
    echo "Please enter migration name"
    exit 1
fi

cd FakeApp.Infrastructure/

dotnet ef --startup-project ../FakeApp.Api/ migrations add $migrationName

dotnet ef --startup-project ../FakeApp.Api/ database update
