using MonkeyApp.Model;
using MonkeyApp.Services.Interface;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using MonkeyApp.View;

namespace MonkeyApp.ViewModel
{
    public partial class MonkeyViewModel : BaseViewModel
    {
        private readonly IMonkeyService _monkeyService;

        public ObservableCollection<Monkey> Monkeys { get; } = new();

        public MonkeyViewModel(IMonkeyService monkeyService)
        {
            Title = "List of Monkeys";
            _monkeyService = monkeyService;
        }

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
        async Task GetMonkeysAsync()
        {
            if (IsBusy) return;
            try
            {
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
            }
        }
    }
}
