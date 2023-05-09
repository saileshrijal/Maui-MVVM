using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MonkeyApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyApp.ViewModel
{
    [QueryProperty("Monkey", "Monkey")]
    public partial class MonkeyDetailsViewModel : BaseViewModel
    {
        IMap _map;
        public MonkeyDetailsViewModel(IMap map)
        {
            _map = map;
        }

        [ObservableProperty]
        Monkey monkey;

        [RelayCommand]
        async Task OpenMapAsync()
        {
            try
            {
                await Map.OpenAsync(Monkey.Latitude, Monkey.Longitude, 
                    new MapLaunchOptions
                    {
                        Name = Monkey.Name,
                        NavigationMode = NavigationMode.None
                    });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error",
                       $"Unable to open map: {ex.Message}", "Ok");
            }
        }
    }
}
