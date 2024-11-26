using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarPathfinding : MonoBehaviour
{
    public Transform player;
    public Tilemap collisionTilemap;
    public Vector2Int gridSize;

    // Added diagonal directions
    private Vector2Int[] directions = {
        new Vector2Int(1, 0),   // right
        new Vector2Int(-1, 0),  // left
        new Vector2Int(0, 1),   // up
        new Vector2Int(0, -1),  // down
        new Vector2Int(1, 1),   // up-right
        new Vector2Int(-1, 1),  // up-left
        new Vector2Int(1, -1),  // down-right
        new Vector2Int(-1, -1)  // down-left
    };

    public class Node
    {
        public Vector2Int position;
        public int gCost;
        public int hCost;
        public Node parent;

        public int fCost => gCost + hCost;

        public Node(Vector2Int position) { this.position = position; }
    }

    void Start()
    {
        BoundsInt bounds = collisionTilemap.cellBounds;
        gridSize = new Vector2Int(bounds.size.x, bounds.size.y);
    }

    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int target)
    {
        Debug.Log($"Finding path from {start} to {target}");
        
        if (!IsValidPosition(start))
        {
            Debug.LogError($"Invalid start position: {start}");
            return new List<Vector2Int>();
        }
        
        if (!IsValidPosition(target))
        {
            Debug.LogError($"Invalid target position: {target}");
            return new List<Vector2Int>();
        }

        List<Node> openList = new List<Node>();
        HashSet<Vector2Int> closedList = new HashSet<Vector2Int>();
        
        Node startNode = new Node(start);
        startNode.gCost = 0;
        startNode.hCost = CalculateHeuristic(start, target);
        
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Node currentNode = FindLowestFCostNode(openList);
            openList.Remove(currentNode);

            if (currentNode.position == target)
            {
                Debug.Log("Path found!");
                return ReconstructPath(currentNode);
            }

            closedList.Add(currentNode.position);

            foreach (var direction in directions)
            {
                Vector2Int neighborPos = currentNode.position + direction;
                
                if (!IsValidPosition(neighborPos) || closedList.Contains(neighborPos))
                    continue;

                // Calculate movement cost (1.4 for diagonal, 1 for cardinal directions)
                float movementCost = direction.x != 0 && direction.y != 0 ? 1.4f : 1f;

                // Check if diagonal movement is blocked by corner obstacles
                if (movementCost > 1f)
                {
                    Vector2Int cornerCheck1 = new Vector2Int(currentNode.position.x + direction.x, currentNode.position.y);
                    Vector2Int cornerCheck2 = new Vector2Int(currentNode.position.x, currentNode.position.y + direction.y);
                    
                    if (!IsValidPosition(cornerCheck1) || !IsValidPosition(cornerCheck2))
                    {
                        continue; // Skip this diagonal if corners are blocked
                    }
                }

                int tentativeGCost = currentNode.gCost + Mathf.RoundToInt(movementCost * 10);

                Node neighborNode = openList.Find(n => n.position == neighborPos);
                if (neighborNode == null)
                {
                    neighborNode = new Node(neighborPos);
                    neighborNode.gCost = tentativeGCost;
                    neighborNode.hCost = CalculateHeuristic(neighborPos, target);
                    neighborNode.parent = currentNode;
                    openList.Add(neighborNode);
                }
                else if (tentativeGCost < neighborNode.gCost)
                {
                    neighborNode.parent = currentNode;
                    neighborNode.gCost = tentativeGCost;
                }
            }
        }

        Debug.LogWarning("No path found!");
        return new List<Vector2Int>();
    }

    private int CalculateHeuristic(Vector2Int start, Vector2Int end)
    {
        // Using Diagonal distance heuristic
        int dx = Mathf.Abs(start.x - end.x);
        int dy = Mathf.Abs(start.y - end.y);
        return 10 * (dx + dy) + (14 - 2 * 10) * Mathf.Min(dx, dy);
    }

    private Node FindLowestFCostNode(List<Node> nodes)
    {
        Node lowest = nodes[0];
        for (int i = 1; i < nodes.Count; i++)
        {
            if (nodes[i].fCost < lowest.fCost || 
                (nodes[i].fCost == lowest.fCost && nodes[i].hCost < lowest.hCost))
            {
                lowest = nodes[i];
            }
        }
        return lowest;
    }

    public bool IsValidPosition(Vector2Int cellPosition)
    {
        Vector3Int tilePosition = new Vector3Int(cellPosition.x, cellPosition.y, 0);
        
        if (!collisionTilemap.cellBounds.Contains(tilePosition))
        {
            return false;
        }

        return !collisionTilemap.HasTile(tilePosition);
    }

    private List<Vector2Int> ReconstructPath(Node endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node currentNode = endNode;
        
        while (currentNode != null)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.parent;
        }
        
        path.Reverse();
        Debug.Log($"Path found with {path.Count} nodes");
        return path;
    }
}