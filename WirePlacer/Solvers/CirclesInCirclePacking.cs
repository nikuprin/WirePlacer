using WirePlacer.Models;
using Point = WirePlacer.Models.Point;

namespace WirePlacer.Solvers;

/// <summary>
///     Solves circle packing problem using one pass approach.
///     Inspiration source:
///     https://stackoverflow.com/questions/3851668/position-n-circles-of-different-radii-inside-a-larger-circle-without-overlapping
/// </summary>
public static class CirclesInCirclePacking
{
    internal static List<Circle> PackCircles(IList<double> radii)
    {
        using var basePoints = BasePoints(0.01, 0.1, 1000).GetEnumerator();
        var freePoints = new List<Point>();
        var sortedRadii = new List<double>(radii);
        sortedRadii.Sort();
        sortedRadii.Reverse();
        var circles = new List<Circle>();
        foreach (var radius in sortedRadii)
        {
            var pointToPop = new object();
            foreach (var freePoint in freePoints.Where(freePoint => circles.IsSpaceForNewCircle(freePoint, radius)))
            {
                circles.Add(new Circle(freePoint, radius));
                pointToPop = freePoint;
                break;
            }

            if (pointToPop is Point p)
            {
                freePoints.Remove(p);
                continue;
            }

            while (basePoints.MoveNext())
            {
                var basePoint = basePoints.Current;
                if (!circles.IsSpaceForNewCircle(basePoint, radius))
                {
                    freePoints.Add(basePoint);
                    continue;
                }

                circles.Add(new Circle(basePoint, radius));
                break;
            }
        }

        return circles;
    }

    private static IEnumerable<Point> BasePoints(double radialRes, double angularRes, double maxRadius)
    {
        const double circleAngle = 2 * Math.PI;
        var r = 0d;
        while (r < maxRadius)
        {
            var theta = 0d;
            while (theta < circleAngle)
            {
                yield return new Point(r * Math.Cos(theta), r * Math.Sin(theta));
                var limitedR = r > 1 ? 1 : Math.Sqrt(r);
                theta += angularRes;
            }

            r += radialRes;
        }
    }

    private static bool IsSpaceForNewCircle(this IList<Circle> circles, Point point, double radius)
    {
        return !circles.Any() || circles.All(c => point.Distance(c.Center) > c.Radius + radius);
    }
}
