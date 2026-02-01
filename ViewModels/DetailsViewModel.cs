using CommunityToolkit.Mvvm.ComponentModel;
using MAUIMobile.Models;

namespace MAUIMobile.ViewModels
{
    [QueryProperty("Astroid", "Astroid")]
    public partial class DetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        Astroid astroid;
    }
}
