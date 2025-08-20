namespace ShapeCalculator.Factories;

using ShapeCalculator.Models;

// Factory class demonstrating FACTORY PATTERN
public class ShapeFactory
{
    public IShape CreateShape(string shapeType, params double[] parameters)
    {
        return shapeType.ToLower() switch
        {
            "circle" when parameters.Length >= 1 => new Circle(parameters[0]),
            "rectangle" when parameters.Length >= 2 => new Rectangle(parameters[0], parameters[1]),
            "triangle" when parameters.Length >= 3 => new Triangle(parameters[0], parameters[1], parameters[2]),
            "square" when parameters.Length >= 1 => new Square(parameters[0]),
            "ellipse" when parameters.Length >= 2 => new Ellipse(parameters[0], parameters[1]),
            _ => throw new ArgumentException($"Unknown shape type: {shapeType} or invalid parameters")
        };
    }

    public IShape CreateRandomShape()
    {
        var random = new Random();
        var shapes = new[] { "circle", "rectangle", "triangle", "square", "ellipse" };
        var selectedShape = shapes[random.Next(shapes.Length)];

        return selectedShape switch
        {
            "circle" => new Circle(random.Next(1, 10)),
            "rectangle" => new Rectangle(random.Next(1, 10), random.Next(1, 10)),
            "triangle" => new Triangle(random.Next(3, 8), random.Next(3, 8), random.Next(3, 8)),
            "square" => new Square(random.Next(1, 10)),
            "ellipse" => new Ellipse(random.Next(5, 15), random.Next(3, 10)),
            _ => new Circle(5) // Default fallback
        };
    }
}
