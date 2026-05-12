@echo off
echo Derleme baslatiliyor (Visual Studio MSBuild)...
"C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" /t:Rebuild /p:Configuration=Release
if %ERRORLEVEL% equ 0 (
    echo.
    echo Derleme basarili! Dosya yolu: bin\Release\ScreenWatcher.exe
) else (
    echo.
    echo Derleme sirasinda hata olustu.
)
pause
