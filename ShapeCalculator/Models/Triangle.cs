namespace ShapeCalculator.Models;

// Concrete shape class demonstrating OPEN/CLOSED PRINCIPLE
public class Triangle : IShape, IDrawable
{
    private double _sideA;
    private double _sideB;
    private double _sideC;

    public Triangle(double sideA, double sideB, double sideC)
    {
        if (sideA <= 0 || sideB <= 0 || sideC <= 0)
            throw new ArgumentException("All sides must be positive");
        
        if (sideA + sideB <= sideC || sideA + sideC <= sideB || sideB + sideC <= sideA)
            throw new ArgumentException("Invalid triangle: sum of any two sides must be greater than the third side");
        
        _sideA = sideA;
        _sideB = sideB;
        _sideC = sideC;
    }

    public double CalculateArea()
    {
        // Heron's formula
        double s = (_sideA + _sideB + _sideC) / 2;
        return Math.Sqrt(s * (s - _sideA) * (s - _sideB) * (s - _sideC));
    }

    public double CalculatePerimeter()
    {
        return _sideA + _sideB + _sideC;
    }

    public string GetDescription()
    {
        return $"Triangle with sides {_sideA:F2}, {_sideB:F2}, {_sideC:F2}";
    }

    // IDrawable implementation
    public void Draw()
    {
        Console.WriteLine($"  🎨 Drawing a triangle with sides {_sideA:F2}, {_sideB:F2}, {_sideC:F2}");
    }

    public void Erase()
    {
        Console.WriteLine($"  🧽 Erasing the triangle");
    }
}
