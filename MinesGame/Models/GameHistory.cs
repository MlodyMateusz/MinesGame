using SQLite;

namespace MinesGame.Models;

public class GameHistory
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Username { get; set; }

    public DateTime Date { get; set; }

    public bool Win { get; set; }

    public int RevealedTiles { get; set; }

    public double Multiplier { get; set; }

    public double Winnings { get; set; }
}