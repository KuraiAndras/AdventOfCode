param(
    [Parameter(Mandatory = $true)]
    [int] $Year,
    [Parameter(Mandatory = $true)]
    [int] $Day
)

$CurrentDirectory = Get-Location

$ProjectFolder = "${CurrentDirectory}\Years\${Year}\Day${Day}"
$InputFile = "${ProjectFolder}\Data1.txt"
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

Write-Host "Downloading input"

$AuthCookie = Get-Content ".\cookie.txt"

Invoke-WebRequest -Uri "https://adventofcode.com/${Year}/day/${Day}/input" -Headers @{"cookie" = "session=${AuthCookie}" } -OutFile $InputFile
