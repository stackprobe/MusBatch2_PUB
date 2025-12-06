CALL C:\Dev\Factory\SetEnv.bat
runsub Release

MD C:\temp
RDMD /RM C:\temp\Release
RDMD /MD C:\temp\Release\out

C:\apps\Compress\Compress.exe ^
	CUI\out\CUI ^
	C:\temp\Release\out\CUI.cmp-gz
C:\apps\Compress\Compress.exe ^
	GUI\out\GUI ^
	C:\temp\Release\out\GUI.cmp-gz

sha512 /O ^
	C:\temp\Release\out\CUI.cmp-gz ^
	C:\temp\Release\out\CUI.cmp-gz.hash
sha512 /O ^
	C:\temp\Release\out\GUI.cmp-gz ^
	C:\temp\Release\out\GUI.cmp-gz.hash

SFCP /CDO ^
	Installer\out\Installer ^
	C:\temp\Release\out

SFCP /MD ^
	C:\temp\Release\out ^
	C:\temp\Release\MusBatch

z7 /C C:\temp\Release\MusBatch

START C:\temp\Release

TIMEOUT 2
