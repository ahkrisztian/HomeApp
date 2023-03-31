using HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO;
using HomeAppMAUI.DataServices;
using HomeAppMAUI.ViewModels;

namespace HomeAppMAUI.Pages;

public partial class CreateHomePage : ContentPage
{
    public CreateHomePage(CreateViewModel createViewModel)
	{
		InitializeComponent();

		BindingContext = createViewModel;
	}

}