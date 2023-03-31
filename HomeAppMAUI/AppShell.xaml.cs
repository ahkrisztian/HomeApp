using HomeAppMAUI.Pages;

namespace HomeAppMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(HomeDetails), typeof(HomeDetails));
            Routing.RegisterRoute(nameof(CreateHomePage), typeof(CreateHomePage));
        }
    }
}