namespace ShapeCalculator.Models;

// Concrete shape class demonstrating OPEN/CLOSED PRINCIPLE
public class Rectangle : IShape, IDrawable, IColorable, IResizable
{
    private double _width;
    private double _height;
    private string _color = "Black";
    private double _scale = 1.0;

    public Rectangle(double width, double height)
    {
        if (width <= 0 || height <= 0)
            throw new ArgumentException("Width and height must be positive");
        
        _width = width;
        _height = height;
    }

    public double CalculateArea()
    {
        return _width * _height * _scale * _scale;
    }

    public double CalculatePerimeter()
    {
        return 2 * (_width + _height) * _scale;
    }

    public string GetDescription()
    {
        return $"Rectangle with width {_width:F2} and height {_height:F2}";
    }

    // IDrawable implementation
    public void Draw()
    {
        Console.WriteLine($"  🎨 Drawing a {_color} rectangle {_width:F2} x {_height:F2}");
    }

    public void Erase()
    {
        Console.WriteLine($"  🧽 Erasing the rectangle");
    }

    // IColorable implementation
    public void SetColor(string color)
    {
        _color = color ?? "Black";
    }

    public string GetColor()
    {
        return _color;
    }

    public void ClearColor()
    {
        _color = "Black";
    }

    // IResizable implementation
    public void Resize(double factor)
    {
        if (factor > 0)
        {
            _scale = factor;
        }
    }

    public double GetScale()
    {
        return _scale;
    }
}
