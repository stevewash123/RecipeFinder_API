@echo off
echo Starting Recipe Finder Angular Frontend...
echo.

cd Frontend\recipe-finder

echo Checking if node_modules are installed...
if not exist node_modules (
    echo Installing npm dependencies...
    npm install
    if %ERRORLEVEL% neq 0 (
        echo.
        echo ERROR: npm install failed!
        pause
        exit /b 1
    )
)

echo.
echo Checking Angular CLI availability...
ng version >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo Angular CLI not found or not working properly.
    echo Please run fix-angular-windows.bat first.
    pause
    exit /b 1
)

echo.
echo Starting development server directly (skipping build step)...
echo Frontend will be available at: http://localhost:4200
echo.
echo Press Ctrl+C to stop the development server
echo.

ng serve --open

pause