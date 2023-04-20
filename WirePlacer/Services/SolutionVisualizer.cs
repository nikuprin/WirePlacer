using WirePlacer.Drawables;
using WirePlacer.Models;

namespace WirePlacer.Services;

/// <summary>
///     Service for visualizing wire bundle solution.
/// </summary>
public class SolutionVisualizer : ISolutionVisualizer
{
    public IDrawable GetDrawable(WireBundle wireBundle)
    {
        IDrawable cm = new CenterMark();
        IDrawable wbv = new WireBundleVisualization(wireBundle);
        return new SolutionVisualization(new[] { cm, wbv });
    }
}
