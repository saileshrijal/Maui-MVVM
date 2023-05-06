using MonkeyApp.ViewModel;

namespace MonkeyApp.View;

public partial class MainPage : ContentPage
{
	public MainPage(MonkeyViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}