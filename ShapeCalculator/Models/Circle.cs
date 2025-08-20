namespace ShapeCalculator.Models;

// Concrete shape class demonstrating OPEN/CLOSED PRINCIPLE
public class Circle : IShape, IDrawable, IColorable, IResizable
{
    private double _radius;
    private string _color = "Black";
    private double _scale = 1.0;

    public Circle(double radius)
    {
        if (radius <= 0)
            throw new ArgumentException("Radius must be positive", nameof(radius));
        
        _radius = radius;
    }

    public double CalculateArea()
    {
        return Math.PI * _radius * _radius * _scale * _scale;
    }

    public double CalculatePerimeter()
    {
        return 2 * Math.PI * _radius * _scale;
    }

    public string GetDescription()
    {
        return $"Circle with radius {_radius:F2}";
    }

    // IDrawable implementation
    public void Draw()
    {
        Console.WriteLine($"  🎨 Drawing a {_color} circle with radius {_radius:F2}");
    }

    public void Erase()
    {
        Console.WriteLine($"  🧽 Erasing the circle");
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
