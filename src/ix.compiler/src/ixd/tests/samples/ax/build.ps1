dotnet run --project ..\..\..\ix.compiler\src\ixc\Ix.ixc.csproj --framework net7.0
$targetIP = [Environment]::GetEnvironmentVariable('AXTARGET')
$targetInput = [Environment]::GetEnvironmentVariable('AXTARGETPLATFORMINPUT')
apax build
apax sld --accept-security-disclaimer -t $targetIP -i $targetInput -r --default-server-interface