@echo off
pushd "%~dp0"
powershell Compress-7Zip "Shelf\bin\Release\net5.0-windows" -ArchiveFileName "Shelf_win10.zip" -Format Zip
:exit
popd