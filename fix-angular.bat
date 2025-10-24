@echo off
echo Fixing Angular Dependencies...
echo.

cd Frontend\recipe-finder

echo Step 1: Removing corrupted node_modules...
if exist node_modules rmdir /s /q node_modules
if exist package-lock.json del package-lock.json

echo.
echo Step 2: Clearing npm cache...
npm cache clean --force

echo.
echo Step 3: Installing fresh dependencies...
npm install

echo.
echo Step 4: Verifying Angular installation...
npx ng version

echo.
echo Angular dependencies fixed! You can now run:
echo .\launch-both.bat
echo.
pause