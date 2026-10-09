using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using UnityEngine.Tilemaps;
using System.Collections.Generic;


[System.Serializable]
public class ResourcePack
{
    public string name;
    public string id;
    public string version;
    public string author;
    public string description;
}

public class RecourcePackManager : MonoBehaviour
{

    public static RecourcePackManager Instance { get; private set; }

    [System.NonSerialized]
    public Dictionary<string, ResourcePack> loadedPacks = new();

    public void LoadBaseGameTextures()
    {
        string GameDataPath = Path.Combine(Application.dataPath, "GameData");
        string baseGamePath = Path.Combine(GameDataPath, "Base");
        LoadRecourcePack(Path.Combine(baseGamePath, "BaseResourcePack"));
    }

    public void LoadAllPacks()
    {
        string GameDataPath = Path.Combine(Application.dataPath, "GameData");
        string packsDirectory = Path.Combine(GameDataPath, "ResourcePacks");
        foreach (string dir in Directory.GetDirectories(packsDirectory))
        {
            Debug.Log("Loading resource pack from directory: " + dir);
            LoadRecourcePack(dir);
        }
    }

    private bool ReadResourcePackMetadata(string packPath)
{
    string metadataFilePath = Path.Combine(packPath, "pack.json");

    if (!File.Exists(metadataFilePath))
    {
        Debug.LogWarning($"No pack.json found in {packPath}. Skipping resource pack.");
        return false;
    }

    string jsonContent = File.ReadAllText(metadataFilePath);

    ResourcePack metadata =
        JsonUtility.FromJson<ResourcePack>(jsonContent);

    if (metadata == null || string.IsNullOrEmpty(metadata.id))
    {
        Debug.LogError($"Invalid pack.json in {packPath}");
        return false;
    }

    loadedPacks[metadata.id] = metadata;

    Debug.Log(
        $"Loading Resource Pack: {metadata.name}, " +
        $"Version: {metadata.version}, " +
        $"Author: {metadata.author}"
    );

    return true;
}

    public void LoadRecourcePack(string packPath)
    {
        // register tiles with namespace:localId rather than localId itself
        ReadResourcePackMetadata(packPath);
        string texturesPath = Path.Combine(packPath, "Textures");
        foreach (string dir in Directory.GetDirectories(texturesPath))
        {
            string dirName = Path.GetFileName(dir);
            Debug.Log(dirName + " is the current directory being processed in the resource pack.");
            if (dirName == "Blocks")
            {
                foreach (string file in Directory.GetFiles(dir, "*.png"))
                {
                    string id = Path.GetFileNameWithoutExtension(file);
                    Debug.Log("Loading block texture: " + file + " " + "with ID: " + id);
                    
                    TileBase tile = TileAssetManager.Instance.LoadTileFromFile(file);
                    TileRegistry.Register(id, tile);
                }
            } else
            {
                foreach (string file in Directory.GetFiles(dir, "*.png"))
                {
                    string id = Path.GetFileNameWithoutExtension(file);
                    Debug.Log("Loading texture: " + file + " " + "with ID: " + id);
                    Texture2D texture = ConvertToTexture2D(file, id);
                    TextureRegistry.Register(id, texture);
                }
            }
        }
    }

    private Texture2D ConvertToTexture2D(string filePath, string id)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);
        return texture;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
