param(
    [Parameter(Mandatory = $true)]
    [int] $Year,
    [Parameter(Mandatory = $true)]
    [int] $Day
)

$CurrentDirectory = Get-Location

$ProjectFolder = "${CurrentDirectory}\Years\${Year}\Day${Day}"
$ProjectFile = "${ProjectFolder}\Day${Day}.csproj"

Write-Host("Creating project at path: ${ProjectFile}")

if (Test-Path $ProjectFile) {
    Write-Host "Project file already exists"
    return
}

if (-Not (Test-Path -Path $ProjectFolder)) {
    New-Item -Path $ProjectFolder -ItemType Directory 
}

dotnet new console -o $ProjectFolder

dotnet sln add $ProjectFolder
