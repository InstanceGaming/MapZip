@echo off
SET builderPath=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\
SET packagePath=%~dp0Installer\packages\Newtonsoft.Json.10.0.3\
SET webAddress=http://packages.nuget.org/api/v1/package/Newtonsoft.Json/
if exist "%packagePath%" ( goto :msb ) else ( goto :getpkgs )

:getpkgs
echo Getting NuGet package(s) from remote.
mkdir "%packagePath%"
powershell -Command "(New-Object Net.WebClient).DownloadFile('%webAddress%', '%~dp0package.zip')"
powershell -Command "Invoke-WebRequest '%webAddress%' -OutFile '%~dp0package.zip'"
cd "%~dp0Dist\7z\"
7za.exe x "%~dp0package.zip" -o"%packagePath%" -r
del /s /q "%~dp0package.zip"

:msb
C:
if exist "%builderPath%MsBuild.exe" ( cd "%builderPath%"
MSBuild.exe "%~dp0mapzip.sln" /t:Rebuild /p:Configuration=Release /p:Platform="any cpu") else ( echo Could not find MSBuild.exe. Do you have Visual Studio installed? )

pause