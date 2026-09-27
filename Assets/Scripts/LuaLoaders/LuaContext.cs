using MoonSharp.Interpreter;

[MoonSharpUserData]
public abstract class LuaContext
{
    public LuaTimeApi time { get; }
    public LuaRandomApi random { get; }
    public LuaWorldApi world { get; }

    protected LuaContext(
        LuaTimeApi time,
        LuaRandomApi random,
        LuaWorldApi world)
    {
        this.time = time;
        this.random = random;
        this.world = world;
    }
}

[MoonSharpUserData]
public sealed class LuaMobContext : LuaContext
{
    public LuaEntityApi self { get; }
    public LuaEntityStateApi state { get; }
    public LuaNavigationApi navigation { get; }
    public LuaPlayerApi player { get; }

    public LuaMobContext(
        LuaEntityApi self,
        LuaEntityStateApi state,
        LuaNavigationApi navigation,
        LuaPlayerApi player,
        LuaTimeApi time,
        LuaRandomApi random,
        LuaWorldApi world)
        : base(time, random, world)
    {
        this.self = self;
        this.state = state;
        this.navigation = navigation;
        this.player = player;
    }
}

[MoonSharpUserData]
public sealed class LuaBlockContext : LuaContext
{
    public LuaGridPosition pos { get; }
    public LuaBlockApi block { get; }
    public LuaBlockStateApi state { get; }
    public LuaPlayerApi player { get; }

    public LuaBlockContext(
        LuaGridPosition position,
        LuaBlockApi blockApi,
        LuaBlockStateApi stateApi,
        LuaPlayerApi playerApi,
        LuaTimeApi time,
        LuaRandomApi random,
        LuaWorldApi world)
        : base(time, random, world)
    {
        pos = position;
        block = blockApi;
        state = stateApi;
        player = playerApi;
    }
}