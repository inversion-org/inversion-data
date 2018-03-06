#!/bin/bash

dotnet restore

dotnet build

dotnet pack

pushd Inversion.Data/bin/debug
  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json
popd

pushd Inversion.Data.AmazonSNS/bin/debug
  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json
popd

pushd Inversion.Data.AmazonSQS/bin/debug
  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json
popd

pushd Inversion.Data.Couchbase/bin/debug
  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json
popd

pushd Inversion.Data.DynamoDB/bin/debug
  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json
popd

pushd Inversion.Data.MongoDB/bin/debug
  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json
popd

pushd Inversion.Data.Redis/bin/debug
  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json
popd

pushd Inversion.Data.Sql/bin/debug
  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json
popd