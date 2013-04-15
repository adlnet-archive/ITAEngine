@echo off
call env.bat

echo Running Unit Tests
call nunit-console2 --xml=%UNIT_TEST_LOG% ITAEngineUnitTests\bin\Debug\ITAEngineUnitTests.DLL

