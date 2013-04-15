@echo off
call env.bat

mkdir %LOG_DIR%
echo Building the ITAEngine Release
call %MONO_DIR%/xbuild /verbosity:quiet /t:Build /p:Configuration=Release /p:CscToolExe=gmcs.bat ITAEngine.sln > %BUILD_LOG%

echo Building the ITAEngine Debug
call %MONO_DIR%/xbuild /verbosity:quiet /t:Build /p:Configuration=Debug /p:CscToolExe=gmcs.bat ITAEngine.sln >> %BUILD_LOG%

echo Copying DLLs to Unity Application Directory
copy %TESTSCRIPT_DLL_DIR%\ITAEngine.dll  %MORSE_UNITY_DIR%\Assets
copy %TESTSCRIPT_DLL_DIR%\TestScriptCommon.dll  %MORSE_UNITY_DIR%\Assets

