# run build

dotnet run --project cake/Build.csproj --do-test false --do-pack true --do-publish true --test-level 1
exit $LASTEXITCODE;