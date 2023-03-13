using HomeAppDataAccessLibrary.Models.HomeModels;
using HomeAppMAUI.DataServices;
using HomeAppMAUI.Model;
using HomeAppMAUI.Pages;
using System.Diagnostics;

namespace HomeAppMAUI;

public partial class MainPage : ContentPage
{
    private readonly IHomeDataService dataService;

    public MainPage(IHomeDataService dataService)
    {
        InitializeComponent();
        this.dataService = dataService;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        collectionView.ItemsSource = await dataService.GetHomeModelsById(2);
    }

    async void CreateHome(object sender, EventArgs e)
    {
        Debug.WriteLine("Create button clicked");

        var navigationParameter = new Dictionary<string, object>
        {
            {
                nameof(NewHomeModel), new NewHomeModel()
            }
        };

        await Shell.Current.GoToAsync(nameof(CreateHomePage), navigationParameter);
    }

    async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Debug.WriteLine("Home clicked");

        var navigationParameter = new Dictionary<string, object>
        {
            {
                nameof(NewHomeModel), e.CurrentSelection.FirstOrDefault() as NewHomeModel
            }
        };

        await Shell.Current.GoToAsync(nameof(UpdateHomePage), navigationParameter);
    }
}