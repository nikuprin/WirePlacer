namespace WirePlacer.Models;

public readonly struct Circle
{
    public static readonly Circle Invalid = new(new Point(0, 0), -1);
    private const double MultiplicativeEpsilon = 1 + 1e-14;
    public Point Center { get; }
    public double Radius { get; }

    public Circle(Point center, double radius)
    {
        Center = center;
        Radius = radius;
    }

    public bool Contains(Point p)
    {
        return Center.Distance(p) <= Radius * MultiplicativeEpsilon;
    }

    public Circle Translate(double dx, double dy)
    {
        return new Circle(Center.Translate(dx, dy), Radius);
    }

    public IEnumerable<Point> CircumferencePoints(double angularRes = 0.01)
    {
        const double circleAngle = 2 * Math.PI;
        var theta = 0d;
        while (theta < circleAngle)
        {
            yield return new Point(Center.X + Radius * Math.Cos(theta), Center.Y + Radius * Math.Sin(theta));
            theta += angularRes;
        }
    }
}
