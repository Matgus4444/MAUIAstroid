using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUIMobile.Models;
using MAUIMobile.Services;
using System.Collections.ObjectModel;

namespace MAUIMobile.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly AstroidService astroidService;
        [ObservableProperty]
        ObservableCollection<Astroid> astroids = new ObservableCollection<Astroid>();

        [ObservableProperty]
        ObservableCollection<Astroid> allAstroids = new ObservableCollection<Astroid>();

        [ObservableProperty]
        string newAstroid = "";

        public MainViewModel(AstroidService astroidService)
        {
            this.astroidService = astroidService;
            SelectedDate = DateTime.Today;
        }

        [ObservableProperty]
        DateTime selectedDate;

        public event Func<string, string, string, Task> ShowAlertRequested;

        [ObservableProperty]
        private bool isBusy;

        private bool _showHazardousOnly;
        public bool ShowHazardousOnly
        {
            get => _showHazardousOnly;
            set
            {
                if (_showHazardousOnly != value)
                {
                    _showHazardousOnly = value;
                    OnPropertyChanged();
                    FilterAstroids();
                }
            }
        }
        private void FilterAstroids()
        {
            Astroids.Clear();
            foreach (var a in AllAstroids)
            {
                if (!ShowHazardousOnly || a.IsPotentiallyHazardous)
                    Astroids.Add(a);
            }
        }

        [RelayCommand]
        async Task DemoApiError()
        {
            try
            {
                // Call the API with an invalid/faulty key
                var faultyAstroids = await astroidService.GetAstroids(SelectedDate, "Invalid_Key");

               
                MainThread.BeginInvokeOnMainThread(() =>
        {
            Astroids.Clear();
            foreach (var a in faultyAstroids)
                Astroids.Add(a);
        });
            }
            catch (HttpRequestException httpEx)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Network Error",
                    $"Could not reach NASA API: {httpEx.Message}",
                    "Close");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    $"An unexpected error occurred: {ex.Message}",
                    "Close");
            }
        }

        [RelayCommand]
        public async Task LoadAsteroids()
        {
            try
            {
                IsBusy = true; //Show loader
                var list = await astroidService.GetAstroids(SelectedDate);

                if (list == null || list.Count == 0)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "API Error",
                        "No asteroids found for this date.",
                        "Close");
                    return;
                }

                
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Astroids.Clear();
                    AllAstroids.Clear();
                    foreach (var a in list)
                        AllAstroids.Add(a);

                    FilterAstroids();
                });
            }
            catch (HttpRequestException httpEx)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Network Error",
                    $"Could not reach NASA API: {httpEx.Message}",
                    "Close");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    $"An unexpected error occurred: {ex.Message}",
                    "Close");
            }
            finally
            {
                IsBusy = false; // hide loader
            }
        }
        [RelayCommand]
        async void Get()
        {

            Astroids.Clear();
            var astroidList = await astroidService.GetAstroids(SelectedDate);
            foreach (var astroid in astroidList)
            {
                Astroids.Add(astroid);
            }
        }

        [RelayCommand]
        async Task GoToDetail(Astroid astroid)
        {
            if (astroid is null) return;
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}", true, new Dictionary<string, object> {
                { "Astroid", astroid}
            });
        }

        [RelayCommand]
        void Add()
        {
            if (NewAstroid != null)
            {
                Astroids.Add(new Astroid() { Name = NewAstroid });
                NewAstroid = "";
            }
        }


    }
}
