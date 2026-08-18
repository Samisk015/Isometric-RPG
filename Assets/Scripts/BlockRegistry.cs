using System.Collections.Generic;
using UnityEngine;

public static class BlockRegistry
{
    private static readonly Dictionary<string, BlockDefinition> definitions =
        new Dictionary<string, BlockDefinition>();

    public static void Register(BlockDefinition definition)
    {
        Debug.Log($"Registering block: {definition.FullId}");
        if (definitions.ContainsKey(definition.FullId))
        {
            Debug.LogWarning($"Block with ID '{definition.FullId}' is already registered. Overwriting.");
            definitions[definition.FullId] = definition;
            return;
        }

        definitions.Add(definition.FullId, definition);
    }

    public static BlockDefinition Get(string fullId)
    {
        return definitions[fullId];
    }

    public static bool TryGet(
        string fullId,
        out BlockDefinition definition)
    {
        return definitions.TryGetValue(fullId, out definition);
    }
}