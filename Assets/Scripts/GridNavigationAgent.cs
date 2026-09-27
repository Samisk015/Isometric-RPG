using System;
using UnityEngine;

// This is intentionally a request interface. A later pathfinding implementation can
// subscribe to MoveRequested without changing the Lua API.
public sealed class GridNavigationAgent : MonoBehaviour
{
    public bool IsMoving { get; private set; }
    public Vector3Int Target { get; private set; }
    public event Action<Vector3Int> MoveRequested;

    public void RequestMove(Vector3Int target)
    {
        Target = target;
        IsMoving = true;
        MoveRequested?.Invoke(target);
    }

    public void Stop()
    {
        IsMoving = false;
    }

    public void MarkArrived()
    {
        IsMoving = false;
    }
}
