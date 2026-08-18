using System.IO;
using UnityEngine;

public class GameDataLoader : MonoBehaviour
{
    private void Awake()
    {
        // LoadGameData();
    }

    private void LoadGameData()
    {
        string gameDataPath = Path.Combine(
            Application.dataPath,
            "GameData"
        );

        string basePath = Path.Combine(gameDataPath, "Base");
        string modsPath = Path.Combine(gameDataPath, "Mods");

        ModLoader.LoadContentPack(basePath);

        if (!Directory.Exists(modsPath))
            return;

        foreach (string modFolder in Directory.GetDirectories(modsPath))
        {
            ModLoader.LoadContentPack(modFolder);
        }
    }
}