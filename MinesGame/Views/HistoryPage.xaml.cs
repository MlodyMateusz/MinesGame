using MinesGame.Services;

namespace MinesGame.Views;

public partial class HistoryPage : ContentPage
{
    DatabaseService databaseService = new();

    public HistoryPage()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        HistoryView.ItemsSource = await databaseService.GetGames();
    }
}