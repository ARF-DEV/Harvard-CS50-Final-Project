using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool isPlaceable;
    public bool isWalkable;

    

    public Vector2 worldPos;

    public Node(bool placeable, bool walkable, Vector2 pos)
    {
        isPlaceable = placeable;
        isWalkable = walkable;
        worldPos = pos;
    }

    
}
