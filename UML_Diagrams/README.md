# UML Diagrams for C# Projects

This directory contains UML Class Diagrams and Use Case Diagrams for all the projects developed during the IT training period.

## Diagrams Created

### 1. BankingSystem
- **Class Diagram**: Shows inheritance hierarchy (BankAccount → SavingsAccount, CheckingAccount, BusinessAccount), interfaces (IInterestBearing), services (AccountService, TransactionService, NotificationService), and the Transaction model.
- **Use Case Diagram**: Shows actors (Customer, BankEmployee, System) and use cases for account management, transactions, and notifications.

### 2. StudentGradeCalculator
- **Class Diagram**: Shows the Student class with all its properties and methods for grade management.
- **Use Case Diagram**: Shows actors (Student, Teacher, Administrator) and use cases for grade management and GPA calculation.

### 3. FileManagementSystem
- **Class Diagram**: Shows FileInfo model and FileManager service with file operations.
- **Use Case Diagram**: Shows actors (User, System) and use cases for file operations.

### 4. NotificationService
- **Class Diagram**: Shows interfaces (INotificationProvider, INotificationObserver, INotificationCommand, INotificationHandler), concrete implementations, and services demonstrating Observer, Command, and Chain of Responsibility patterns.
- **Use Case Diagram**: Shows actors (Application, User, System) and use cases for notification management.

### 5. TextAdventureGame
- **Class Diagram**: Shows interfaces (IGameObject, ILocation, ICommand), classes (Player, Item, Location, Commands), and GameEngine demonstrating Command pattern.
- **Use Case Diagram**: Shows actors (Player, GameSystem) and use cases for game interactions.

### 6. WeatherDataAnalyzer
- **Class Diagram**: Shows WeatherRecord class with all weather properties and analysis methods.
- **Use Case Diagram**: Shows actors (User, Analyst, System) and use cases for weather data analysis.

### 7. InventoryManagementSystem
- **Class Diagram**: Shows Product class with inventory management properties and methods.
- **Use Case Diagram**: Shows actors (Manager, Employee, System) and use cases for inventory management.

## How to View the Diagrams

These diagrams are in PlantUML format (.puml). To view them:

### VS Code (Recommended - Easiest Method)

1. **Install PlantUML Extension**:
   - Open VS Code
   - Press `Ctrl+Shift+X` to open Extensions
   - Search for "PlantUML" by jebbs
   - Click Install

2. **Install Java** (Required for PlantUML):
   - Download from: https://adoptium.net/ or https://www.oracle.com/java/technologies/downloads/
   - Install and restart VS Code

3. **View Diagrams**:
   - Open any `.puml` file
   - Press `Alt+D` to preview the diagram
   - Or right-click → "Preview PlantUML"
   - Or use Command Palette (`Ctrl+Shift+P`) → "PlantUML: Preview Current Diagram"

4. **Export to Image**:
   - Press `Ctrl+Shift+P`
   - Type "PlantUML: Export Current Diagram"
   - Choose PNG, SVG, or PDF format

### Online Viewer (No Installation Required)

1. Open the `.puml` file
2. Copy all content
3. Go to: http://www.plantuml.com/plantuml/uml/
4. Paste the content
5. View the diagram

### IntelliJ IDEA

1. Install PlantUML plugin
2. Open the .puml files
3. Right-click → "PlantUML" → "Preview Diagram"

### Command Line

Install PlantUML and use:
```bash
java -jar plantuml.jar *.puml
```

**See VS_Code_Setup_Guide.md for detailed VS Code setup instructions.**

## Design Patterns Demonstrated

- **Observer Pattern**: NotificationService (NotificationCenter, INotificationObserver)
- **Command Pattern**: NotificationService (INotificationCommand), TextAdventureGame (ICommand)
- **Chain of Responsibility**: NotificationService (INotificationHandler)
- **Factory Pattern**: (Referenced in other projects)
- **Strategy Pattern**: (Referenced in other projects)

## SOLID Principles Demonstrated

- **Single Responsibility Principle**: Each service class has one responsibility
- **Open/Closed Principle**: Classes are open for extension, closed for modification
- **Liskov Substitution Principle**: Derived classes can substitute base classes
- **Interface Segregation Principle**: Small, focused interfaces (IInterestBearing, IGameObject)
- **Dependency Inversion Principle**: High-level modules depend on abstractions (INotificationProvider, ICommand)

## Notes

- All diagrams follow standard UML notation
- Relationships are shown with appropriate arrows (inheritance, implementation, association, dependency)
- Use case diagrams show actors, use cases, and relationships (include, extend)
- Class diagrams show visibility modifiers: + (public), - (private), # (protected)

