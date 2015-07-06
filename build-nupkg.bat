@ECHO OFF

nuget pack src\Dev\Dev.csproj -Prop Configuration=Release
nuget pack src\Dev.Data\Dev.Data.csproj -Prop Configuration=Release
nuget pack src\Dev.Common\Dev.Common.csproj -Prop Configuration=Release

@ECHO ON
pause