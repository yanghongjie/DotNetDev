
@ECHO OFF

mstest /testcontainer:tests\Test.Dev\bin\Debug\Test.Dev.dll /testcontainer:tests\Test.Dev.Data\bin\Debug\Test.Dev.Data.dll 

@ECHO ON
pause