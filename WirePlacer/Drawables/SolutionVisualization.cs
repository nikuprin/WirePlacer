namespace WirePlacer.Drawables;

/// <summary>
/// Visualizes solution for wire placement.
/// Basically, serves as a container for drawables.
/// </summary>
public class SolutionVisualization : IDrawable
{
	private List<IDrawable> drawables;

	public SolutionVisualization(IEnumerable<IDrawable> drawables)
	{
		this.drawables = new List<IDrawable>(drawables);
	}

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
		drawables.ForEach(d => d.Draw(canvas, dirtyRect));
    }
}
