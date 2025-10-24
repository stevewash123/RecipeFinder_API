@echo off
echo Starting Recipe Parser API...
echo.

cd Backend\RecipeParser.Api

echo Building the API project...
dotnet build
if %ERRORLEVEL% neq 0 (
    echo.
    echo ERROR: API build failed!
    echo Please check the compilation errors above.
    pause
    exit /b 1
)

echo.
echo API built successfully! Starting development server...
echo API will be available at: http://localhost:5000
echo Swagger UI will be available at: http://localhost:5000/swagger
echo.
echo Press Ctrl+C to stop the API server
echo.

dotnet run

pause