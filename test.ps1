# run build

dotnet run --project cake/Build.csproj --do-test true --do-pack true --test-level 10
exit $LASTEXITCODE;