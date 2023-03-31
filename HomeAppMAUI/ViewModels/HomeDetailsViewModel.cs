using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeAppDataAccessLibrary.Models.HomeModels;
using HomeAppDataAccessLibrary.Models.RoomModels;

namespace HomeAppMAUI.ViewModels;

[QueryProperty(nameof(Home), "HomeModel")]
public partial class HomeDetailsViewModel : BaseViewModel
{       

    public HomeDetailsViewModel()
    { 
        
    }

    [ObservableProperty]
    HomeModel home;

    [RelayCommand]
    async Task GoToDetailPageCommand()
    {
        await Shell.Current.GoToAsync("..");
    }
}
