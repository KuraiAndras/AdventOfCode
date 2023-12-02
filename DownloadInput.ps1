Write-Host "Downloading input"

$AuthCookie = Get-Content ".\cookie.txt"

Invoke-WebRequest -Uri "https://adventofcode.com/${Year}/day/${Day}/input" -Headers @{"cookie" = "session=${AuthCookie}" } -OutFile $InputFile
