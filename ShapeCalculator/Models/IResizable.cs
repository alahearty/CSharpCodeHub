namespace ShapeCalculator.Models;

// Interface for shapes that can be resized - demonstrates INTERFACE SEGREGATION
public interface IResizable
{
    void Resize(double factor);
    double GetScale();
}
