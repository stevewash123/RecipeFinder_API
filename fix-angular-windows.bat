@echo off
echo ==========================================
echo    Fixing Angular Dependencies (Windows)
echo ==========================================
echo.

cd Frontend\recipe-finder

echo Step 1: Checking Node and npm versions...
node --version
npm --version

echo.
echo Step 2: Removing corrupted dependencies...
if exist node_modules rmdir /s /q node_modules
if exist package-lock.json del package-lock.json

echo.
echo Step 3: Clearing npm cache...
npm cache clean --force

echo.
echo Step 4: Installing fresh dependencies...
npm install

echo.
echo Step 5: Installing Angular CLI globally (if needed)...
npm install -g @angular/cli@latest

echo.
echo Step 6: Verifying Angular installation...
ng version

echo.
echo Step 7: Testing Angular build tools...
ng build --configuration development

echo.
echo Angular dependencies fixed! You can now run:
echo .\launch-both.bat
echo.
pause