Remove-Item -Path ".\bin\publish" -Recurse

dotnet publish -c Release -f "net8.0-windows" /p:PublishProfile="net8-x64"
dotnet publish -c Release -f "net8.0-windows" /p:PublishProfile="net8-x86"

dotnet publish -c Release -f "net6.0-windows" /p:PublishProfile="net6-x64"
dotnet publish -c Release -f "net6.0-windows" /p:PublishProfile="net6-x86"

Copy-Item -Path ".\bin\Release\net451\" ".\bin\Publish\" -Recurse -Force
Copy-Item -Path ".\bin\Release\net35\" ".\bin\Publish\" -Recurse -Force

$ver = "1.4.2"

Compress-Archive -Path ".\bin\publish\net8-x64\*" -DestinationPath ".\bin\publish\ppgui-net8-x64-$ver.zip" -Force
Compress-Archive -Path ".\bin\publish\net8-x86\*" -DestinationPath ".\bin\publish\ppgui-net8-x86-$ver.zip" -Force

Compress-Archive -Path ".\bin\publish\net6-x64\*" -DestinationPath ".\bin\publish\ppgui-net6-x64-$ver.zip" -Force
Compress-Archive -Path ".\bin\publish\net6-x86\*" -DestinationPath ".\bin\publish\ppgui-net6-x86-$ver.zip" -Force

Compress-Archive -Path ".\bin\publish\net451\*" -DestinationPath ".\bin\publish\ppgui-net451-$ver.zip" -Force
Compress-Archive -Path ".\bin\publish\net35\*" -DestinationPath ".\bin\publish\ppgui-net35-$ver.zip" -Force
