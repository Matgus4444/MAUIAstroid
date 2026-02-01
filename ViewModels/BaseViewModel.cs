using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MAUIMobile.ViewModels
{
    public partial class BaseViewModel : ObservableObject

    {
        [ObservableProperty]
        private string? title;


    }
}
