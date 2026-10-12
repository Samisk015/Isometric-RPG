using UnityEngine;

public sealed class LuaMobBehaviourHost : MonoBehaviour
{
    private MobController mob;
    private LuaBehaviourModule behaviour;

    public void Initialize(
        MobController mobController,
        LuaBehaviourModule luaBehaviour)
    {
        mob = mobController ?? throw new System.ArgumentNullException(nameof(mobController));
        behaviour = luaBehaviour ?? throw new System.ArgumentNullException(nameof(luaBehaviour));
        RegisterIfReady();
    }

    private void OnEnable()
    {
        RegisterIfReady();
    }

    private void OnDisable()
    {
        MobDecisionScheduler.Instance?.Unregister(this);
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
        if (mob != null && mob.LivingEntity.IsAlive)
        {
            behaviour?.Call("on_tick", CreateContext(null, gameTick));
        }
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

    private void RegisterIfReady()
    {
        if (isActiveAndEnabled && mob != null && behaviour != null)
        {
            MobDecisionScheduler.Instance?.Register(this);
        }
    }
}
