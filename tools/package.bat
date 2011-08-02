@ECHO on
:SETENV
CALL env.bat

:CREATE_DIR
if not exist %OUT_DIR% mkdir %OUT_DIR%

:FONT
ECHO start generate font
if not exist %TMP_DIR% mkdir %TMP_DIR%
rem %BIN_DIR%\FontGenerator.exe 23 23 8 8 %TEXT_DIR% %TMP_DIR%\font.brfnt 宋体
%BIN_DIR%\FontGenerator.exe 23 23 8 8 %TEXT_DIR% %OUT_DIR%\MESS\FONT_A.BRFNT 宋体
%BIN_DIR%\FontGenerator.exe 23 23 8 8 %TEXT_DIR% %OUT_DIR%\MESS\FONT_A_21.BRFNT 仿宋
%BIN_DIR%\FontGenerator.exe 21 21 8 8 %TEXT_DIR% %OUT_DIR%\MESS\FONT_B.BRFNT 黑体
%BIN_DIR%\FontGenerator.exe 18 18 8 8 %TEXT_DIR% %OUT_DIR%\MESS\FONT_B_16.BRFNT 楷体

:TEXT
ECHO start handle text
%BIN_DIR%\bmg.exe -drb %TEXT_DIR% %OUT_DIR%\MESS
ECHO end handle text

:IMAGE
ECHO start handle image
%BIN_DIR%\PackageImg.exe %EXTRACT_DIR%\png %EXTRACT_DIR%\List.xml %EXTRACT_DIR%\tpl
ECHO end handle image

:PACKAGE_IMAGE
ECHO start package image
%BIN_DIR%\PackageACR.exe %WII_SDK_HOME%\x86\bin\darchD.exe %EXTRACT_DIR%\tpl %OUT_DIR%\LYT
ECHO end package image

:PATCH_ISO
ECHO start patch iso
%BIN_DIR%\iso_patch.exe %OUT_DIR% %RAW_ISO% REVOLUTION/LANG/ENG
ECHO end patch iso