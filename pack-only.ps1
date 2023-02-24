# run build

dotnet run --project cake/Build.csproj --do-test false --do-pack true --do-publish false --test-level 1
exit $LASTEXITCODE;