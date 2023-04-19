using WirePlacer.Solvers;

namespace WirePlacer.Models;

/// <summary>
/// Represents a bundle with wires inside.
/// </summary>
public struct WireBundle
{
    public static readonly WireBundle Invalid = new WireBundle(double.NaN, new List<Circle>());
    public double Radius { get; }
    public double Diameter => Radius * 2;
    public List<Circle> Wires { get; }

    public WireBundle(double radius, IEnumerable<Circle> wires)
    {
        Radius = radius;
        Wires = new List<Circle>(wires);
    }

    /// <summary>
    /// Finds a feasible placement of wires inside wire bundle.
    /// </summary>
    /// <param name="radii">Radii of inner wires.</param>
    /// <returns></returns>
    public static async Task<WireBundle> FromRadiiAsync(IList<double> radii)
    {
        return await Task.Run(() =>
        {
            var packedCircles = CirclesInCirclePacking.PackCircles(radii);
            var points = packedCircles.SelectMany(c => new List<Point>
        {
            new Point(c.Center.X - c.Radius, c.Center.Y),
            new Point(c.Center.X + c.Radius, c.Center.Y),
            new Point(c.Center.X, c.Center.Y - c.Radius),
            new Point(c.Center.X, c.Center.Y + c.Radius),
        }).ToList();
            var enclosingCircle = SmallestEnclosingCircle.MakeCircle(points);
            var shifted = packedCircles.Select(c => c.Translate(-enclosingCircle.Center.X, -enclosingCircle.Center.Y));
            return new WireBundle(enclosingCircle.Radius, shifted);
        });
    }
}
