using System.Collections.Generic;

public static class MobRegistry
{
    private static readonly Dictionary<string, MobDefinition> definitions = new();

    public static void Register(MobDefinition definition)
    {
        definitions[definition.FullId] = definition;
    }

    public static bool TryGet(string id, out MobDefinition definition)
    {
        return definitions.TryGetValue(id, out definition);
    }
}
