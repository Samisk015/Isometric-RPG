using UnityEngine;
using MoonSharp.Interpreter;
using System.IO;

public class MobAI : MonoBehaviour
{
    public float speed = 3.0f;
    public Transform player;

    private Script luaScript;
    private DynValue updateBehaviorFunc;

    void Start()
    {
        // 1. Register types so MoonSharp can interact with C#
        UserData.RegisterType<MobAI>();
        UserData.RegisterType<Vector3>();

        luaScript = new Script();

        // 2. Pass this mob instance to Lua as a global variable
        luaScript.Globals["mob"] = this;

        // 3. Load the Lua mod file (e.g. from a StreamingAssets/Mods folder)
        string modPath = Path.Combine(Application.streamingAssetsPath, "Mods/mob_behavior.lua");
        if (File.Exists(modPath))
        {
            luaScript.DoFile(modPath);
            updateBehaviorFunc = luaScript.Globals.Get("UpdateBehavior");
        }
    }

    void Update()
    {
        // 4. Call the Lua function every frame to process AI logic
        if (updateBehaviorFunc != null && updateBehaviorFunc.Type == DataType.Function)
        {
            luaScript.Call(updateBehaviorFunc);
        }
    }

    // --- Helper Methods Exposed to Lua ---

    public Vector3 GetPlayerPosition()
    {
        return player.position;
    }

    public Vector3 GetMobPosition()
    {
        return transform.position;
    }

    public void MoveTowards(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void AttackPlayer()
    {
        Debug.Log("Mob attacks the player!");
    }
}