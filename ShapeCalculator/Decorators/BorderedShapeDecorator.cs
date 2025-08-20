namespace ShapeCalculator.Decorators;

using ShapeCalculator.Models;

// Decorator for adding borders to shapes
public class BorderedShapeDecorator : IShapeDecorator
{
    private readonly IShape _shape;
    private readonly string _borderColor;
    private readonly double _borderWidth;

    public IShape WrappedShape => _shape;
    public string BorderColor => _borderColor;
    public double BorderWidth => _borderWidth;

    public BorderedShapeDecorator(IShape shape, string borderColor, double borderWidth)
    {
        _shape = shape ?? throw new ArgumentNullException(nameof(shape));
        _borderColor = borderColor ?? "Black";
        _borderWidth = borderWidth > 0 ? borderWidth : 1;
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
        return $"{_shape.GetDescription()} with {_borderWidth:F1}px {_borderColor} border";
    }
}
