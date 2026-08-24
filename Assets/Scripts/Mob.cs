using UnityEngine;

public class Mob : LivingEntity
{
    public Vector3Int GridPosition { get; private set; }

    public void MoveRandomly()
    {
        // Actual Unity movement.
    }

    public void Say(string message)
    {
        Debug.Log(message);
    }
}

public class MobApi
{
    
}