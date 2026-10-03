using UnityEngine;

public sealed class MobSpawner : MonoBehaviour
{
    [SerializeField] private GameObject mobPrefab;

    public GameObject Spawn(string mobId, Vector3Int position, string persistentId = null)
    {
        if (mobPrefab == null || !MobRegistry.TryGet(mobId, out MobDefinition definition))
        {
            Debug.LogWarning($"Cannot spawn mob '{mobId}'. Missing prefab or definition.");
            return null;
        }

        GameObject instance = Instantiate(mobPrefab);
        MobController controller = instance.GetComponent<MobController>() ?? instance.AddComponent<MobController>();
        LuaMobBehaviourHost luaHost = instance.GetComponent<LuaMobBehaviourHost>() ?? instance.AddComponent<LuaMobBehaviourHost>();
        controller.Initialize(definition, persistentId, position);

        if (!string.IsNullOrWhiteSpace(definition.BehaviourId) && LuaBehaviourRegistry.Instance.TryGetMobBehaviour(definition.BehaviourId, out LuaBehaviourModule behaviour))
        {
            luaHost.Initialize(controller, behaviour);
            luaHost.OnSpawn();
        }

        return instance;
    }
}
