using CommunityToolkit.Mvvm.ComponentModel;

namespace MonkeyApp.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        public BaseViewModel()
        {
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBool))]
        bool isBusy;

        [ObservableProperty]
        string title;

        public bool IsNotBool => !IsBusy;

    }
}
 