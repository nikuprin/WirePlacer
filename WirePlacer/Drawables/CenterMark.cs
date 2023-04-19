namespace WirePlacer.Drawables;

/// <summary>
/// Draws center mark in the middle of the canvas.
/// </summary>
public class CenterMark : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        var minDim = Math.Min(dirtyRect.Height, dirtyRect.Width);
        var r = minDim * 0.05f;
        var l = minDim * 0.04f;
        var centerX = dirtyRect.Width / 2;
        var centerY = dirtyRect.Height / 2;
        canvas.StrokeColor = Colors.Green;
        canvas.StrokeSize = 1;
        canvas.DrawCircle(centerX, centerY, r);
        canvas.DrawLine(centerX - l, centerY, centerX + l, centerY);
        canvas.DrawLine(centerX, centerY - l, centerX, centerY + l);
    }
}
