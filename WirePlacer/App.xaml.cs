using WirePlacer.Views;

namespace WirePlacer;

public partial class App : Application
{
	public App(MainPage mainPage)
	{
		InitializeComponent();

		MainPage = mainPage;
	}
}
