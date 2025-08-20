namespace ShapeCalculator.Decorators;

using ShapeCalculator.Models;

// Decorator for adding shadows to shapes
public class ShadowedShapeDecorator : IShapeDecorator
{
    private readonly IShape _shape;
    private readonly double _shadowOffset;

    public IShape WrappedShape => _shape;
    public double ShadowOffset => _shadowOffset;

    public ShadowedShapeDecorator(IShape shape, double shadowOffset)
    {
        _shape = shape ?? throw new ArgumentNullException(nameof(shape));
        _shadowOffset = shadowOffset > 0 ? shadowOffset : 1;
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
        return $"{_shape.GetDescription()} with {_shadowOffset:F1}px shadow";
    }
}
