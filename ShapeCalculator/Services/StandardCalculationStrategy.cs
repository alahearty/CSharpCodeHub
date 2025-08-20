namespace ShapeCalculator.Services;

using ShapeCalculator.Models;

// Standard calculation strategy
public class StandardCalculationStrategy : ICalculationStrategy
{
    public double CalculateArea(IShape shape)
    {
        return shape.CalculateArea();
    }

    public double CalculatePerimeter(IShape shape)
    {
        return shape.CalculatePerimeter();
    }

    public string GetStrategyName()
    {
        return "Standard";
    }
}
