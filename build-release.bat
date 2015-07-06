@ECHO OFF

msbuild /t:Rebuild /p:Configuration=Release;TargetFrameworkVersion=v4.5;OutputPath=bin\Release src\Dev\Dev.csproj
msbuild /t:Rebuild /p:Configuration=Release;TargetFrameworkVersion=v4.5;OutputPath=bin\Release src\Dev.Data\Dev.Data.csproj
msbuild /t:Rebuild /p:Configuration=Release;TargetFrameworkVersion=v4.5;OutputPath=bin\Release src\Dev.Common\Dev.Common.csproj

@ECHO ON
pause
