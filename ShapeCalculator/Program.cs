using ShapeCalculator.Models;
using ShapeCalculator.Services;
using ShapeCalculator.Factories;

namespace ShapeCalculator;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("🔷 Advanced OOP & SOLID Principles - Shape Calculator Tutorial");
        Console.WriteLine("============================================================\n");

        // Demonstrate Open/Closed Principle
        DemonstrateOpenClosedPrinciple();
        
        Console.WriteLine("\n" + new string('-', 60) + "\n");
        
        // Demonstrate Interface Segregation Principle
        DemonstrateInterfaceSegregationPrinciple();
        
        Console.WriteLine("\n" + new string('-', 60) + "\n");
        
        // Demonstrate Strategy Pattern
        DemonstrateStrategyPattern();
        
        Console.WriteLine("\n" + new string('-', 60) + "\n");
        
        // Demonstrate Factory Pattern
        DemonstrateFactoryPattern();
        
        Console.WriteLine("\n" + new string('-', 60) + "\n");
        
        // Demonstrate Decorator Pattern
        DemonstrateDecoratorPattern();
        
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    static void DemonstrateOpenClosedPrinciple()
    {
        Console.WriteLine("🔹 OPEN/CLOSED PRINCIPLE DEMONSTRATION");
        Console.WriteLine("=====================================");
        
        var calculator = new AreaCalculator();
        
        // Create different shapes
        var shapes = new List<IShape>
        {
            new Circle(5),
            new Rectangle(4, 6),
            new Triangle(3, 4, 5),
            new Square(7),
            new Ellipse(8, 6) // New shape without modifying existing code
        };

        Console.WriteLine("\nCalculating areas for different shapes:");
        foreach (var shape in shapes)
        {
            var area = calculator.CalculateArea(shape);
            var perimeter = calculator.CalculatePerimeter(shape);
            
            Console.WriteLine($"\n{shape.GetType().Name}:");
            Console.WriteLine($"  Area: {area:F2}");
            Console.WriteLine($"  Perimeter: {perimeter:F2}");
            Console.WriteLine($"  Description: {shape.GetDescription()}");
        }
    }

    static void DemonstrateInterfaceSegregationPrinciple()
    {
        Console.WriteLine("🔹 INTERFACE SEGREGATION PRINCIPLE DEMONSTRATION");
        Console.WriteLine("==============================================");
        
        var shapes = new List<IShape>
        {
            new Circle(3),
            new Rectangle(5, 4),
            new Triangle(6, 8, 10)
        };

        Console.WriteLine("\nUsing different interfaces for different capabilities:");
        
        foreach (var shape in shapes)
        {
            Console.WriteLine($"\n{shape.GetType().Name}:");
            Console.WriteLine($"  {shape.GetDescription()}");
            
            // Only shapes that can be drawn implement IDrawable
            if (shape is IDrawable drawable)
            {
                drawable.Draw();
            }
            
            // Only shapes that can be colored implement IColorable
            if (shape is IColorable colorable)
            {
                colorable.SetColor("Blue");
                Console.WriteLine($"  Color set to: {colorable.GetColor()}");
            }
            
            // Only shapes that can be resized implement IResizable
            if (shape is IResizable resizable)
            {
                resizable.Resize(1.5);
                Console.WriteLine($"  Resized by 1.5x");
            }
        }
    }

    static void DemonstrateStrategyPattern()
    {
        Console.WriteLine("🔹 STRATEGY PATTERN DEMONSTRATION");
        Console.WriteLine("=================================");
        
        var shapes = new List<IShape>
        {
            new Circle(4),
            new Rectangle(6, 8),
            new Triangle(5, 12, 13)
        };

        // Different calculation strategies
        var strategies = new List<ICalculationStrategy>
        {
            new StandardCalculationStrategy(),
            new PreciseCalculationStrategy(),
            new FastCalculationStrategy()
        };

        Console.WriteLine("\nUsing different calculation strategies:");
        
        foreach (var strategy in strategies)
        {
            Console.WriteLine($"\n{strategy.GetType().Name}:");
            
            foreach (var shape in shapes)
            {
                var area = strategy.CalculateArea(shape);
                var perimeter = strategy.CalculatePerimeter(shape);
                
                Console.WriteLine($"  {shape.GetType().Name}: Area={area:F2}, Perimeter={perimeter:F2}");
            }
        }
    }

    static void DemonstrateFactoryPattern()
    {
        Console.WriteLine("🔹 FACTORY PATTERN DEMONSTRATION");
        Console.WriteLine("=================================");
        
        var shapeFactory = new ShapeFactory();
        
        // Create shapes using factory
        var shapes = new List<IShape>
        {
            shapeFactory.CreateShape("Circle", 5),
            shapeFactory.CreateShape("Rectangle", 4, 6),
            shapeFactory.CreateShape("Triangle", 3, 4, 5),
            shapeFactory.CreateShape("Square", 7),
            shapeFactory.CreateShape("Ellipse", 8, 6)
        };

        Console.WriteLine("\nShapes created using factory pattern:");
        foreach (var shape in shapes)
        {
            if (shape != null)
            {
                Console.WriteLine($"  Created: {shape.GetDescription()}");
            }
        }
    }

    static void DemonstrateDecoratorPattern()
    {
        Console.WriteLine("🔹 DECORATOR PATTERN DEMONSTRATION");
        Console.WriteLine("==================================");
        
        // Base shape
        IShape baseShape = new Circle(5);
        Console.WriteLine($"\nBase shape: {baseShape.GetDescription()}");
        
        // Add color decorator
        var coloredShape = new ColoredShapeDecorator(baseShape, "Red");
        Console.WriteLine($"Colored shape: {coloredShape.GetDescription()}");
        
        // Add border decorator
        var borderedShape = new BorderedShapeDecorator(coloredShape, "Black", 2);
        Console.WriteLine($"Bordered shape: {borderedShape.GetDescription()}");
        
        // Add shadow decorator
        var shadowedShape = new ShadowedShapeDecorator(borderedShape, 5);
        Console.WriteLine($"Shadowed shape: {shadowedShape.GetDescription()}");
        
        // Calculate area (decorators don't affect calculations)
        var calculator = new AreaCalculator();
        var area = calculator.CalculateArea(shadowedShape);
        var perimeter = calculator.CalculatePerimeter(shadowedShape);
        
        Console.WriteLine($"\nCalculations (unaffected by decorators):");
        Console.WriteLine($"  Area: {area:F2}");
        Console.WriteLine($"  Perimeter: {perimeter:F2}");
    }
}
