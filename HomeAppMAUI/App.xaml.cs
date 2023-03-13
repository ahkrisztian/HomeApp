using HomeAppMAUI.Pages;

namespace HomeAppMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Routing.RegisterRoute(nameof(CreateHomePage), typeof(CreateHomePage));
        }
    }
}