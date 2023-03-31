using HomeAppDataAccessLibrary.Models.HomeModels;
using HomeAppMAUI.DataServices;
using HomeAppMAUI.Pages;
using HomeAppMAUI.ViewModels;
using System.Diagnostics;

namespace HomeAppMAUI;

public partial class MainPage : ContentPage
{    
    public MainPage(MainViewModel mainViewModel)
    {
        InitializeComponent();
        BindingContext = mainViewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

    }

}