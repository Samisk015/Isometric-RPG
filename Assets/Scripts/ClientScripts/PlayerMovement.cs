
using UnityEngine;
using UnityEngine.Tilemaps;

public enum BlockFace
{
    Top,
    Left,
    Right
}

public struct BlockSelection
{
    public Vector3Int blockPosition;
    public BlockFace face;
}

public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;

    [SerializeField] private float blockWidth = 1f;
    [SerializeField] private float topFaceHeight = 0.5f;
    // [SerializeField] private float topFaceYOffset = 0.25f;
    public GameObject tileHighlight;

    private void MoveTo(Vector3 destination)
    {
        gameObject.transform.position = destination;
    }

    private BlockFace GetHoveredFace(Vector2 mouseWorld, Vector2 blockCenter, float blockWidth, float topHeight)
        {
        Vector2 local = mouseWorld - blockCenter;

        float normalizedX = Mathf.Abs(local.x) / (blockWidth * 0.5f);
        float normalizedY = Mathf.Abs(local.y) / (topHeight * 0.5f);

        // Mouse is inside the top diamond.
        if (normalizedX + normalizedY <= 1f)
            return BlockFace.Top;

        return local.x < 0f
            ? BlockFace.Left
            : BlockFace.Right;
        }
   
    void Update()
    {
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D[] hits = Physics2D.RaycastAll(mouseWorld, Vector2.zero);

            int bestHeight = -1;
            Tilemap bestTilemap = null;
            Vector3Int bestCell = Vector3Int.zero;

            foreach (RaycastHit2D hit in hits)
            {
                Tilemap tilemap = hit.collider.GetComponent<Tilemap>();

                if (tilemap == null)
                    continue;

                int height = System.Array.IndexOf(
                    ChunkRenderer.Instance.tilemaps,
                    tilemap
                );

                if (height == -1)
                    continue;

                Vector3Int cell = tilemap.WorldToCell(mouseWorld);
                cell.z = 0;

                if (!tilemap.HasTile(cell))
                    continue;

                if (height > bestHeight)
                {
                    bestHeight = height;
                    bestTilemap = tilemap;
                    bestCell = cell;
                }
            }

            if (bestTilemap != null)
            {
                Vector3Int blockPos =
                    new Vector3Int(bestCell.x, bestCell.y, bestHeight);

                Debug.Log("Selected block: " + blockPos);
                if (!World.Instance.TryGetBlock(blockPos))
                    Debug.Log("NO BLOCK");
                
                // Debug.Log(World.Instance.GetBlock(blockPos).definition.FullId);
                Vector3 worldPos = ChunkRenderer.Instance.tilemaps[blockPos.z].GetCellCenterWorld(new Vector3Int(blockPos.x, blockPos.y, 0));

                BlockSelection selection = new BlockSelection{blockPosition = blockPos, face = GetHoveredFace(mouseWorld, worldPos, blockWidth, topFaceHeight)};
                Debug.Log(selection.face);
                tileHighlight.transform.position = worldPos;
                Vector3 playerTeleportPos = worldPos + Vector3Int.forward;
                if (Input.GetMouseButtonDown(1) && selection.face == BlockFace.Top)
                {
                    Vector3Int standingCell = selection.blockPosition + Vector3Int.forward;
                     if (standingCell.z < ChunkRenderer.Instance.tilemaps.Length)
                        {
                            Vector3 standingPosition =
                                ChunkRenderer.Instance.tilemaps[standingCell.z]
                                    .GetCellCenterWorld(
                                        new Vector3Int(
                                            standingCell.x,
                                            standingCell.y,
                                            0
                                        )
                                    );

                            MoveTo(standingPosition);
                        }
                }
                
            }
    }
}
