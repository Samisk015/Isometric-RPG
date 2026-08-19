using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileAssetManager : MonoBehaviour
{
    public static TileAssetManager Instance { get; private set; }

    private readonly Dictionary<string, TileBase> blockTiles = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RegisterBlockTile(string fullId, TileBase tile)
    {
        if (string.IsNullOrWhiteSpace(fullId))
            throw new ArgumentException("Block ID cannot be empty.", nameof(fullId));

        if (tile == null)
            throw new ArgumentNullException(nameof(tile));

        blockTiles[fullId] = tile;
    }

    public bool TryGetBlockTile(string fullId, out TileBase tile)
    {
        return blockTiles.TryGetValue(fullId, out tile);
    }

    public TileBase GetBlockTile(string fullId)
    {
        if (!blockTiles.TryGetValue(fullId, out TileBase tile))
        {
            Debug.LogError($"No tile registered for block '{fullId}'.");
            return null;
        }

        return tile;
    }

    public TileBase LoadTileFromFile(string imagePath, float pixelsPerUnit = 32f)
    {
        if (!File.Exists(imagePath))
        {
            Debug.LogError($"Texture file not found: {imagePath}");
            return null;
        }

        byte[] imageBytes = File.ReadAllBytes(imagePath);

        Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false)
        {
            filterMode = FilterMode.Point,
            wrapMode = TextureWrapMode.Clamp,
            name = Path.GetFileNameWithoutExtension(imagePath)
        };

        if (!ImageConversion.LoadImage(texture, imageBytes, markNonReadable: false))
        {
            Debug.LogError($"Failed to load texture: {imagePath}");
            Destroy(texture);
            return null;
        }

        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f),
            pixelsPerUnit
        );

        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.name = texture.name;
        tile.sprite = sprite;

        return tile;
    }
}