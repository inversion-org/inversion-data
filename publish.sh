#!/bin/bash

if [[ "$TRAVIS_BRANCH" = "master" ]] && [[ "$TRAVIS_PULL_REQUEST" = "false" ]]; then

	pushd Inversion.Data/bin/Debug
	  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json || exit 1
	popd

	pushd Inversion.Data.AmazonSNS/bin/Debug
	  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json || exit 1
	popd

	pushd Inversion.Data.AmazonSQS/bin/Debug
	  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json || exit 1
	popd

	pushd Inversion.Data.Couchbase/bin/Debug
	  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json || exit 1
	popd

	pushd Inversion.Data.DynamoDB/bin/Debug
	  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json || exit 1
	popd

	pushd Inversion.Data.MongoDB/bin/Debug
	  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json || exit 1
	popd

	pushd Inversion.Data.Redis/bin/Debug
	  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json || exit 1
	popd

	pushd Inversion.Data.Sql/bin/Debug
	  dotnet nuget push *.nupkg -k $NUGET_KEY -s https://api.nuget.org/v3/index.json || exit 1
	popd
fi

