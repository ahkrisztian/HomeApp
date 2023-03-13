using HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO;
using HomeAppMAUI.DataServices;
using HomeAppMAUI.Model;

namespace HomeAppMAUI.Pages;

[QueryProperty(nameof(NewHomeModel), "NewHomeModel")]
public partial class CreateHomePage : ContentPage
{
	private readonly IHomeDataService dataService;

    NewHomeModel _newHome;

    public NewHomeModel NewHomeModel
    {
        get => _newHome;
        set
        {
            _newHome = value; OnPropertyChanged();
        }
    }


    public CreateHomePage(IHomeDataService dataService)
	{
		InitializeComponent();

		this.dataService = dataService;

		BindingContext = this;
	}

	async void OnSaveButtonClicked(object sender, EventArgs e)
	{
        await dataService.CreateHomeModel(NewHomeModel);

        await Shell.Current.GoToAsync("..");
    }
	async void OnCancelButtonClicked(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync("..");
    }
}