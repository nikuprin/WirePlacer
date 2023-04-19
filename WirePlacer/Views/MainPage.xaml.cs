using WirePlacer.ViewModels;

namespace WirePlacer.Views;

public partial class MainPage : ContentPage
{
	public MainPage(IMainViewModel mainViewModel)
	{
		BindingContext = mainViewModel;
		InitializeComponent();
	}
}
