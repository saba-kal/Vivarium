using UnityEngine;
using System.Collections;

public class GameDataService
{
    private static GameData _gameData;

    public GameData LoadGame()
    {
        return _gameData;
    }

    public GameData StartNewGame()
    {
        _gameData = new GameData();
        return _gameData;
    }

    public void SaveGame(GameData gameData)
    {
        _gameData = gameData;
    }

    public void IncrementCurrentLevel()
    {
        _gameData.CurrentLevelIndex++;
    }
}
