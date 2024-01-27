REM Copy ../Art/icon.png to the current directory
copy /y ..\Art\icon.png .
REM Copy ../Art/manifest.json to the current directory
copy /y ..\Art\manifest.json .
REM Copy ../README.md to the current directory
copy /y ..\README.md .
REM Copy ../CHANGELOG.md to the current directory
copy /y ..\CHANGELOG.md .
REM Copy all files from ../FontFixer/build/bin/Debug to the current directory
xcopy /s /y /q ..\FontFixer\build\bin\Debug\* .

REM Create a zip file named FontFixer.zip containing all files (except build.bat) in the current directory
"C:\Program Files\7-Zip\7z.exe" a FontFixer.zip * -x!build.bat -x!FontFixer.zip

for %%I in (*) do if not "%%I"=="FontFixer.zip" if not "%%I"=="build.bat" del /q "%%I"
for /d %%D in (*) do if not "%%D"=="FontFixer.zip" if not "%%D"=="build.bat" rd /s /q "%%D"