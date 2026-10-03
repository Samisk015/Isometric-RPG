using System;
using System.Collections.Generic;
using System.IO;
using MoonSharp.Interpreter;
using UnityEngine;

public sealed class LuaBehaviourRegistry
{
    public static LuaBehaviourRegistry Instance { get; } = new();

    private readonly Dictionary<string, LuaBehaviourModule> mobBehaviours = new();
    private readonly Dictionary<string, LuaBehaviourModule> blockBehaviours = new();

    private LuaBehaviourRegistry()
    {
        RegisterApiTypes();
    }

    private static void RegisterApiTypes()
    {
        UserData.RegisterType<LuaMobContext>();
        UserData.RegisterType<LuaBlockContext>();
        UserData.RegisterType<LuaEntityApi>();
        UserData.RegisterType<LuaEntityStateApi>();
        UserData.RegisterType<LuaNavigationApi>();
        UserData.RegisterType<LuaWorldApi>();
        UserData.RegisterType<LuaPlayerApi>();
        UserData.RegisterType<LuaTimeApi>();
        UserData.RegisterType<LuaRandomApi>();
        UserData.RegisterType<LuaGridPosition>();
        UserData.RegisterType<LuaBlockApi>();
        UserData.RegisterType<LuaBlockStateApi>();
    }

    public void RegisterMobBehaviour(string id, string source)
    {
        mobBehaviours[id] = LoadModule(id, source);
    }

    public void RegisterBlockBehaviour(string id, string source)
    {
        blockBehaviours[id] = LoadModule(id, source);
    }

    public void LoadFromDirectory(string modPath, string modNamespace)
    {
        LoadDirectory(
            Path.Combine(modPath, "Lua", "MobBehaviours"),
            modNamespace,
            RegisterMobBehaviour);
        LoadDirectory(
            Path.Combine(modPath, "Lua", "BlockBehaviours"),
            modNamespace,
            RegisterBlockBehaviour);
    }

    public bool TryGetMobBehaviour(string id, out LuaBehaviourModule behaviour)
    {
        return mobBehaviours.TryGetValue(id, out behaviour);
    }

    public bool TryGetBlockBehaviour(string id, out LuaBehaviourModule behaviour)
    {
        return blockBehaviours.TryGetValue(id, out behaviour);
    }

    private static void LoadDirectory(
        string directory,
        string modNamespace,
        Action<string, string> register)
    {
        if (!Directory.Exists(directory))
        {
            return;
        }

        foreach (string path in Directory.GetFiles(directory, "*.lua", SearchOption.AllDirectories))
        {
            string id = $"{modNamespace}:{Path.GetFileNameWithoutExtension(path)}";
            try
            {
                register(id, File.ReadAllText(path));
                Debug.Log($"Loaded Lua behaviour: {id}");
            }
            catch (Exception exception)
            {
                Debug.LogError($"Lua behaviour '{id}' could not load: {exception.Message}");
            }
        }
    }

    private static LuaBehaviourModule LoadModule(string id, string source)
    {
        Script script = new Script(CoreModules.Preset_HardSandbox);
        DynValue result = script.DoString(source);

        if (result.Type != DataType.Table)
        {
            throw new ScriptRuntimeException($"Lua behavior '{id}' must return a table.");
        }

        return new LuaBehaviourModule(id, script, result.Table);
    }
}
