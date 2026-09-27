using System.Collections.Generic;
using UnityEngine;

public sealed class MobDecisionScheduler : MonoBehaviour
{
    public static MobDecisionScheduler Instance { get; private set; }
    [SerializeField] private float decisionsPerSecond = 5f;

    private readonly List<LuaMobBehaviourHost> mobs = new();
    private int nextMobIndex;
    private long gameTick;
    private float workCredit;

    private void Awake()
    {
        Instance = this;
    }

    public void Register(LuaMobBehaviourHost host)
    {
        if (host != null && !mobs.Contains(host)) mobs.Add(host);
    }

    public void Unregister(LuaMobBehaviourHost host)
    {
        mobs.Remove(host);
    }

    private void Update()
    {
        gameTick++;
        if (mobs.Count == 0 || decisionsPerSecond <= 0f) return;
        workCredit += mobs.Count * decisionsPerSecond * Time.deltaTime;
        int budget = Mathf.FloorToInt(workCredit);
        workCredit -= budget;

        for (int i = 0; i < budget && mobs.Count > 0; i++)
        {
            if (nextMobIndex >= mobs.Count)
            {
                nextMobIndex = 0;
            }

            LuaMobBehaviourHost host = mobs[nextMobIndex];
            if (host != null) host.OnDecisionTick(gameTick);
            nextMobIndex++;
        }
    }
}
