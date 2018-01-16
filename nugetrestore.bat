@echo off
SET packagePath=%~dp0Installer\packages\Newtonsoft.Json.10.0.3\
SET webAddress=http://packages.nuget.org/api/v1/package/Newtonsoft.Json/
C:
echo Getting NuGet package(s) from remote.
mkdir %packagePath%
powershell -Command "(New-Object Net.WebClient).DownloadFile('%webAddress%', '%~dp0package.zip')"
powershell -Command "Invoke-WebRequest %webAddress% -OutFile %~dp0package.zip"
%~dp0Dist\7z\7za.exe x %~dp0package.zip -o%packagePath% -r
del /s /q %~dp0package.zip 