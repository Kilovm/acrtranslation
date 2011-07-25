@ECHO on
:SETENV
CALL env.bat
:update
%BIN_DIR%\ExtractImgs.exe -c %WII_SDK_HOME%\x86\bin\darchD.exe %RAW_ISO_EXTRACTED_DIR%\LANG\ENG\LYT %EXTRACT_DIR%\tpl %EXTRACT_DIR%\png %EXTRACT_DIR%\list.xml