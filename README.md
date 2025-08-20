# CSharpCodeHub - Advanced OOP & SOLID Principles Tutorial

This repository contains comprehensive tutorials demonstrating advanced Object-Oriented Programming concepts and SOLID principles through practical console applications.

## 🏗️ Project Structure

```
CSharpCodeHub/
├── BankingSystem/          # Banking System Tutorial
├── ShapeCalculator/        # Shape Calculator Tutorial  
├── NotificationService/     # Notification Service Tutorial
├── TextAdventureGame/      # Text Adventure Game Tutorial
├── ConsoleRPG/             # Console RPG Game Tutorial
├── FileManagementSystem/   # File Management System Tutorial
├── StudentGradeCalculator/ # Student Grade Calculator Tutorial
├── InventoryManagementSystem/ # Inventory Management System Tutorial
├── WeatherDataAnalyzer/    # Weather Data Analyzer Tutorial
└── README.md               # This file
```

## 🎯 Tutorial Applications

### 1. 🏦 Banking System
**Demonstrates:** Inheritance, Polymorphism, Encapsulation, Abstraction, Single Responsibility Principle

**Key Features:**
- Abstract `BankAccount` base class with derived classes (`SavingsAccount`, `CheckingAccount`, `BusinessAccount`)
- Interface segregation with `IInterestBearing`
- Service classes with single responsibilities (`AccountService`, `TransactionService`, `NotificationService`)
- Polymorphic behavior across different account types

**OOP Concepts:**
- **Inheritance:** Account types inherit from base `BankAccount`
- **Polymorphism:** Different account types handle deposits/withdrawals differently
- **Encapsulation:** Private fields with public properties
- **Abstraction:** Abstract base class defining contract

**SOLID Principles:**
- **SRP:** Each service has one responsibility
- **OCP:** Open for extension (new account types) without modifying existing code
- **LSP:** Derived classes can substitute base class
- **ISP:** Small, focused interfaces
- **DIP:** High-level modules depend on abstractions

### 2. 🔷 Shape Calculator
**Demonstrates:** Open/Closed Principle, Interface Segregation Principle, Strategy Pattern, Factory Pattern, Decorator Pattern

**Key Features:**
- `IShape` interface with multiple implementations (`Circle`, `Rectangle`, `Triangle`, `Square`, `Ellipse`)
- Segregated interfaces (`IDrawable`, `IColorable`, `IResizable`)
- Calculation strategies (`StandardCalculationStrategy`, `PreciseCalculationStrategy`, `FastCalculationStrategy`)
- Shape factory for object creation
- Decorator pattern for adding visual effects

**OOP Concepts:**
- **Inheritance:** `Square` inherits from `Rectangle`
- **Polymorphism:** All shapes implement `IShape` interface
- **Composition:** Decorators wrap shapes to add functionality

**SOLID Principles:**
- **OCP:** New shapes can be added without modifying existing code
- **ISP:** Interfaces are small and focused
- **DIP:** Calculator depends on `IShape` abstraction

**Design Patterns:**
- **Strategy:** Different calculation approaches
- **Factory:** Shape creation
- **Decorator:** Adding visual effects to shapes

### 3. 📱 Notification Service
**Demonstrates:** Dependency Inversion Principle, Liskov Substitution Principle, Observer Pattern, Command Pattern, Chain of Responsibility Pattern

**Key Features:**
- `INotificationProvider` interface with multiple implementations
- Observer pattern for notification handling
- Command pattern for queuing and executing notifications
- Chain of responsibility for message processing

**OOP Concepts:**
- **Inheritance:** Different notification providers implement same interface
- **Polymorphism:** Service works with any provider implementation

**SOLID Principles:**
- **DIP:** High-level service depends on abstractions
- **LSP:** New providers can substitute existing ones
- **OCP:** New notification types can be added easily

**Design Patterns:**
- **Observer:** Notification subscribers
- **Command:** Notification commands with undo capability
- **Chain of Responsibility:** Message processing pipeline

### 4. 🗺️ Text Adventure Game
**Demonstrates:** Command Pattern, Factory Pattern, State Management, Game Engine Architecture

**Key Features:**
- Command-based game system (`MoveCommand`, `TakeCommand`, `UseCommand`)
- Location-based world with connected areas
- Item system with inheritance (`HealthPotion`, `Key`, `Treasure`)
- Player inventory and health management

**OOP Concepts:**
- **Inheritance:** Items inherit from base `Item` class
- **Polymorphism:** Different items have different behaviors
- **Encapsulation:** Player state management

**Design Patterns:**
- **Command:** Game actions as commands
- **Factory:** World and item creation
- **State:** Game state management

### 5. ⚔️ Console RPG Game
**Demonstrates:** Combat System, Equipment System, Skill System, Factory Pattern, Strategy Pattern

**Key Features:**
- Turn-based combat system
- Equipment system with stat bonuses
- Skill system with different abilities
- Experience and leveling system
- Enemy AI and random encounters

**OOP Concepts:**
- **Inheritance:** Characters inherit from base interfaces
- **Polymorphism:** Different skills and equipment types
- **Encapsulation:** Player stats and inventory

**Design Patterns:**
- **Strategy:** Combat strategies
- **Factory:** Enemy and item creation
- **Observer:** Combat event handling

### 6. 📁 File Management System
**Demonstrates:** File I/O Operations, Collections, Error Handling, Data Processing

**Key Features:**
- File and directory operations (create, delete, list, search)
- File information analysis and statistics
- File organization by type and date
- Drive space monitoring and analysis
- Recursive file searching and filtering

**Programming Concepts:**
- **File I/O:** Reading, writing, and managing files
- **Collections:** Lists, dictionaries, and LINQ operations
- **Error Handling:** Try-catch blocks and exception management
- **Data Processing:** File metadata extraction and analysis

**Technical Skills:**
- System.IO operations
- File attributes and properties
- Directory traversal
- File size formatting
- Search algorithms

### 7. 🎓 Student Grade Calculator
**Demonstrates:** Data Structures, Algorithms, Statistical Analysis, Reporting

**Key Features:**
- Student information management
- Grade calculation and GPA computation
- Statistical analysis (averages, trends, rankings)
- Performance tracking and reporting
- Letter grade conversion and analysis

**Programming Concepts:**
- **Data Structures:** Lists, dictionaries, and custom objects
- **Algorithms:** Sorting, searching, and statistical calculations
- **Data Validation:** Input validation and error handling
- **Reporting:** Data aggregation and presentation

**Technical Skills:**
- Mathematical calculations
- Statistical analysis
- Data aggregation
- Report generation
- Performance metrics

### 8. 📦 Inventory Management System
**Demonstrates:** CRUD Operations, Data Persistence, Business Logic, Reporting

**Key Features:**
- Product management (add, update, delete, view)
- Stock level monitoring and alerts
- Supplier management
- Transaction processing
- Inventory reports and analytics

**Programming Concepts:**
- **CRUD Operations:** Create, Read, Update, Delete
- **Data Persistence:** In-memory data storage
- **Business Logic:** Stock calculations and validations
- **Reporting:** Data analysis and presentation

**Technical Skills:**
- Data management
- Business rule implementation
- Alert systems
- Financial calculations
- Inventory tracking

### 9. 🌤️ Weather Data Analyzer
**Demonstrates:** Data Processing, Statistical Analysis, Pattern Recognition, Visualization

**Key Features:**
- Weather data collection and storage
- Temperature and precipitation analysis
- Seasonal pattern recognition
- Extreme weather detection
- Climate trend analysis and forecasting

**Programming Concepts:**
- **Data Processing:** Large dataset handling
- **Statistical Analysis:** Mean, median, trends, patterns
- **Pattern Recognition:** Seasonal and cyclical patterns
- **Data Visualization:** Console-based charts and graphs

**Technical Skills:**
- Statistical calculations
- Time series analysis
- Pattern matching
- Data aggregation
- Trend analysis

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- Visual Studio 2022, VS Code, or any C# IDE

### Running the Applications

1. **Banking System:**
   ```bash
   cd BankingSystem
   dotnet run
   ```

2. **Shape Calculator:**
   ```bash
   cd ShapeCalculator
   dotnet run
   ```

3. **Notification Service:**
   ```bash
   cd NotificationService
   dotnet run
   ```

4. **Text Adventure Game:**
   ```bash
   cd TextAdventureGame
   dotnet run
   ```

5. **Console RPG Game:**
   ```bash
   cd ConsoleRPG
   dotnet run
   ```

6. **File Management System:**
   ```bash
   cd FileManagementSystem
   dotnet run
   ```

7. **Student Grade Calculator:**
   ```bash
   cd StudentGradeCalculator
   dotnet run
   ```

8. **Inventory Management System:**
   ```bash
   cd InventoryManagementSystem
   dotnet run
   ```

9. **Weather Data Analyzer:**
   ```bash
   cd WeatherDataAnalyzer
   dotnet run
   ```

## 📚 Learning Objectives

### OOP Concepts
- **Classes & Objects:** Creating and using classes
- **Inheritance:** Building class hierarchies
- **Polymorphism:** Using interfaces and virtual methods
- **Encapsulation:** Data hiding and access control
- **Abstraction:** Abstract classes and interfaces

### SOLID Principles
- **S**ingle Responsibility Principle (SRP)
- **O**pen/Closed Principle (OCP)
- **L**iskov Substitution Principle (LSP)
- **I**nterface Segregation Principle (ISP)
- **D**ependency Inversion Principle (DIP)

### Design Patterns
- **Strategy Pattern:** Algorithm selection
- **Factory Pattern:** Object creation
- **Observer Pattern:** Event handling
- **Command Pattern:** Action encapsulation
- **Decorator Pattern:** Dynamic behavior addition
- **Chain of Responsibility:** Request processing

### Standard Programming Skills
- **File I/O Operations:** Reading, writing, and managing files
- **Data Structures:** Lists, dictionaries, arrays, and collections
- **Algorithms:** Sorting, searching, and statistical calculations
- **Error Handling:** Exception management and validation
- **Data Processing:** Large dataset handling and analysis
- **Business Logic:** Real-world application development
- **Reporting:** Data aggregation and presentation

## 🎮 Interactive Features

### Text Adventure Game Commands
- `move [location]` - Move to connected location
- `take [item]` - Pick up items
- `use [item]` - Use items from inventory
- `inventory` - Show player inventory
- `look` - Examine current location
- `help` - Show available commands

### Console RPG Game Features
- **Combat System:** Turn-based battles with skills
- **Equipment Management:** Equip weapons and armor
- **Exploration:** Random encounters and item discovery
- **Character Progression:** Experience, leveling, and stat increases

### File Management System Features
- **File Operations:** Create, delete, list, and search files
- **Directory Analysis:** Space usage and file statistics
- **File Organization:** Sort by type, date, and size
- **Search Capabilities:** Recursive and pattern-based searching

### Student Grade Calculator Features
- **Grade Management:** Add, update, and calculate grades
- **Performance Analysis:** GPA calculation and statistical analysis
- **Reporting:** Individual and class performance reports
- **Trend Analysis:** Performance tracking over time

### Inventory Management Features
- **Product Management:** Full CRUD operations
- **Stock Monitoring:** Low stock alerts and tracking
- **Supplier Management:** Vendor information and relationships
- **Transaction Processing:** Sales and purchase tracking

### Weather Data Analyzer Features
- **Data Collection:** Weather record management
- **Pattern Analysis:** Seasonal and cyclical patterns
- **Statistical Analysis:** Temperature, humidity, and precipitation trends
- **Forecasting:** Weather prediction based on historical data

## 🔧 Code Quality Features

- **Error Handling:** Comprehensive exception handling
- **Input Validation:** User input validation and sanitization
- **Logging:** Console-based logging and feedback
- **Modularity:** Well-separated concerns and responsibilities
- **Extensibility:** Easy to add new features and types
- **Data Integrity:** Validation and business rule enforcement
- **Performance:** Efficient algorithms and data structures

## 📖 Code Examples

### Interface Segregation Example
```csharp
// Good: Small, focused interfaces
public interface IDrawable { void Draw(); void Erase(); }
public interface IColorable { void SetColor(string color); string GetColor(); }

// Bad: Large, unfocused interface
public interface IShape { void Draw(); void Erase(); void SetColor(string color); string GetColor(); void Resize(double factor); }
```

### Dependency Inversion Example
```csharp
// Good: Depends on abstraction
public class NotificationService
{
    private readonly List<INotificationProvider> _providers;
    
    public void AddProvider(INotificationProvider provider) { ... }
}

// Bad: Depends on concrete implementation
public class NotificationService
{
    private readonly EmailProvider _emailProvider;
    private readonly SmsProvider _smsProvider;
}
```

### Data Processing Example
```csharp
// Efficient data processing with LINQ
public double CalculateAverageTemperature()
{
    return WeatherRecords
        .Where(r => r.Date >= StartDate && r.Date <= EndDate)
        .Select(r => r.Temperature)
        .DefaultIfEmpty(0)
        .Average();
}
```

## 🤝 Contributing

Feel free to contribute by:
- Adding new examples
- Improving existing code
- Adding more design patterns
- Enhancing documentation
- Reporting bugs or issues

## 📄 License

This project is open source and available under the MIT License.

## 🎓 Educational Use

These tutorials are designed for educational purposes to help developers understand:
- Advanced C# programming concepts
- SOLID principles in practice
- Design pattern implementation
- Clean code architecture
- Object-oriented design best practices
- Real-world application development
- Data processing and analysis
- Business logic implementation

---

**Happy Coding! 🚀**
