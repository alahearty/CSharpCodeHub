namespace ShapeCalculator.Models;

// Concrete shape class demonstrating OPEN/CLOSED PRINCIPLE
public class Ellipse : IShape, IDrawable, IColorable
{
    private double _semiMajorAxis;
    private double _semiMinorAxis;
    private string _color = "Black";

    public Ellipse(double semiMajorAxis, double semiMinorAxis)
    {
        if (semiMajorAxis <= 0 || semiMinorAxis <= 0)
            throw new ArgumentException("Semi-major and semi-minor axes must be positive");
        
        if (semiMajorAxis < semiMinorAxis)
            throw new ArgumentException("Semi-major axis must be greater than or equal to semi-minor axis");
        
        _semiMajorAxis = semiMajorAxis;
        _semiMinorAxis = semiMinorAxis;
    }

    public double CalculateArea()
    {
        return Math.PI * _semiMajorAxis * _semiMinorAxis;
    }

    public double CalculatePerimeter()
    {
        // Approximation using Ramanujan's formula
        double h = Math.Pow(_semiMajorAxis - _semiMinorAxis, 2) / Math.Pow(_semiMajorAxis + _semiMinorAxis, 2);
        return Math.PI * (_semiMajorAxis + _semiMinorAxis) * (1 + (3 * h) / (10 + Math.Sqrt(4 - 3 * h)));
    }

    public string GetDescription()
    {
        return $"Ellipse with semi-major axis {_semiMajorAxis:F2} and semi-minor axis {_semiMinorAxis:F2}";
    }

    // IDrawable implementation
    public void Draw()
    {
        Console.WriteLine($"  🎨 Drawing a {_color} ellipse {_semiMajorAxis:F2} x {_semiMinorAxis:F2}");
    }

    public void Erase()
    {
        Console.WriteLine($"  🧽 Erasing the ellipse");
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
}
