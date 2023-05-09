using MonkeyApp.Model;
using MonkeyApp.Services.Interface;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using MonkeyApp.View;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MonkeyApp.ViewModel
{
    public partial class MonkeyViewModel : BaseViewModel
    {
        private readonly IMonkeyService _monkeyService;

        public ObservableCollection<Monkey> Monkeys { get; } = new();

        IConnectivity _connectivity;
        IGeolocation _geolocation;

        public MonkeyViewModel(IMonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
        {
            Title = "List of Monkeys";
            _monkeyService = monkeyService;
            _connectivity = connectivity;
            _geolocation = geolocation;
        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async Task GoToDetailsAsync(Monkey monkey)
        {
            if (monkey is null) return;
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true,
                new Dictionary<string, object>
                {
                    {"Monkey", monkey}
                });
        }

        [RelayCommand]
        async Task GetClosestMonkeyAsync()
        {
            if(IsBusy || Monkeys.Count == 0) return;
            try
            {
                var location = await _geolocation.GetLastKnownLocationAsync();
                if (location is null)
                {
                    location = await _geolocation.GetLocationAsync(
                        new GeolocationRequest
                        {
                            DesiredAccuracy = GeolocationAccuracy.Medium,
                            Timeout = TimeSpan.FromSeconds(30)
                        });
                }

                if(location is null) return;

                var first = Monkeys.OrderBy(m=>
                    location.CalculateDistance(m.Latitude, m.Longitude, DistanceUnits.Miles))
                    .FirstOrDefault();

                if (first == null) return;

                await Shell.Current.DisplayAlert("Closest Monkey",
                       $"{first.Name} in {first.Location}", "Ok");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error",
                       $"Unable to get closest monkey: {ex.Message}", "Ok");
            }
        }

        [RelayCommand]
        async Task GetMonkeysAsync()
        {
            if (IsBusy) return;
            try
            {
                if(_connectivity.NetworkAccess != NetworkAccess.Internet) 
                {
                    await Shell.Current.DisplayAlert("Internet Issue!",
                        "Check your internet and try again", "Ok");
                    return;
                }
                IsBusy = true;
                var monkeys = await _monkeyService.GetMonkeys();
                if (Monkeys.Count != 0)
                    Monkeys.Clear();
                foreach (var monkey in monkeys)
                    Monkeys.Add(monkey);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;

            }
        }
    }
}
