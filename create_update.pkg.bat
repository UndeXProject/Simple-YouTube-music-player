@echo off
set /P ver="Enter new version: "
cd updater\bin\Release\
rename updater.exe _update
7z a -tzip -mx5 -r0 update.pkg _update
move update.pkg ..\..\..\update.pkg
rename _update updater.exe
cd ..\..\..\
echo %ver% > .version
echo Build update.pkg for version %ver% complite.
echo Open 7zip FM...
%SystemRoot%\explorer.exe "%CD%\Simple_YouTube_Music_Player\bin\Release"
7zFM update.pkg
echo Complite!
pause