using WirePlacer.Models;

namespace WirePlacer.Drawables;

/// <summary>
///     Draws wire bundle - organization of wires inside a bundle.
/// </summary>
public class WireBundleVisualization : IDrawable
{
    private readonly WireBundle wireBundle;

    public WireBundleVisualization(WireBundle wireBundle)
    {
        this.wireBundle = wireBundle;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        var minDim = Math.Min(dirtyRect.Height, dirtyRect.Width);
        var outerRadius = minDim * 0.45f;
        var scale = outerRadius / wireBundle.Radius;
        var centerX = dirtyRect.Width / 2;
        var centerY = dirtyRect.Height / 2;

        // outer
        canvas.StrokeColor = Colors.Red;
        canvas.StrokeSize = 3;
        canvas.DrawCircle(centerX, centerY, outerRadius + 3);

        // inner
        canvas.StrokeColor = Colors.Blue;
        canvas.StrokeSize = 3;
        foreach (var wire in wireBundle.Wires)
        {
            var x = Convert.ToSingle(wire.Center.X * scale + centerX);
            var y = Convert.ToSingle(wire.Center.Y * scale + centerY);
            var r = Convert.ToSingle(wire.Radius * scale);
            canvas.DrawCircle(x, y, r);
        }
    }
}
