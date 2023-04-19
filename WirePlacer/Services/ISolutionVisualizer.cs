using WirePlacer.Models;

namespace WirePlacer.Services;

public interface ISolutionVisualizer
{
    IDrawable GetDrawable(WireBundle wireBundle);
}
