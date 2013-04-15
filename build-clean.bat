@echo off
SET PATH=C:\PROGRA~2\MONO-2~1.7\bin;%PATH%;

echo Clean the project
call xbuild /t:Clean  /p:Configuration=Debug ITAEngine.sln 

echo *****************************************************
call xbuild /t:Clean /p:Configuration=Release ITAEngine.sln

erase /S *~
erase *.swp
erase /S svn-*
erase /S *.dll
erase apps\UnityMorseMinimal\*.sln
erase apps\UnityMorseMinimal\*.csproj
erase apps\UnityMorseMinimal\*.pidb

rmdir /S /Q apps\UnityMorseMinimal\Library
rmdir /S /Q apps\UnityMorseMinimal\Temp
rmdir /S /Q latex 
rmdir /S /Q html
rmdir /S /Q test-results

