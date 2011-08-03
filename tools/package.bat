@ECHO on
:SETENV
CALL env.bat

:CREATE_DIR
if not exist %OUT_DIR% mkdir %OUT_DIR%
if not exist %OUT_DIR%\MESS mkdir %OUT_DIR%\MESS

:FONT
ECHO start generate font
%BIN_DIR%\FontGenerator.exe 23 23 8 8 %TEXT_DIR% %OUT_DIR%\MESS\FONT_A.BRFNT ËÎÌå
%BIN_DIR%\FontGenerator.exe 23 23 8 8 %TEXT_DIR% %OUT_DIR%\MESS\FONT_A_21.BRFNT ·ÂËÎ
%BIN_DIR%\FontGenerator.exe 21 21 8 8 %TEXT_DIR% %OUT_DIR%\MESS\FONT_B.BRFNT ºÚÌå
%BIN_DIR%\FontGenerator.exe 18 18 8 8 %TEXT_DIR% %OUT_DIR%\MESS\FONT_B_16.BRFNT ¿¬Ìå

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