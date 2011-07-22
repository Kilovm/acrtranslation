@ECHO on
:SETENV
CALL env.bat

:CREATE_DIR
if not exist %OUT_DIR% mkdir %OUT_DIR%

:FONT
ECHO start generate font
if not exist %TMP_DIR% mkdir %TMP_DIR%
%BIN_DIR%\FontGenerator.exe 22 22 8 8 %TEXT_DIR% %TMP_DIR%\font.brfnt ËÎÌו
ECHO copy font
if not exist %OUT_DIR%\MESS mkdir %OUT_DIR%\MESS
copy /Y %TMP_DIR%\font.brfnt %OUT_DIR%\MESS\FONT_A.BRFNT
copy /Y %TMP_DIR%\font.brfnt %OUT_DIR%\MESS\FONT_A_21.BRFNT

:TEXT
ECHO start handle text
%BIN_DIR%\bmg.exe -drb %TEXT_DIR% %OUT_DIR%\MESS
ECHO end handle text

:IMAGE
ECHO start handle image
%BIN_DIR%\PackageImg.exe %IMG_DIR% %IMG_DIR%\List.xml %TMP_DIR%\img
ECHO end handle image

:PACKAGE_IMAGE
ECHO start package image
%BIN_DIR%\PackageACR.exe %WII_SDK_HOME%\x86\bin\darchD.exe %TMP_DIR%\img %OUT_DIR%\LYT
ECHO end package image

:PATCH_ISO
ECHO start patch iso
%BIN_DIR%\iso_patch.exe %OUT_DIR% %RAW_ISO% REVOLUTION/LANG/ENG
ECHO end patch iso