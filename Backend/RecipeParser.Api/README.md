# Recipe Finder - ASP.NET Core API

**Lexical Parser Demonstration: Recipe Finder** - C# ASP.NET Core Web API Backend

A modern Web API demonstrating Boolean query parsing, AST generation, and SQL code generation using the Superpower parsing library.

## 🚀 Quick Start

```bash
# Restore packages
dotnet restore

# Run the API
dotnet run --urls=http://localhost:5000
```

API will be available at:
- **Base URL**: http://localhost:5000
- **Swagger Documentation**: http://localhost:5000/swagger

## 🏗️ Architecture

### **Backend Stack**
- **ASP.NET Core 8** Web API
- **Superpower** parsing library for Boolean expressions
- **TheMealDB API** integration for real recipe data
- **Swagger/OpenAPI** documentation
- **CORS** configured for Angular frontend

### **Key Components**
- **Boolean Query Parser**: Handles AND, OR, NOT operators with parentheses
- **AST Generation**: Creates Abstract Syntax Trees from Boolean expressions
- **SQL Code Generation**: Transforms parsed queries into executable SQL
- **REST API Endpoints**: Search, validation, and dropdown options

## 📋 API Endpoints

### **Recipe Search**
```http
POST /api/recipe/search
Content-Type: application/json

{
  "query": "italian AND (chicken OR pasta) AND NOT seafood"
}
```

**Response:**
```json
{
  "query": "italian AND (chicken OR pasta) AND NOT seafood",
  "results": [...],
  "parseTree": {...},
  "generatedSQL": "SELECT r.*, c.CategoryName...",
  "executionTime": "15ms",
  "resultCount": 42
}
```

### **Query Validation**
```http
GET /api/recipe/validate?query=chicken AND pasta
```

### **Dropdown Options**
```http
GET /api/recipe/dropdown-options
```

### **Recipe Details**
```http
GET /api/recipe/{id}
```

## 🔍 Parser Features

### **Boolean Expression Support**
- **Operators**: AND, OR, NOT
- **Parentheses**: Complex grouping `(italian OR french) AND NOT seafood`
- **Include/Exclude**: Natural language support for dietary restrictions
- **Error Handling**: Comprehensive syntax validation

### **AST Generation**
```csharp
var parseResult = SimpleRecipeParser.ParseQuery(query);
// Generates structured Abstract Syntax Tree
```

### **SQL Code Generation**
```csharp
var sql = GenerateSQL(query);
// Transforms: "italian AND chicken"
// Into: "SELECT r.* FROM Recipes r WHERE a.AreaName = 'italian' AND i.IngredientName LIKE '%chicken%'"
```

## 🎯 Portfolio Value

### **Technical Demonstrations**
- **Lexical Analysis**: Tokenization and parsing with Superpower library
- **AST Construction**: Building and traversing Abstract Syntax Trees
- **Code Generation**: Transform parsed structures into executable SQL
- **Modern API Design**: RESTful endpoints with comprehensive documentation

### **Computer Science Concepts**
- **Compiler Theory**: Practical application of parsing techniques
- **Language Processing**: Boolean expression evaluation
- **Tree Structures**: AST manipulation and traversal
- **Code Transformation**: Text → Structure → Executable Code pipeline

## 🔗 Related Repositories

- **🔗 Frontend UI**: [RecipeFinder_UI](https://github.com/stevewash123/RecipeFinder_UI) - Angular 18 TypeScript Frontend
- **📖 Complete Documentation**: See main project for setup instructions and architecture overview

## 🔧 Development

### **Project Structure**
```
RecipeParser.Api/
├── Controllers/        # API endpoints
├── Models/            # Request/response models
├── Services/          # Business logic and TheMealDB integration
├── Parser/            # Boolean expression parsing logic
├── Program.cs         # Application configuration
└── appsettings.json   # Configuration settings
```

### **Key Dependencies**
```xml
<PackageReference Include="Superpower" Version="3.0.0" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0" />
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="8.0.0" />
```

### **CORS Configuration**
```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

## 🧪 Testing

### **Test Endpoints with curl**
```bash
# Search recipes
curl -X POST http://localhost:5000/api/recipe/search \
  -H "Content-Type: application/json" \
  -d '{"query": "italian AND chicken"}'

# Validate query
curl "http://localhost:5000/api/recipe/validate?query=italian%20AND%20chicken"

# Get dropdown options
curl http://localhost:5000/api/recipe/dropdown-options
```

### **Swagger UI Testing**
Navigate to http://localhost:5000/swagger for interactive API testing.

## 📦 Deployment

### **Production Build**
```bash
dotnet publish -c Release -o publish
```

### **Docker Support** (Future Enhancement)
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0
COPY publish/ app/
WORKDIR /app
EXPOSE 80
ENTRYPOINT ["dotnet", "RecipeParser.Api.dll"]
```

## 🔍 Parser Implementation Details

### **Boolean Expression Grammar**
```
Expression := Term (('AND' | 'OR') Term)*
Term := 'NOT'? Factor
Factor := Identifier | '(' Expression ')'
Identifier := [a-zA-Z_][a-zA-Z0-9_]*
```

### **SQL Transformation Rules**
- **Cuisines/Areas**: `italian` → `a.AreaName = 'italian'`
- **Ingredients**: `chicken` → `i.IngredientName LIKE '%chicken%'`
- **Dietary**: `vegetarian` → `r.IsVegetarian = 1`
- **Attributes**: `quick` → `r.CookTimeMinutes <= 30`

---

**Part of the Lexical Parser Demonstration project showcasing modern C# API development with parsing concepts.**

*For frontend integration and complete setup, see [RecipeFinder_UI](https://github.com/stevewash123/RecipeFinder_UI)*