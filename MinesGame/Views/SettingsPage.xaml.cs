namespace MinesGame.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();

        UsernameEntry.Text = Preferences.Get("username", "");
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        Preferences.Set("username", UsernameEntry.Text);

        await DisplayAlert("Saved", "Settings saved", "OK");
    }
}