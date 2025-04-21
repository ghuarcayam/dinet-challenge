# run-all.ps1

# 1. Validar Node.js
$requiredNodeVersion = [Version]"18.0.0"
try {
    $nodeVersionOutput = node -v
    $nodeVersion = [Version]($nodeVersionOutput -replace '[^0-9\.]', '')
    if ($nodeVersion -lt $requiredNodeVersion) {
        Write-Error "Node.js debe ser al menos versión $requiredNodeVersion. Versión actual: $nodeVersion"
        exit 1
    } else {
        Write-Host "✅ Node.js versión válida: $nodeVersion"
    }
} catch {
    Write-Error "❌ Node.js no está instalado."
    exit 1
}

# 2. Verificar certificado de desarrollo para HTTPS en .NET
Write-Host "`n🔐 Verificando certificado de desarrollo para HTTPS..."
dotnet dev-certs https --trust

# 3. Ejecutar Identity en https://localhost:6001
$identityPath = "$PSScriptRoot\src\API\identity\identity.csproj"
Start-Process powershell -ArgumentList "dotnet run --project `"$identityPath`" --urls=https://localhost:5001"
Write-Host "🚀 Identity lanzado en https://localhost:5001"

# 4. Ejecutar API en https://localhost:5001
$apiPath = "$PSScriptRoot\src\API\Challenge.API\Challenge.API.csproj"
Start-Process powershell -ArgumentList "dotnet run --project `"$apiPath`" --urls=https://localhost:44339"
Write-Host "🚀 API lanzado en https://localhost:44339"

# 5. Ejecutar Frontend Angular (HTTPS opcional)
$frontPath = "$PSScriptRoot\src\front"
Write-Host "`n🌐 Iniciando Frontend Angular..."
Start-Process powershell -WorkingDirectory $frontPath -ArgumentList "npm install; npm start"

Write-Host "`n✅ Todos los servicios se están ejecutando."
