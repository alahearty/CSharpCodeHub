namespace ShapeCalculator.Services;

using ShapeCalculator.Models;

// Fast calculation strategy (less precise but faster)
public class FastCalculationStrategy : ICalculationStrategy
{
    public double CalculateArea(IShape shape)
    {
        // Use lower precision for faster calculations
        var area = shape.CalculateArea();
        return Math.Round(area, 2); // Round to 2 decimal places
    }

    public double CalculatePerimeter(IShape shape)
    {
        // Use lower precision for faster calculations
        var perimeter = shape.CalculatePerimeter();
        return Math.Round(perimeter, 2); // Round to 2 decimal places
    }

    public string GetStrategyName()
    {
        return "Fast (Low Precision)";
    }
}
