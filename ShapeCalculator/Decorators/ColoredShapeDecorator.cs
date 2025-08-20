namespace ShapeCalculator.Decorators;

using ShapeCalculator.Models;

// Decorator for adding color to shapes
public class ColoredShapeDecorator : IShapeDecorator
{
    private readonly IShape _shape;
    private readonly string _color;

    public IShape WrappedShape => _shape;
    public string Color => _color;

    public ColoredShapeDecorator(IShape shape, string color)
    {
        _shape = shape ?? throw new ArgumentNullException(nameof(shape));
        _color = color ?? "Black";
    }

    public double CalculateArea()
    {
        return _shape.CalculateArea();
    }

    public double CalculatePerimeter()
    {
        return _shape.CalculatePerimeter();
    }

    public string GetDescription()
    {
        return $"{_color} {_shape.GetDescription()}";
    }
}
