param(
    [Parameter(Mandatory = $true)]
    [int] $Year,
    [Parameter(Mandatory = $true)]
    [int] $Day
)

Write-Host "Downloading input"

$AuthCookie = Get-Content ".\cookie.txt"

$CurrentDirectory = Get-Location
$ProjectFolder = "${CurrentDirectory}\Years\${Year}\Day${Day}"
$InputFile = "${ProjectFolder}\Data1.txt"

Invoke-WebRequest -Uri "https://adventofcode.com/${Year}/day/${Day}/input" -Headers @{"cookie" = "session=${AuthCookie}" } -OutFile $InputFile
