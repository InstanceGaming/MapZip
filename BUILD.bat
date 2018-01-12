@echo off
SET builderPath=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\
C:
if exist "%builderPath%MsBuild.exe" ( cd "%builderPath%"
MSBuild.exe %~dp0mapzip.sln /t:Rebuild /p:Configuration=Release /p:Platform="any cpu") else ( echo Could not find MSBuild.exe. Do you have Visual Studio installed?)
pause