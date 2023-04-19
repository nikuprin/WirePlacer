using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WirePlacer.Drawables;
using WirePlacer.Models;
using WirePlacer.Services;

namespace WirePlacer.ViewModels;

public class MainViewModel : ObservableObject, IMainViewModel
{
	private readonly IInputDataReader inputDataReader;
	private readonly ISolutionVisualizer solutionVisualizer;
	public IAsyncRelayCommand FindSolution { get; }
	private IDrawable circle;
	public IDrawable SolutionVisualization { get => circle; private set => SetProperty(ref circle, value); }
	private bool loading;
	public bool Loading { get => loading; private set => SetProperty(ref loading, value); }
	private string msg;
	public string Msg { get => msg; private set => SetProperty(ref msg, value); }
	private WireBundle wireBundle;
	public WireBundle WireBundle { get => wireBundle; set => SetProperty(ref wireBundle, value); }

	public MainViewModel(IInputDataReader inputDataReader,
		ISolutionVisualizer solutionVisualizer)
	{
		this.inputDataReader = inputDataReader;
		this.solutionVisualizer = solutionVisualizer;
        FindSolution = new AsyncRelayCommand(ReadInputDataAndFindSolution);
		WireBundle = WireBundle.Invalid;
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
        WireBundle = await Models.WireBundle.FromRadiiAsync(radii);
		SolutionVisualization = solutionVisualizer.GetDrawable(WireBundle);
        Loading = false;
    }
}
