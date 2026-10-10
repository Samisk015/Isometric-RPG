using UnityEngine;

public class OperationsOrder : MonoBehaviour
{
    private void Start()
    {
        if (GetComponent<MobDecisionScheduler>() == null) gameObject.AddComponent<MobDecisionScheduler>();
        if (GetComponent<BlockRandomTickSystem>() == null) gameObject.AddComponent<BlockRandomTickSystem>();
        if (GetComponent<MobRandomTickSystem>() == null) gameObject.AddComponent<MobRandomTickSystem>();
        RecourcePackManager.Instance.LoadBaseGameTextures();
        RecourcePackManager.Instance.LoadAllPacks();
        ModLoader.LoadBaseGame();
        ModLoader.LoadAllMods();

        Debug.Log("All mods and resource packs loaded successfully.");

        ChunkManager.Instance.Test();
    }
}
