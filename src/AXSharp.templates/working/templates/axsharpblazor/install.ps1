dotnet tool restore
cd ax
apax install -L
apax build
axcode .
axcode -g ..\README.md:0