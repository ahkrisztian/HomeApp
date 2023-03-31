using HomeAppMAUI.ViewModels;

namespace HomeAppMAUI.Pages;

public partial class HomeDetails : ContentPage
{
	public HomeDetails(HomeDetailsViewModel homeDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = homeDetailsViewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
		base.OnNavigatedTo(args);
    }
}