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
    public float aggroRange = 5f; // Distance at which enemy starts following player
    public float deaggroRange = 7f; // Distance at which enemy stops following player (slightly larger to prevent constant switching)
    
    private AStarPathfinding pathfinding;
    private List<Vector2Int> path;
    private int currentPathIndex = 0;
    private Rigidbody2D rb;
    private Vector3 currentMoveDirection = Vector3.right;
    private bool isAggro = false;
    private Vector3 startingPosition; // Store the enemy's initial position

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
        startingPosition = transform.position; // Save the starting position
        
        StartCoroutine(UpdatePath());
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if player is within aggro range
        if (!isAggro && distanceToPlayer <= aggroRange)
        {
            isAggro = true;
            Debug.Log("Player entered aggro range!");
        }
        // Check if player has left the deaggro range
        else if (isAggro && distanceToPlayer > deaggroRange)
        {
            isAggro = false;
            path = null; // Clear the current path
            ReturnToStart();
            Debug.Log("Player left aggro range!");
        }

        // Only move if we have a path and are either aggro'd on the player or returning home
        if (path != null && path.Count > 0)
        {
            MoveAlongPath();
        }
    }

    void ReturnToStart()
    {
        Vector3Int startCell = collisionTilemap.WorldToCell(startingPosition);
        Vector3Int currentCell = collisionTilemap.WorldToCell(transform.position);
        
        Vector2Int startGridPos = new Vector2Int(startCell.x, startCell.y);
        Vector2Int currentGridPos = new Vector2Int(currentCell.x, currentCell.y);

        path = pathfinding.FindPath(currentGridPos, startGridPos);
        currentPathIndex = 0;
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

            if (isAggro) // Only update path to player if aggro'd
            {
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
        // Draw the aggro ranges
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, deaggroRange);

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