using MoonSharp.Interpreter;

public sealed class LuaBehaviourModule
{
    public string Id { get; }
    public Script Script { get; }
    public Table Module { get; }

    public LuaBehaviourModule(string id, Script script, Table module)
    {
        Id = id;
        Script = script;
        Module = module;
    }

    public bool HasCallback(string callbackName)
    {
        return Module.Get(callbackName).Type == DataType.Function;
    }

    public void Call(string callbackName, LuaContext context)
    {
        DynValue callback = Module.Get(callbackName);

        if (callback.Type != DataType.Function)
        {
            return;
        }

        Script.Call(callback, context);
    }
}