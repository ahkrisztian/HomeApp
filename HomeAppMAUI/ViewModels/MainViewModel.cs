using HomeAppDataAccessLibrary.Models.HomeModels;
using HomeAppMAUI.DataServices;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using HomeAppMAUI.Pages;
using HomeAppDataAccessLibrary.Models.DTOs.UserDTO;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HomeAppMAUI.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly IHomeDataService dataService;
    private readonly IUserDataService userDataService;

    [ObservableProperty]
    ReadUserDTO user;

    public ObservableCollection<HomeModel> homeModels { get; set; } = new();
    public MainViewModel(IHomeDataService dataService, IUserDataService userDataService) 
    {
        this.dataService = dataService;
        this.userDataService = userDataService;

        User = Task.Run(async () => await userDataService.GetUserById(2)).Result;

        var homes = Task.Run(async () => await dataService.GetHomeModelsById(User.Id)).Result;

        homeModels = new ObservableCollection<HomeModel>(homes);
    }

    [RelayCommand]
    async Task GetHomeModelsAsyncById()
    {
        try
        {
            var homes =  await dataService.GetHomeModelsById(User.Id);

            if(homeModels.Count != 0)
            {
                homeModels.Clear();
            }

            foreach(var home in homes)
            {
                homeModels.Add(home);
            }
        }
        catch (Exception)
        {
            await Shell.Current.DisplayAlert("Error!", "Unable to get Home Models", "Ok");
        }
    }

    [RelayCommand]
    async Task GoToUpdateHomePage(HomeModel model)
    {
        await Shell.Current.GoToAsync(nameof(CreateHomePage),true, 
            new Dictionary<string, object>
            {
                {"HomeModel", model}
            });
    }

    [RelayCommand]
    async Task GoToHomeDetailPage(HomeModel homemodel)
    {
        if(homemodel is null)
        {
            return;
        }

        await Shell.Current.GoToAsync(nameof(HomeDetails), true,
            new Dictionary<string, object>
            {
                {nameof(HomeModel), homemodel}
            });
    }

    [RelayCommand]
    async Task GoToCreatePage(ReadUserDTO readUserDTO)
    {
        await Shell.Current.GoToAsync(nameof(CreateHomePage), true,
            new Dictionary<string, object>
            {
                {nameof(ReadUserDTO), readUserDTO}
            });
    }
}

