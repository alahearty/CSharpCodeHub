namespace ShapeCalculator.Decorators;

using ShapeCalculator.Models;

// Base decorator interface
public interface IShapeDecorator : IShape
{
    IShape WrappedShape { get; }
}
