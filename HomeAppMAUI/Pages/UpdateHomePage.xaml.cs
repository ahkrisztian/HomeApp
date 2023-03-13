using HomeAppMAUI.DataServices;
using HomeAppMAUI.Model;

namespace HomeAppMAUI.Pages;

[QueryProperty(nameof(NewHomeModel), "NewHomeModel")]
public partial class UpdateHomePage : ContentPage
{
    private readonly IHomeDataService dataService;

    NewHomeModel _home;
    public NewHomeModel Home
    {
        get => _home;
        set
        {
            _home = value;
            OnPropertyChanged();
        }
    }
   
    public UpdateHomePage(IHomeDataService dataService)
	{
		InitializeComponent();
        this.dataService = dataService;

        BindingContext = this;
    }
}