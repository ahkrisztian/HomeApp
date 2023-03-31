using CommunityToolkit.Mvvm.ComponentModel;

namespace HomeAppMAUI.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    public BaseViewModel()
    {
    }

    [ObservableProperty]
    string title;
}
