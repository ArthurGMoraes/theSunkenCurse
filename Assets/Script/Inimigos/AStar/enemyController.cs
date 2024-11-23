using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public Tilemap collisionTilemap;
    public float moveSpeed = 2f;
    public float pathUpdateInterval = 0.5f;
    public float rotationSpeed = 5f;
    
    private AStarPathfinding pathfinding;
    private List<Vector2Int> path;
    private int currentPathIndex = 0;
    private Rigidbody2D rb;
    private Vector3 currentMoveDirection = Vector3.right;

    void Start()
    {
        pathfinding = GetComponent<AStarPathfinding>();
        if (pathfinding == null)
        {
            pathfinding = gameObject.AddComponent<AStarPathfinding>();
        }
        
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        
        pathfinding.collisionTilemap = collisionTilemap;
        
        StartCoroutine(UpdatePath());
    }

    void Update()
    {
        if (path != null && path.Count > 0)
        {
            MoveAlongPath();
        }
    }

    IEnumerator UpdatePath()
    {
        while (true)
        {
            if (player == null || collisionTilemap == null)
            {
                Debug.LogError("Missing player or tilemap reference!");
                yield break;
            }

            Vector3Int enemyCell = collisionTilemap.WorldToCell(transform.position);
            Vector3Int playerCell = collisionTilemap.WorldToCell(player.position);

            Vector2Int enemyGridPos = new Vector2Int(enemyCell.x, enemyCell.y);
            Vector2Int playerGridPos = new Vector2Int(playerCell.x, playerCell.y);

            List<Vector2Int> newPath = pathfinding.FindPath(enemyGridPos, playerGridPos);

            if (newPath != null && newPath.Count > 0)
            {
                path = newPath;
                currentPathIndex = 0;
                Debug.Log($"New path found with {path.Count} nodes");
            }

            yield return new WaitForSeconds(pathUpdateInterval);
        }
    }

    void MoveAlongPath()
    {
        if (currentPathIndex >= path.Count) return;

        Vector3Int targetCell = new Vector3Int(path[currentPathIndex].x, path[currentPathIndex].y, 0);
        Vector3 targetPosition = collisionTilemap.GetCellCenterWorld(targetCell);
        
        // Calculate movement direction
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        
        // Only update direction if we're actually moving
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            currentMoveDirection = moveDirection;
            
            // Calculate the angle for rotation
            float targetAngle = Mathf.Atan2(currentMoveDirection.y, currentMoveDirection.x) * Mathf.Rad2Deg;
            
            // Smoothly rotate to the movement direction
            float currentAngle = rb.rotation;
            float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotationSpeed);
            rb.rotation = newAngle;
        }

        // Move towards the target
        transform.position = Vector3.MoveTowards(
            transform.position, 
            targetPosition, 
            moveSpeed * Time.deltaTime
        );

        // Check if we've reached the current target
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Look ahead to the next path point if available
            if (currentPathIndex < path.Count - 1)
            {
                Vector3Int nextCell = new Vector3Int(path[currentPathIndex + 1].x, path[currentPathIndex + 1].y, 0);
                Vector3 nextPosition = collisionTilemap.GetCellCenterWorld(nextCell);
                currentMoveDirection = (nextPosition - targetPosition).normalized;
            }
            currentPathIndex++;
        }
    }

    void OnDrawGizmos()
    {
        // Draw the path for debugging
        if (path != null && path.Count > 0)
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < path.Count - 1; i++)
            {
                Vector3 start = collisionTilemap.GetCellCenterWorld(new Vector3Int(path[i].x, path[i].y, 0));
                Vector3 end = collisionTilemap.GetCellCenterWorld(new Vector3Int(path[i + 1].x, path[i + 1].y, 0));
                Gizmos.DrawLine(start, end);
            }
        }

        // Draw movement direction for debugging
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, currentMoveDirection);
    }
}