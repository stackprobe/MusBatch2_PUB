CALL C:\Dev\Factory\SetEnv.bat
Cx **
IF ERRORLEVEL 1 GOTO BLDERR

RDMD /RM out
RDMD /MD out\CUI
COPY src\*.exe out\CUI

GOTO END
:BLDERR
C:\app\MsgBox\MsgBox.exe E "BUILD ERROR"
:END
