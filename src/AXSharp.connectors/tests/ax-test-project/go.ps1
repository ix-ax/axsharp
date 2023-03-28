$targetIP = [Environment]::GetEnvironmentVariable('AX_WEBAPI_TARGET')
$targetInput = [Environment]::GetEnvironmentVariable('AXTARGETPLATFORMINPUT')
$targetIP1500= [Environment]::GetEnvironmentVariable('AXTARGET1500');
$targetInput1500 = ".\bin\1500\"
apax build
apax sld --accept-security-disclaimer -t $targetIP -i $targetInput -r --default-server-interface
apax sld --accept-security-disclaimer -t $targetIP1500 -i $targetInput1500 -r --default-server-interface