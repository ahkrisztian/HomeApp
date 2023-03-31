using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeAppDataAccessLibrary.Models.AddressModels;
using HomeAppDataAccessLibrary.Models.DTOs.AddressDTO;
using HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO;
using HomeAppDataAccessLibrary.Models.DTOs.UserDTO;
using HomeAppDataAccessLibrary.Models.HomeModels;
using HomeAppMAUI.DataServices;

namespace HomeAppMAUI.ViewModels
{
    [QueryProperty(nameof(User), "ReadUserDTO")]
    public partial class CreateViewModel : BaseViewModel
    {
        private readonly IHomeDataService dataService;

        CreateHomeModelDTO home { get; set; } = new CreateHomeModelDTO() { Address = new AddressModel() };

        [ObservableProperty]
        ReadUserDTO user;

        [ObservableProperty]
        ReadAddressDTO address;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        int userid;
        public CreateViewModel(IHomeDataService dataService)
        {
            this.dataService = dataService;
        }

        async Task CreateHome()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task CreateNewHomeModel()
        {
            home.Name = Name;
            home.Description = Description;
            home.UserId = Userid;

            home.Address.City = Address.City;
            home.Address.Country = Address.Country;
            home.Address.Street = Address.Street;

            try
            {
                await dataService.CreateHomeModel(home);
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error!", "Unable to get Home Models", "Ok");
            }
        }
    }
}
