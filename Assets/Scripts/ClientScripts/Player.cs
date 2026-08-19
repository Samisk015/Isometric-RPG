using UnityEngine;

public class Player : MonoBehaviour
{
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
