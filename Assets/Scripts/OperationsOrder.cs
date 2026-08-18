using UnityEngine;

public class OperationsOrder : MonoBehaviour
{
    private void Start()
    {
        RecourcePackManager.Instance.LoadBaseGameTextures();
        RecourcePackManager.Instance.LoadAllPacks();
        ModLoader.LoadBaseGame();
        ModLoader.LoadAllMods();

        Debug.Log("All mods and resource packs loaded successfully.");

        ChunkManager.Instance.Test();
    }
}
