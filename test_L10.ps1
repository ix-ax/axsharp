# run build

dotnet run --project cake/Build.csproj --do-test --do-pack --test-level 10
exit $LASTEXITCODE;