namespace ShapeCalculator.Models;

// Interface for shapes that can be colored - demonstrates INTERFACE SEGREGATION
public interface IColorable
{
    void SetColor(string color);
    string GetColor();
    void ClearColor();
}
