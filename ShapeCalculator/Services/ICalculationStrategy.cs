namespace ShapeCalculator.Services;

using ShapeCalculator.Models;

// Strategy interface for different calculation approaches
public interface ICalculationStrategy
{
    double CalculateArea(IShape shape);
    double CalculatePerimeter(IShape shape);
    string GetStrategyName();
}
