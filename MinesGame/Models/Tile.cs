using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MinesGame.Models;

public class Tile
{
    public bool IsMine { get; set; }

    public bool IsRevealed { get; set; }

    public string ImageSource
    {
        get
        {
            if (!IsRevealed)
                return "hidden.png";

            return IsMine ? "bomb.png" : "gem.png";
        }
    }

    public Color TileColor
    {
        get
        {
            if (!IsRevealed)
                return Colors.Gray;

            return IsMine ? Colors.Red : Colors.Green;
        }
    }
}