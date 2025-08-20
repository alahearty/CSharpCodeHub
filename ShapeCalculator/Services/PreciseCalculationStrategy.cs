namespace ShapeCalculator.Services;

using ShapeCalculator.Models;

// High precision calculation strategy
public class PreciseCalculationStrategy : ICalculationStrategy
{
    public double CalculateArea(IShape shape)
    {
        // Use higher precision for calculations
        var area = shape.CalculateArea();
        return Math.Round(area, 6); // Round to 6 decimal places
    }

    public double CalculatePerimeter(IShape shape)
    {
        // Use higher precision for calculations
        var perimeter = shape.CalculatePerimeter();
        return Math.Round(perimeter, 6); // Round to 6 decimal places
    }

    public string GetStrategyName()
    {
        return "High Precision";
    }
}
