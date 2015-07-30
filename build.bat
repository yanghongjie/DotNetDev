
@ECHO OFF
IF /I "%1"=="" GOTO BuildAll
IF /I "%1"=="Debug" GOTO BuildDebug
IF /I "%1"=="Release" GOTO BuildRelease

:BuildAll
msbuild /t:Rebuild /p:Configuration=Debug;TargetFrameworkVersion=v4.5;OutputPath=bin\Debug src\Dev\Dev.csproj
msbuild /t:Rebuild /p:Configuration=Debug;TargetFrameworkVersion=v4.5;OutputPath=bin\Debug src\Dev.Data\Dev.Data.csproj
msbuild /t:Rebuild /p:Configuration=Release;TargetFrameworkVersion=v4.5;OutputPath=bin\Release src\Dev\Dev.csproj
msbuild /t:Rebuild /p:Configuration=Release;TargetFrameworkVersion=v4.5;OutputPath=bin\Release src\Dev.Data\Dev.Data.csproj
GOTO End

:BuildDebug
msbuild /t:Rebuild /p:Configuration=Debug;TargetFrameworkVersion=v4.5;OutputPath=bin\Debug src\Dev\Dev.csproj
msbuild /t:Rebuild /p:Configuration=Debug;TargetFrameworkVersion=v4.5;OutputPath=bin\Debug src\Dev.Data\Dev.Data.csproj
GOTO End

:BuildRelease
msbuild /t:Rebuild /p:Configuration=Release;TargetFrameworkVersion=v4.5;OutputPath=bin\Release src\Dev\Dev.csproj
GOTO End

:End
@ECHO ON
pause
