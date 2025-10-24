# Recipe Finder - Angular Frontend

**Lexical Parser Demonstration: Recipe Finder** - Angular 18 TypeScript Frontend

A modern Angular application demonstrating Boolean query building with professional tabbed results interface and real-time query visualization.

## 🚀 Quick Start

**Prerequisites**: Backend API must be running at http://localhost:5000

```bash
# Install dependencies
npm install

# Start development server
npm start
```

Application will be available at http://localhost:4200

## 🏗️ Architecture

### **Frontend Stack**
- **Angular 18** with TypeScript
- **Reactive Forms** for dynamic query building
- **HTTP Client** for API integration
- **Material Design** inspired styling
- **Responsive Grid Layout** for optimal UX

### **Key Components**
- **Query Builder**: 2x2 grid layout with cuisine, ingredients, dietary restrictions, and additional options
- **Results Interface**: Professional tabbed view with Recipe Results, Parse Tree Analysis, and Generated SQL
- **Real-time Updates**: Dynamic query display and state management
- **API Integration**: Full communication with C# backend

## 📋 Features

### **Interactive Query Building**
- **Include/Exclude Logic**: Toggle modes for cuisines and ingredients
- **Operator Selection**: AND/OR operators for complex queries
- **Visual Feedback**: Real-time Boolean query display
- **Preset Queries**: Complex query examples for demonstration

### **Professional Results Display**
- **Recipe Cards**: Beautiful layout with metadata and tags
- **Parse Tree Visualization**: Shows AST structure and analysis
- **SQL Generation**: Displays transformed executable SQL queries
- **Tabbed Interface**: Clean separation of result types

### **Modern Development Patterns**
- **TypeScript Models**: Comprehensive interfaces for API communication
- **Service Architecture**: Clean separation with HTTP service layer
- **State Management**: Proper view state handling
- **Responsive Design**: Works across different screen sizes

## 🔗 Related Repositories

- **🔗 Backend API**: [RecipeFinder_API](https://github.com/stevewash123/RecipeFinder_API) - C# ASP.NET Core Web API with Boolean parser
- **📖 Full Documentation**: See backend repository for complete project overview and setup instructions

## 🎯 Portfolio Value

### **Technical Demonstrations**
- **Modern Angular Development**: TypeScript, reactive forms, HTTP client integration
- **Professional UI/UX**: Tabbed interface, responsive grid, Material Design principles
- **State Management**: Complex form state with real-time updates
- **API Integration**: RESTful communication with type-safe models

### **Industry Patterns**
- **Component Architecture**: Single-page application with clean separation
- **Service Layer**: Proper abstraction for API communication
- **Type Safety**: Comprehensive TypeScript interfaces and models
- **Development Workflow**: Standard Angular CLI project structure

## 🔧 Development

### **Project Structure**
```
src/app/
├── components/          # Angular components
├── services/           # HTTP service layer
├── models/             # TypeScript interfaces
├── app.ts              # Main application component
├── app.html            # Application template
└── app.css             # Application styles
```

### **API Configuration**
The application expects the backend API to be running at:
```typescript
private readonly apiUrl = 'http://localhost:5000/api';
```

### **Available Scripts**
- `npm start` - Start development server
- `npm run build` - Build for production
- `npm test` - Run unit tests
- `ng generate component` - Generate new component

## 🧪 Testing

Run unit tests:
```bash
ng test
```

Run end-to-end tests:
```bash
ng e2e
```

## 📦 Building

Build for production:
```bash
ng build --prod
```

Built files will be in the `dist/` directory.

---

**Part of the Lexical Parser Demonstration project showcasing modern full-stack development with C# and Angular.**

*For complete setup instructions and backend details, see [RecipeFinder_API](https://github.com/stevewash123/RecipeFinder_API)*