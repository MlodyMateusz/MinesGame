using MinesGame.Models;
using MinesGame.Services;
using System.Collections.ObjectModel;

namespace MinesGame.Views;

public partial class GamePage : ContentPage
{
    ObservableCollection<Tile> tiles = new();

    DatabaseService databaseService = new();

    int mineCount = 5;

    bool gameEnded = false;

    double balance = 100.0;

    double currentBet = 10.0;

    double currentMultiplier = 1.0;

    int revealedSafeTiles = 0;

    public GamePage()
    {
        InitializeComponent();

        balance = Preferences.Get("balance", 100.0);

        BalanceLabel.Text = $"Balance: {balance:F2}";

        MineStepper.ValueChanged += MineStepper_ValueChanged;

        GenerateBoard();
    }

    void MineStepper_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        mineCount = (int)e.NewValue;

        MineCountLabel.Text = $"{mineCount} Mines";
    }

    void GenerateBoard()
    {
        tiles.Clear();

        gameEnded = false;

        Random random = new();

        List<int> mineIndexes = new();

        while (mineIndexes.Count < mineCount)
        {
            int index = random.Next(0, 25);

            if (!mineIndexes.Contains(index))
                mineIndexes.Add(index);
        }

        for (int i = 0; i < 25; i++)
        {
            tiles.Add(new Tile
            {
                IsMine = mineIndexes.Contains(i),
                IsRevealed = false
            });
        }

        BoardView.ItemsSource = tiles;
    }

    async void OnTileClicked(object sender, EventArgs e)
    {
        if (gameEnded)
            return;

        ImageButton button = sender as ImageButton;

        Tile tile = button.BindingContext as Tile;

        if (tile.IsRevealed)
            return;

        tile.IsRevealed = true;

        BoardView.ItemsSource = null;
        BoardView.ItemsSource = tiles;

        if (tile.IsMine)
        {
            gameEnded = true;

            await databaseService.AddGame(new GameHistory
            {
                Username = Preferences.Get("username", "Player"),
                Date = DateTime.Now,
                Win = false,
                RevealedTiles = revealedSafeTiles,
                Multiplier = currentMultiplier,
                Winnings = -currentBet
            });

            await DisplayAlert("Game Over",
                "You clicked a mine!",
                "OK");
        }
        else
        {
            revealedSafeTiles++;

            int totalTiles = 25;

            double multiplier = 1.0;

            for (int i = 0; i < revealedSafeTiles; i++)
            {
                multiplier *= ((double)(totalTiles - i) / (totalTiles - mineCount - i));
            }

            currentMultiplier = multiplier * 0.99;

            MultiplierLabel.Text = $"Multiplier: {currentMultiplier:F2}x";

            if (revealedSafeTiles == totalTiles - mineCount)
            {
                gameEnded = true;

                double winnings = currentBet * currentMultiplier;
                balance += winnings;

                Preferences.Set("balance", balance);
                BalanceLabel.Text = $"Balance: {balance:F2}";

                await databaseService.AddGame(new GameHistory
                {
                    Username = Preferences.Get("username", "Player"),
                    Date = DateTime.Now,
                    Win = true,
                    RevealedTiles = revealedSafeTiles,
                    Multiplier = currentMultiplier,
                    Winnings = winnings
                });

                await DisplayAlert("You Win!",
                    $"All safe tiles revealed!\nWon {winnings:F2}",
                    "OK");
            }
        }
    }

    async void OnCashoutClicked(object sender, EventArgs e)
    {
        if (gameEnded)
            return;

        gameEnded = true;

        double winnings = currentBet * currentMultiplier;

        balance += winnings;

        Preferences.Set("balance", balance);

        BalanceLabel.Text = $"Balance: {balance:F2}";

        await databaseService.AddGame(new GameHistory
        {
            Username = Preferences.Get("username", "Player"),
            Date = DateTime.Now,
            Win = true,
            RevealedTiles = revealedSafeTiles,
            Multiplier = currentMultiplier,
            Winnings = winnings
        });

        await DisplayAlert("Cash Out",
            $"Won {winnings:F2}",
            "OK");
    }

    async void OnNewGameClicked(object sender, EventArgs e)
    {
        currentMultiplier = 1.0;

        revealedSafeTiles = 0;

        MultiplierLabel.Text = "Multiplier: 1.00x";

        if (!double.TryParse(BetEntry.Text, out currentBet))
        {
            currentBet = 10;
        }

        if (currentBet > balance)
        {
            await DisplayAlert("Error",
                "Not enough balance",
                "OK");

            return;
        }

        balance -= currentBet;

        Preferences.Set("balance", balance);

        BalanceLabel.Text = $"Balance: {balance:F2}";

        GenerateBoard();
    }

    async void OnResetBalanceClicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert(
            "Reset Balance",
            "Are you sure you want to reset your balance to 100?",
            "Yes",
            "No");
    
        if (!result)
            return;
    
        balance = 100.0;
    
        Preferences.Set("balance", balance);
    
        BalanceLabel.Text = $"Balance: {balance:F2}";
    
        await DisplayAlert(
            "Balance Reset",
            "Your balance has been reset to 100.00",
            "OK");
    }
}
