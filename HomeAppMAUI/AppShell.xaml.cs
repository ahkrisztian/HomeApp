using HomeAppMAUI.Pages;

namespace HomeAppMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CreateHomePage), typeof(CreateHomePage));
            Routing.RegisterRoute(nameof(UpdateHomePage), typeof(UpdateHomePage));
        }
    }
}