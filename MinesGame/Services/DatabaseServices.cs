using SQLite;
using MinesGame.Models;

namespace MinesGame.Services;

public class DatabaseService
{
    SQLiteAsyncConnection database;

    public async Task Init()
    {
        if (database != null)
            return;

        string path = Path.Combine(FileSystem.AppDataDirectory, "game.db");

        database = new SQLiteAsyncConnection(path);

        await database.DropTableAsync<GameHistory>();

        await database.CreateTableAsync<GameHistory>();
    }

    public async Task AddGame(GameHistory game)
    {
        await Init();

        await database.InsertAsync(game);
    }

    public async Task<List<GameHistory>> GetGames()
    {
        await Init();

        return await database.Table<GameHistory>().ToListAsync();
    }
}