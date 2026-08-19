using System.Collections.Generic;
using UnityEngine;

public static class RegionRegistry
{
    private static readonly Dictionary<string, RegionDefinition> definitions =
        new Dictionary<string, RegionDefinition>();

    public static void Register(RegionDefinition definition)
    {
        Debug.Log($"Registering region: {definition.FullId}");
        if (definitions.ContainsKey(definition.FullId))
        {
            Debug.LogWarning($"Region with ID '{definition.FullId}' is already registered. Overwriting.");
            definitions[definition.FullId] = definition;
            return;
        }

        definitions.Add(definition.FullId, definition);
    }

    public static RegionDefinition Get(string fullId)
    {
        return definitions[fullId];
    }

    public static bool TryGet(
        string fullId,
        out RegionDefinition definition)
    {
        return definitions.TryGetValue(fullId, out definition);
    }
}