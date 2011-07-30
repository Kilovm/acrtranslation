@ECHO on
:SETENV
CALL env.bat
:update
%BIN_DIR%\ExtractImgs.exe -c --nocompress %EXTRACT_DIR%\tpl %EXTRACT_DIR%\png
%BIN_DIR%\PackageImg.exe %EXTRACT_DIR%\png %EXTRACT_DIR%\List.xml %EXTRACT_DIR%\tpl
%BIN_DIR%\ExtractImgs.exe -c --nocompress %EXTRACT_DIR%\tpl %EXTRACT_DIR%\png2