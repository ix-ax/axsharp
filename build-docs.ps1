# run build
dotnet run --project cake/Build.csproj -- $args --do-docs true
exit $LASTEXITCODE;