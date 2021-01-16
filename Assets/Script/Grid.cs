using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Grid : MonoBehaviour
{
    public Texture2D mapImg;
    public float gridRadius;
    
    public LayerMask unWalkable;
    public Queue<Node> walkables;
    public List<Node> container;
    public Node start, end;
    public Vector2 TopLeft;


    public Tilemap tilemap;

    public Tile grass;
    public Tile road;
    Node[,] grid;
    int gridDiameter;
    
    Vector2 worldSize;
    int gridCountX, gridCountY;

    
    protected void Awake()
    {
        gridDiameter = Mathf.RoundToInt(gridRadius * 2);
        walkables = new Queue<Node>();
        container = new List<Node>();
        worldSize.x = mapImg.width * gridDiameter;
        worldSize.y = mapImg.height * gridDiameter;
        
        TopLeft = transform.position - new Vector3(worldSize.x / 2, -worldSize.y / 2, 0);
        
        //gridCountX = Mathf.RoundToInt(worldSize.x / gridDiameter);
        //gridCountY = Mathf.RoundToInt(worldSize.y / gridDiameter);

        gridCountX = mapImg.width / gridDiameter;
        gridCountY = mapImg.height / gridDiameter;
        
        grid = new Node[gridCountX, gridCountY];
        
        int WSy = (int)worldSize.y;
        for (int i = 0; i < gridCountX; i++)
        {
            for (int j = 0; j < gridCountY; j++)
            {
                Vector2 nodePos = TopLeft + new Vector2(
                    i * gridDiameter + gridRadius,
                    -j * gridDiameter - gridRadius
                );
                
                
                Color curPix = mapImg.GetPixel(i, Mathf.RoundToInt(WSy - j - 1));
                if (curPix != Color.red && curPix != Color.green && curPix != Color.blue && curPix != Color.black && curPix != Color.white)
                {
                    Debug.LogError("Error Color Not Match! :" + curPix);
                }
                bool placeable = false, walkable = false;
                if (curPix == Color.red)
                {
                    placeable = false;
                    walkable = !placeable;
                    tilemap.SetTile(tilemap.WorldToCell(nodePos), road);
                    
                }
                else if (curPix == Color.green)
                {
                    placeable = true;
                    walkable = !placeable;
                    tilemap.SetTile(tilemap.WorldToCell(nodePos), grass);
                    
                }
                else if (curPix == Color.blue)
                {
                    placeable = false;
                    walkable = false;
                    tilemap.SetTile(tilemap.WorldToCell(nodePos), grass);
                }
                
                //
                // TODO INSTATIATE SPRITE BASE ON PIXEL COLOR
                //

                //bool placeable = Physics2D.OverlapCircle(nodePos, gridRadius, unWalkable);
                //bool walkable = !placeable;
                
                grid[j, i] = new Node(placeable, walkable, nodePos);
                if (curPix == Color.white)
                {
                    
                    
                    start = grid[j, i];
                    tilemap.SetTile(tilemap.WorldToCell(nodePos), road); 
                }
                else if (curPix == Color.black)
                {
                    end = grid[j, i];
                    tilemap.SetTile(tilemap.WorldToCell(nodePos), road);
                }
                
            }
            
    
        }
        
        Node p = start;
        
        while(p != end && p != null)
        {
            //Debug.Log(p.worldPos);
            walkables.Enqueue(p);
            p = GetNextWalkable(p);
        }

        walkables.Enqueue(end);

        //Debug.Log(walkables.Count);
    }

    void OnDrawGizmosSelected()
    {
        if (grid != null)
        {
            foreach (Node node in grid)
            {
                if (node.isPlaceable && !node.isWalkable)
                {
                    Gizmos.color = Color.green;
                }
                else if (!node.isPlaceable && node.isWalkable)
                {
                    Gizmos.color = Color.clear;
                }
                else
                {
                    Gizmos.color = Color.blue;
                }
                Gizmos.DrawWireCube(node.worldPos, new Vector2 (gridDiameter - .1f, gridDiameter - .1f));
            }
        }
        if (start != null && end != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawCube(start.worldPos, new Vector2 (gridDiameter - .1f, gridDiameter - .1f));
            Gizmos.color = Color.black;
            Gizmos.DrawCube(end.worldPos, new Vector2 (gridDiameter - .1f, gridDiameter - .1f));
        }
        
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(Vector2.zero, worldSize);
        
    }

    public Node WorldPointToNode(Vector2 pos)
    {
        if (pos == null) return grid[0,0];
        int x = (int)Mathf.Clamp((worldSize.x/2 + pos.x) / gridDiameter, 0, gridCountX);
        int y = (int)Mathf.Clamp((worldSize.y/2 - pos.y) / gridDiameter, 0, gridCountY);

        
        return grid[y, x]; 
        
    }
    private Node GetNextWalkable(Node currentNode)
    {
        Node A = null;
        
        Vector2 pos = currentNode.worldPos;
        
        int x = (int)Mathf.Clamp((worldSize.x/2 + pos.x) / gridDiameter, 0, gridCountX);
        int y = (int)Mathf.Clamp((worldSize.y/2 - pos.y) / gridDiameter, 0, gridCountY);
        
        if (grid[y, Mathf.Clamp(x + 1, 0, gridCountX)].isWalkable && !container.Contains(grid[y, Mathf.Clamp(x + 1, 0, gridCountX)]))
        {
            A = grid[y, Mathf.Clamp(x + 1, 0, gridCountX)];
        }
        else if(grid[y, Mathf.Clamp(x - 1, 0, gridCountX)].isWalkable && !container.Contains(grid[y, Mathf.Clamp(x - 1, 0, gridCountX)]))
        {
            A = grid[y, Mathf.Clamp(x - 1, 0, gridCountX)];
        }
        else if(grid[Mathf.Clamp(y + 1, 0, gridCountY), x].isWalkable && !container.Contains(grid[Mathf.Clamp(y + 1, 0, gridCountY), x]))
        {
            A = grid[Mathf.Clamp(y + 1, 0, gridCountY), x];
        }
        else if(grid[Mathf.Clamp(y - 1, 0, gridCountY), x].isWalkable && !container.Contains(grid[Mathf.Clamp(y - 1, 0, gridCountY), x]))
        {
            A = grid[Mathf.Clamp(y - 1, 0, gridCountY), x];
        }
        container.Add(A);
        return A;

    }
    
}
