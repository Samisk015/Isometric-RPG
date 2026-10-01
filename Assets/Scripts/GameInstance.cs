using UnityEngine;

public static class GameEngine
{
    public static GameInstance GameInstance;
}

public enum GameState
{
    Null,
    Loaded,
    Not_Loaded,
    Loading
}

public class GameInstance
{
    private GameState gameState;

    public GameState GetState()
    {
        return gameState;
    }

    public void SetState(GameState state)
    {
        gameState = state;
    }
}