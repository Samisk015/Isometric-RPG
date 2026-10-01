using UnityEngine;

public class Region : MonoBehaviour
{
    [System.NonSerialized]
    public RegionDefinition definition;

    public Region(RegionDefinition regionDefinition)
    {
        this.definition = regionDefinition;
    }
}

public class RegionDefinition
{

    public string Namespace { get; }
    public string FullId { get; }
    public string LocalId { get; }
    public int minTemp { get; }
    public int maxTemp { get; }
    public float minHumidity { get; }
    public float maxHumidity { get; }
    public string terrainShape { get; }

    public Color waterShade { get; }
    public Color ambienceShade { get; }

    public RegionDefinition(string regionNamespace,
    string regionLocalId,
    int regionMinTemp,
    int regionMaxTemp,
    float regionMinHumidity,
    float regionMaxHumidity,
    string regionTerrainShape,
    Color regionWaterShade,
    Color regionAmbienceShade)
    {
        Namespace = regionNamespace;
        LocalId = regionLocalId;
        FullId = $"{Namespace}:{LocalId}";
        minTemp = regionMinTemp;
        maxTemp = regionMaxTemp;
        minHumidity = regionMinHumidity;
        maxHumidity = regionMaxHumidity;
        terrainShape = regionTerrainShape;
        waterShade = regionWaterShade;
        ambienceShade = regionAmbienceShade;
    }
}

[System.Serializable]
public class RegionDefinitionData
{
    public string id;
    public int minTemp;
    public int maxTemp;
    public float minHumidity;
    public float maxHumidity;
    public string terrainShape;
}

[System.Serializable]
public class RegionDefinitionFile
{
    public RegionDefinitionData[] regions;
}

