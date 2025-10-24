@echo off
echo ==========================================
echo    Recipe Parser - Full Stack Launcher
echo ==========================================
echo.
echo This will start both the API and Frontend
echo API: http://localhost:5000
echo Frontend: http://localhost:4200
echo.

:: Check if we're in the right directory
if not exist "Backend\RecipeParser.Api" (
    echo ERROR: Backend folder not found!
    echo Please run this script from the RecipeParser root directory.
    pause
    exit /b 1
)

if not exist "Frontend\recipe-finder" (
    echo ERROR: Frontend folder not found!
    echo Please run this script from the RecipeParser root directory.
    pause
    exit /b 1
)

echo Starting API server in background...
echo.

:: Start API in a new window
start "Recipe Parser API" cmd /k "cd Backend\RecipeParser.Api && echo Building API... && dotnet build && if %ERRORLEVEL% equ 0 (echo Starting API server... && dotnet run) else (echo API build failed! && pause)"

:: Wait a moment for API to start
timeout /t 3 /nobreak > nul

echo Starting Angular frontend...
echo.

:: Navigate to frontend and start
cd Frontend\recipe-finder

:: Check if node_modules exist
if not exist node_modules (
    echo Installing npm dependencies...
    npm install
    if %ERRORLEVEL% neq 0 (
        echo ERROR: npm install failed!
        pause
        exit /b 1
    )
)

echo Building and starting Angular development server...
echo This will open your browser automatically when ready.
echo.

:: Start Angular dev server
ng serve --open

echo.
echo Both services have been started!
echo - API: http://localhost:5000 (check the API window)
echo - Frontend: http://localhost:4200
echo.
echo Close this window or press Ctrl+C to stop the frontend.
echo Close the API window separately to stop the backend.
pause