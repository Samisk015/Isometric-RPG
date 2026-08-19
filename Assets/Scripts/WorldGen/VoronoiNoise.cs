using System.Collections.Generic;
using UnityEngine;

public static class VoronoiNoise
{

    public static List<Vector2Int> points = new List<Vector2Int>();
    public static float Sample(
        float worldX,
        float worldY,
        float scale,
        int seed)
    {
        float x = worldX / scale;
        float y = worldY / scale;

        int cellX = Mathf.FloorToInt(x);
        int cellY = Mathf.FloorToInt(y);

        float nearestDistance = float.MaxValue;

        for (int offsetX = -2; offsetX <= 2; offsetX++)
        {
            for (int offsetY = -2; offsetY <= 2; offsetY++)
            {
                int neighbourX = cellX + offsetX;
                int neighbourY = cellY + offsetY;

                Vector2 randomOffset =
                    GetRandomOffset(neighbourX, neighbourY, seed);

                Vector2 point = new Vector2(
                    neighbourX + randomOffset.x,
                    neighbourY + randomOffset.y
                );

                float distance = Vector2.Distance(
                    new Vector2(x, y),
                    point
                );

                nearestDistance =
                    Mathf.Min(nearestDistance, distance);
            }
        }

        return nearestDistance;
    }

    private static Vector2 GetRandomOffset(
        int x,
        int y,
        int seed)
    {
        return new Vector2(
            HashToFloat(x, y, seed),
            HashToFloat(x, y, seed + 1000)
        );
    }

    private static float HashToFloat(
        int x,
        int y,
        int seed)
    {
        unchecked
        {
            uint hash = (uint)seed;
            hash ^= (uint)x * 374761393u;
            hash ^= (uint)y * 668265263u;
            hash = (hash ^ (hash >> 13)) * 1274126177u;
            hash ^= hash >> 16;

            return hash / (float)uint.MaxValue;
        }
    }
}