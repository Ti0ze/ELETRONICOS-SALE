@echo off
chcp 65001 >nul
echo === Verificando .NET SDK ===
dotnet --list-sdks
echo.
echo (Se nao aparecer nenhuma linha comecando com 8. acima, instale o .NET 8 SDK: https://dotnet.microsoft.com/download/dotnet/8.0)
echo.
pause

cd /d "%~dp0"
echo === Apagando bin e obj ===
rmdir /s /q bin 2>nul
rmdir /s /q obj 2>nul

echo === Restaurando e compilando ===
dotnet restore
dotnet build -c Debug

echo.
if exist "bin\Debug\net8.0\LojaEletronicos.exe" (
    echo OK! O executavel foi gerado. Pode voltar ao Visual Studio e apertar F5.
) else (
    echo O executavel NAO foi gerado. Se nao houve erro acima, o antivirus pode estar apagando o .exe.
    echo Adicione esta pasta as exclusoes do Windows Defender e rode de novo.
)
pause
