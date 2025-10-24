@echo off
echo ==========================================
echo    Recipe Parser - Simple Launcher
echo ==========================================
echo.

echo Starting API server...
cd Backend\RecipeParser.Api
start /B dotnet run --urls=http://localhost:5000
echo API started at: http://localhost:5000

echo.
echo Waiting 3 seconds for API to initialize...
timeout /t 3 >nul

echo.
echo Starting Frontend...
cd ..\..\Frontend\recipe-finder

echo.
echo If Angular CLI issues persist, try these commands manually:
echo 1. npm install
echo 2. npx ng serve --port 4200
echo.

npx ng serve --port 4200 --open

pause