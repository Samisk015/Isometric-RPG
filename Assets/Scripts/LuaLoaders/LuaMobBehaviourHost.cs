using UnityEngine;

public sealed class LuaMobBehaviourHost : MonoBehaviour
{
    private MobController mob;
    private LuaBehaviourModule behaviour;

    public void Initialize(
        MobController mobController,
        LuaBehaviourModule luaBehaviour)
    {
        mob = mobController;
        behaviour = luaBehaviour;
        MobDecisionScheduler.Instance?.Register(this);
    }

    private void OnDestroy()
    {
        MobDecisionScheduler.Instance?.Unregister(this);
    }

    public void OnSpawn()
    {
        behaviour?.Call("on_spawn", CreateContext(null));
    }

    public void OnDecisionTick(long gameTick)
    {
        behaviour?.Call("on_tick", CreateContext(null, gameTick));
    }

    public void OnInteract(Player player)
    {
        behaviour?.Call(
            "on_interact",
            CreateContext(new LuaPlayerApi(player)));
    }

    private LuaMobContext CreateContext(
        LuaPlayerApi player,
        long gameTick = 0)
    {
        return new LuaMobContext(
            new LuaEntityApi(mob),
            new LuaEntityStateApi(mob.PersistentState),
            new LuaNavigationApi(mob.Navigation),
            player,
            new LuaTimeApi(gameTick),
            new LuaRandomApi(
                DeterministicSeed.ForEntity(
                    mob.PersistentId,
                    gameTick)),
            new LuaWorldApi(World.Instance));
    }
}
