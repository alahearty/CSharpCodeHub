namespace ShapeCalculator.Models;

// Interface for shapes that can be drawn - demonstrates INTERFACE SEGREGATION
public interface IDrawable
{
    void Draw();
    void Erase();
}
