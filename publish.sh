#!/bin/bash

pushd Inversion.Data/bin/Debug
  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json
popd

pushd Inversion.Data.AmazonSNS/bin/Debug
  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json
popd

pushd Inversion.Data.AmazonSQS/bin/Debug
  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json
popd

pushd Inversion.Data.Couchbase/bin/Debug
  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json
popd

pushd Inversion.Data.DynamoDB/bin/Debug
  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json
popd

pushd Inversion.Data.MongoDB/bin/Debug
  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json
popd

pushd Inversion.Data.Redis/bin/Debug
  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json
popd

pushd Inversion.Data.Sql/bin/Debug
  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json
popd