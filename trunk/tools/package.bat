@ECHO on
:SETENV
CALL env.bat

:CREATE_DIR
if not exist %OUT_DIR% mkdir %OUT_DIR%

:FONT
ECHO start generate font
if not exist %TMP_DIR% mkdir %TMP_DIR%
%BIN_DIR%\FontGenerator.exe 22 22 8 8 %TRANSLATE_TEXT_DIR% %TMP_DIR%\font.brfnt ËÎÌו
ECHO copy font
if not exist %OUT_DIR%\MESS mkdir %OUT_DIR%\MESS
copy /Y %TMP_DIR%\font.brfnt %OUT_DIR%\MESS\FONT_A.BRFNT
copy /Y %TMP_DIR%\font.brfnt %OUT_DIR%\MESS\FONT_A_21.BRFNT

:TEXT
ECHO start handle text
CALL bmg_text.bat
ECHO end handle text

:IMAGE
%BIN_DIR%\PackageImg.exe %IMG_DIR% %IMG_DIR%\List.xml %TMP_IMG_DIR%

:PACKAGE_IMAGE
%BIN_DIR%\PackageACR.exe %WII_SDK_HOME%\x86\bin\darchD.exe %TMP_IMG_DIR% %OUT_DIR%\LYT

:PATCH_ISO
%BIN_DIR%\iso_patch.exe %OUT_DIR% %RAW_ISO% REVOLUTION/LANG/ENG