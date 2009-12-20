@echo off
for /f "usebackq" %%a in (`dir /ad /b`) do call :compress_carc "%%a"
goto :end

:compress_carc
cd "%1"
setlocal enabledelayedexpansion
set fnames=
for /f "usebackq" %%b in (`dir /b`) do set fnames=!fnames! "%%b"
..\darchd -c %fnames% "..\%~n1.arc"
endlocal
cd ..
yaz0enc "%~n1.arc"
del "%~n1.carc.new"
ren "%~n1.arc.yaz0" "%~n1.carc.new"
del "%~n1.arc"
goto :eof

:end
echo on