@echo off
for %%a in (*.carc) do call :extract_carc "%%a"
goto :end

:extract_carc
del "%~1 0.rarc"
rd /s /q "%~n1"
yaz0dec "%~1"
darchd -x "%~n1" "%~1 0.rarc"
del "%~1 0.rarc"
goto :eof

:end
echo on