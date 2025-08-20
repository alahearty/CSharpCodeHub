namespace ShapeCalculator.Models;

// Base interface for all shapes - demonstrates INTERFACE SEGREGATION
public interface IShape
{
    double CalculateArea();
    double CalculatePerimeter();
    string GetDescription();
}
