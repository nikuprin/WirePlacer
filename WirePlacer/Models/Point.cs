namespace WirePlacer.Models;

public readonly struct Point
{
    public double X { get; }
    public double Y { get; }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public Point Subtract(Point p)
    {
        return new Point(X - p.X, Y - p.Y);
    }

    public double Distance(Point p)
    {
        var dx = X - p.X;
        var dy = Y - p.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    // Signed area / determinant thing
    public double Cross(Point p)
    {
        return X * p.Y - Y * p.X;
    }

    public Point Translate(double dx, double dy)
    {
        return new Point(X + dx, Y + dy);
    }
}
