namespace ShapeCalculator.Services;

using ShapeCalculator.Models;

// Service demonstrating OPEN/CLOSED PRINCIPLE
// This calculator can work with any shape that implements IShape
// without modifying the calculator code
public class AreaCalculator
{
    public double CalculateArea(IShape shape)
    {
        if (shape == null)
            throw new ArgumentNullException(nameof(shape));
        
        return shape.CalculateArea();
    }

    public double CalculatePerimeter(IShape shape)
    {
        if (shape == null)
            throw new ArgumentNullException(nameof(shape));
        
        return shape.CalculatePerimeter();
    }

    public double CalculateTotalArea(IEnumerable<IShape> shapes)
    {
        if (shapes == null)
            return 0;
        
        return shapes.Sum(shape => CalculateArea(shape));
    }

    public double CalculateTotalPerimeter(IEnumerable<IShape> shapes)
    {
        if (shapes == null)
            return 0;
        
        return shapes.Sum(shape => CalculatePerimeter(shape));
    }
}
