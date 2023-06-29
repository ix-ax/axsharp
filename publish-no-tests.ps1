# run build

dotnet run --project cake/Build.csproj --do-pack --do-publish --test-level 1
exit $LASTEXITCODE;