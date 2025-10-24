# Recipe Parser - Boolean Query Builder

A portfolio project demonstrating Boolean logic parsing, tokenization, and query evaluation using a recipe search domain.

## 🚀 Quick Start

**Run the complete application:**
```cmd
.\launch-both.bat
```

**If Angular CLI issues occur:**
```cmd
Scripts\fix-cli.bat
.\launch-both.bat
```

This will start:
- **API**: http://localhost:5000 (with Swagger at /swagger)
- **Frontend**: http://localhost:4200 (opens automatically)

## 🏗️ Architecture
- **Backend**: C# ASP.NET Core Web API with Boolean query parser
- **Frontend**: Angular 18 with TypeScript
- **Data Source**: TheMealDB API integration
- **Key Feature**: **Text → AST → SQL** transformation pipeline

## 📋 Features

### 🆕 **Latest Updates**
- **SQL Generation**: Click "Show Generated SQL" to see Boolean queries transformed into executable SQL
- **Parser Education Dialog**: Click "What is a Parser?" for comprehensive explanation
- **Improved Layout**: 2x2 grid layout with better spacing and organization
- **Transformation Pipeline**: Visual demonstration of text → structure → code process

### Backend (C# ASP.NET Core)
- **Boolean Query Parser**: Handles AND, OR, NOT with parentheses
- **TheMealDB Integration**: Real recipe data from external API
- **REST Endpoints**: Search, validation, dropdown options
- **Parse Tree Visualization**: Shows query structure analysis
- **SQL Code Generation**: Transforms AST to executable SQL queries

### Frontend (Angular)
- **Interactive Query Builder**: Visual Boolean query construction with 2x2 grid
- **Real-time Query Display**: Shows Boolean expression as built
- **Hardcoded Dropdown Options**: Fast loading with real TheMealDB values
- **Include/Exclude Logic**: Toggle for cuisines and ingredients
- **Recipe Results**: Beautiful card layout with metadata
- **Dual Visualization**: Both parse tree AND generated SQL display

## 🎯 Portfolio Value

- **Parsing Pipeline**: Demonstrates complete text → structure → executable code transformation
- **Real-world Application**: Shows practical use of lexical analysis and syntax parsing
- **Educational Component**: Comprehensive explanation of parsing concepts for interviews
- **Modern Architecture**: Clean C#/Angular separation with proper API design
- **Production Patterns**: Follows enterprise development standards

## 📚 Documentation

- **[Setup Guide](../FULL-STACK-SETUP-GUIDE.md)** - Standard C#/Angular setup patterns and Windows/WSL best practices
- **[CLAUDE.md](CLAUDE.md)** - Project-specific development notes and implementation details

## 🔧 Manual Launch Options

**API Only:**
```cmd
Scripts\start-api.bat
```

**Frontend Only (after fixing CLI):**
```cmd
Scripts\fix-cli.bat
cd Frontend\recipe-finder
npm start
```

---

*For general C#/Angular project setup patterns, see [Full-Stack Setup Guide](../FULL-STACK-SETUP-GUIDE.md)*