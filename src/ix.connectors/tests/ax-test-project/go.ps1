$targetIP = [Environment]::GetEnvironmentVariable('AXTARGET')
$targetInput = [Environment]::GetEnvironmentVariable('AXTARGETPLATFORMINPUT')
$targetIP
$targetInput
apax build
apax sld --accept-security-disclaimer -t $targetIP -i $targetInput -r --default-server-interface