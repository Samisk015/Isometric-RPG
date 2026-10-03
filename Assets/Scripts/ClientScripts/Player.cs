using UnityEngine;

public enum Gamemode
{
    Creative,
    Survival
}

public class Player : MonoBehaviour
{

    private Gamemode gamemode;

    public Gamemode GetGamemode()
    {
        return gamemode;
    }

    public void SetGamemode(Gamemode mode)
    {
        gamemode = mode;
    }
    public Camera playerCamera;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
             playerCamera.orthographicSize -= 1f;
        } else if (Input.GetKeyDown(KeyCode.Minus))
        {
            playerCamera.orthographicSize += 1f;
        }
    }
}
