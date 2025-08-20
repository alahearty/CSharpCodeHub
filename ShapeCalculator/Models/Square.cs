namespace ShapeCalculator.Models;

// Square class demonstrating inheritance from Rectangle
public class Square : Rectangle
{
    public Square(double side) : base(side, side)
    {
    }

    public override string GetDescription()
    {
        return $"Square with side {_width:F2}";
    }
}
