using CommunityToolkit.Mvvm.Input;
using WirePlacer.Models;

namespace WirePlacer.ViewModels;

public interface IMainViewModel
{
    IAsyncRelayCommand FindSolution { get; }
    IDrawable SolutionVisualization { get; }
    bool Loading { get; }
    string Msg { get; }
    WireBundle WireBundle { get; }
}
