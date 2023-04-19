namespace WirePlacer.Models;

public struct Circle
{
    public static readonly Circle Invalid = new Circle(new Point(0, 0), -1);
    private const double MultiplicativeEpsilon = 1 + 1e-14;
    public Point Center { get; }
    public double Radius { get; }

    public Circle(Point center, double radius)
    {
        this.Center = center;
        this.Radius = radius;
    }

    public bool Contains(Point p)
    {
        return Center.Distance(p) <= Radius * MultiplicativeEpsilon;
    }

    public bool Contains(ICollection<Point> ps)
    {
        foreach (Point p in ps)
        {
            if (!Contains(p))
            {
                return false;
            }
        }
        return true;
    }

    public Circle Translate(double dx, double dy)
    {
        return new Circle(Center.Translate(dx, dy), Radius);
    }
}
