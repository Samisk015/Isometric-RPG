using MoonSharp.Interpreter;
using UnityEngine;

[MoonSharpUserData]
public sealed class LuaGridPosition
{
    public int x { get; }
    public int y { get; }
    public int z { get; }

    public LuaGridPosition(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public LuaGridPosition(Vector3Int position)
        : this(position.x, position.y, position.z)
    {
    }

    public LuaGridPosition offset(int offsetX, int offsetY, int offsetZ)
    {
        return new LuaGridPosition(x + offsetX, y + offsetY, z + offsetZ);
    }

    public Vector3Int ToUnityPosition()
    {
        return new Vector3Int(x, y, z);
    }
}

[MoonSharpUserData]
public sealed class LuaGridPosition2D
{
    public int x { get; }
    public int y { get; }

    public LuaGridPosition2D(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public LuaGridPosition2D(Vector2Int position)
        : this(position.x, position.y)
    {
    }

    public LuaGridPosition2D offset(int offsetX, int offsetY)
    {
        return new LuaGridPosition2D(x + offsetX, y + offsetY);
    }

    public Vector2Int ToUnityPosition()
    {
        return new Vector2Int(x, y);
    }
}

[MoonSharpUserData]
public sealed class LuaEntityApi
{
    private readonly MobController mob;

    public LuaEntityApi(MobController mob)
    {
        this.mob = mob;
    }

    public string id()
    {
        return mob.Definition.FullId;
    }

    public int health()
    {
        return mob.LivingEntity.Health;
    }

    public bool is_alive()
    {
        return mob.LivingEntity.IsAlive;
    }

    public LuaGridPosition position()
    {
        return new LuaGridPosition(mob.GridPosition);
    }

    public bool has_tag(string tagId)
    {
        return mob.Definition.Tags != null &&
               System.Array.Exists(mob.Definition.Tags, tag => tag == tagId);
    }
}

[MoonSharpUserData]
public sealed class LuaEntityStateApi
{
    private readonly EntityState state;

    public LuaEntityStateApi(EntityState state)
    {
        this.state = state;
    }

    public bool has(string key)
    {
        return state.Has(key);
    }

    public void set_number(string key, double value)
    {
        state.SetNumber(key, value);
    }

    public double get_number(string key, double defaultValue = 0)
    {
        return state.GetNumber(key, defaultValue);
    }

    public void set_bool(string key, bool value)
    {
        state.SetBool(key, value);
    }

    public bool get_bool(string key, bool defaultValue = false)
    {
        return state.GetBool(key, defaultValue);
    }

    public void set_string(string key, string value)
    {
        state.SetString(key, value);
    }

    public string get_string(string key, string defaultValue = "")
    {
        return state.GetString(key, defaultValue);
    }

    public void set_position(LuaGridPosition value)
    {
        state.SetPosition(value.ToUnityPosition());
    }

    // public LuaGridPosition get_position(string key, LuaGridPosition defaultValue = null)
    // {
    //     return new LuaGridPosition(
    //         state.GetPosition(
    //             key,
    //             defaultValue?.ToUnityPosition() ?? default));
    // }

    public LuaGridPosition get_position()
    {
        return new LuaGridPosition(state.GetPosition());
    }
}

[MoonSharpUserData]
public sealed class LuaTimeApi
{
    public long game_tick { get; }

    public LuaTimeApi(long gameTick)
    {
        game_tick = gameTick;
    }
}

[MoonSharpUserData]
public sealed class LuaRandomApi
{
    private readonly System.Random random;

    public LuaRandomApi(int seed)
    {
        random = new System.Random(seed);
    }

    public int range_int(int minimumInclusive, int maximumInclusive)
    {
        return random.Next(minimumInclusive, maximumInclusive + 1);
    }

    public double value()
    {
        return random.NextDouble();
    }
}

[MoonSharpUserData]
public sealed class LuaNavigationApi
{
    private readonly GridNavigationAgent navigation;

    public LuaNavigationApi(GridNavigationAgent navigation)
    {
        this.navigation = navigation;
    }

    public void move_to(LuaGridPosition target)
    {
        navigation.RequestMove(target.ToUnityPosition());
    }

    public void stop()
    {
        navigation.Stop();
    }

    public bool is_moving()
    {
        return navigation.IsMoving;
    }
}

[MoonSharpUserData]
public sealed class LuaPlayerApi
{
    private readonly Player player;

    public LuaPlayerApi(Player player)
    {
        this.player = player;
    }

    public bool is_valid() => player != null;

    public Gamemode get_gamemode() => player.GetGamemode();

    public void set_gamemode(Gamemode mode) => player.SetGamemode(mode);
}

[MoonSharpUserData]
public sealed class LuaBlockApi
{
    private readonly Block block;

    public LuaBlockApi(Block block)
    {
        this.block = block;
    }

    public string id() => block?.definition?.FullId ?? "base:air";
    public bool has_tag(string tagId) => block?.definition != null && block.definition.HasTag(tagId);
}

[MoonSharpUserData]
public sealed class LuaBlockStateApi
{
    private readonly EntityState state;

    public LuaBlockStateApi(EntityState state)
    {
        this.state = state;
    }

    public void set_number(string key, double value) => state.SetNumber(key, value);
    public double get_number(string key, double fallback = 0) => state.GetNumber(key, fallback);
    public void set_bool(string key, bool value) => state.SetBool(key, value);
    public bool get_bool(string key, bool fallback = false) => state.GetBool(key, fallback);
    public void set_string(string key, string value) => state.SetString(key, value);
    public string get_string(string key, string fallback = "") => state.GetString(key, fallback);
}

[MoonSharpUserData]
public sealed class LuaWorldApi
{
    private readonly World world;

    public LuaWorldApi(World world)
    {
        this.world = world;
    }

    public float get_temp_mutli()
    {
        return world.GetTempMutli();
    }

    public string get_season()
    {
        return world.GetSeason();
    }

    public bool is_air(LuaGridPosition position)
    {
        return world != null && world.IsAir(position.ToUnityPosition());
    }

    public bool block_has_tag(LuaGridPosition position, string tagId)
    {
        return world != null && world.TryGetBlock(position.ToUnityPosition(), out Block block) && block.definition.HasTag(tagId);
    }

    public bool set_block(LuaGridPosition position, string blockId)
    {
        return world != null && world.SetBlock(position.ToUnityPosition(), blockId);
    }

    public void update_block_texture(LuaGridPosition position, string textureId)
    {
        // to add later: check if its a registered texture and check if it is that block's texture

        if (!(world.TryGetBlock(position.ToUnityPosition()))) return;

        Block block = world.GetBlock(position.ToUnityPosition());

        if (block != null)
        {
            ChunkRenderer.Instance.RenderBlock(position.ToUnityPosition(), block, textureId);
        }
    }

    public void message(LuaPlayerApi player, string text)
    {
        Debug.Log($"[Lua] {text}");
    }
}
