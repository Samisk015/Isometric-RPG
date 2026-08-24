// using MoonSharp.Interpreter;
// using MoonSharp.Interpreter.Loaders;

// public static class LuaManager
// {
//     public static Script Script { get; private set; }

//     public static Table CreateTable()
//     {
//         return new Table(Script);
//     }

//     public static Table LoadScript(string path)
//     {
//         DynValue result = Script.DoFile(path);

//         if (result.Type != DataType.Table)
//             throw new ScriptRuntimeException(
//                 $"Lua behavior '{path}' must return a table."
//             );

//         return result.Table;
//     }

//     public static void Initialize(string luaFolder)
//     {
//         UserData.RegisterType<MobApi>();

//         Script = new Script(
//             CoreModules.Preset_SoftSandbox
//         );

//         FileSystemScriptLoader loader =
//             new FileSystemScriptLoader();

//         loader.ModulePaths = new[]
//         {
//             luaFolder + "/?.lua",
//             luaFolder + "/?/init.lua"
//         };

//         Script.Options.ScriptLoader = loader;
//     }
// }