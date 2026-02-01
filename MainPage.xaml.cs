using MAUIMobile.ViewModels;
using MAUIMobile.Models;

namespace MAUIMobile
{
    public partial class MainPage : ContentPage
    {
   

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            vm.Title = "Near Earth astroids";

        }
        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
           
            if (BindingContext is MainViewModel vm)
            {
                vm.LoadAsteroidsCommand.Execute(null);
            }
        }

    }
}
