using MAUIMobile.ViewModels;

namespace MAUIMobile;

public partial class DetailPage : ContentPage
{
	public DetailPage(DetailsViewModel detailVM)
	{
		InitializeComponent();
		BindingContext = detailVM;

	}
}