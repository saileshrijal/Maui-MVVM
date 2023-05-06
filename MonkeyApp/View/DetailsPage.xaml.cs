using MonkeyApp.ViewModel;

namespace MonkeyApp.View;
public partial class DetailsPage : ContentPage
{
	public DetailsPage(MonkeyDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}