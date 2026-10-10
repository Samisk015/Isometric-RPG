using System.Collections.Generic;
using UnityEngine;

public sealed class MobRandomTickSystem : MonoBehaviour
{
    private const string BurnsInSunTag = "#burns_in_sun";
    private const string OnFireStateKey = "on_fire";

    private static readonly List<MobController> mobs = new();

    [SerializeField] private float ticksPerMobPerSecond = 1f;
    [SerializeField] private int sunDamagePerTick = 1;

    private readonly System.Random random = new();
    private float credit;

    public static void Register(MobController mob)
    {
        if (mob != null && !mobs.Contains(mob))
        {
            mobs.Add(mob);
        }
    }

    public static void Unregister(MobController mob)
    {
        mobs.Remove(mob);
    }

    private void Update()
    {
        if (World.Instance == null || ChunkManager.Instance == null || ticksPerMobPerSecond <= 0f)
        {
            return;
        }

        if (mobs.Count == 0)
        {
            credit = 0f;
            return;
        }

        credit += mobs.Count * ticksPerMobPerSecond * Time.deltaTime;
        int budget = Mathf.FloorToInt(credit);
        credit -= budget;

        for (int i = 0; i < budget && mobs.Count > 0; i++)
        {
            TickMob(mobs[random.Next(mobs.Count)]);
        }
    }

    private void TickMob(MobController mob)
    {
        if (mob == null || mob.Definition == null || mob.LivingEntity == null || !mob.LivingEntity.IsAlive)
        {
            return;
        }

        if (mob.Definition.HasTag(BurnsInSunTag))
        {
            bool exposedToSun = World.Instance.IsDaytime && IsExposedToSky(mob.GridPosition);
            if (exposedToSun)
            {
                mob.PersistentState.SetBool(OnFireStateKey, true);
                mob.LivingEntity.Damage(sunDamagePerTick);
            }
            else if (mob.PersistentState.GetBool(OnFireStateKey, false))
            {
                mob.PersistentState.SetBool(OnFireStateKey, false);
            }
        }

        
    }

    private static bool IsExposedToSky(Vector3Int position)
    {
        for (int z = position.z + 1; z < Chunk.CHUNK_HEIGHT; z++)
        {
            if (!World.Instance.TryGetBlock(new Vector3Int(position.x, position.y, z), out Block block) ||
                block.definition == null ||
                block.definition.FullId != "base:air")
            {
                return false;
            }
        }

        return true;
    }
}
