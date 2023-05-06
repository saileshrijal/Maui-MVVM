using MonkeyApp.View;

namespace MonkeyApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(DetailsPage),typeof(DetailsPage));
	}
}
