using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WirePlacer.Models;
using WirePlacer.Services;

namespace WirePlacer.ViewModels;

public class MainViewModel : ObservableObject, IMainViewModel
{
    private readonly IInputDataReader inputDataReader;
    private readonly ISolutionVisualizer solutionVisualizer;
    private IDrawable circle;
    private bool loading;
    private string msg;
    private WireBundle wireBundle;

    public MainViewModel(IInputDataReader inputDataReader,
        ISolutionVisualizer solutionVisualizer)
    {
        this.inputDataReader = inputDataReader;
        this.solutionVisualizer = solutionVisualizer;
        FindSolution = new AsyncRelayCommand(ReadInputDataAndFindSolution);
        WireBundle = WireBundle.Invalid;
    }

    public IAsyncRelayCommand FindSolution { get; }

    public IDrawable SolutionVisualization
    {
        get => circle;
        private set => SetProperty(ref circle, value);
    }

    public bool Loading
    {
        get => loading;
        private set => SetProperty(ref loading, value);
    }

    public string Msg
    {
        get => msg;
        private set => SetProperty(ref msg, value);
    }

    public WireBundle WireBundle
    {
        get => wireBundle;
        set => SetProperty(ref wireBundle, value);
    }

    private async Task ReadInputDataAndFindSolution()
    {
        var radii = await inputDataReader.PickAndRead();
        if (radii.Count == 0)
        {
            WireBundle = WireBundle.Invalid;
            Loading = false;
            return;
        }

        Loading = true;
        WireBundle = await WireBundle.FromRadiiAsync(radii);
        SolutionVisualization = solutionVisualizer.GetDrawable(WireBundle);
        Loading = false;
    }
}
