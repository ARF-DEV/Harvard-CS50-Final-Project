using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyController : MonoBehaviour
{
    public void SetStats(EnemyObject stats)
    {
        speed = stats.speed;
        health = stats.health;
        bonus = stats.bonus;
    }
    private Grid gridSystem;
    private Queue<Node> walkableNode;
    
    private Node curNode = null;

    
    public float speed;
    
    public int health;
    
    public int bonus;
    private GameController gc;
    private float curSpeed;
    private float freezeTime = 0;

    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gridSystem = FindObjectOfType<Grid>();
        walkableNode = new Queue<Node>(gridSystem.walkables);
        gc = FindObjectOfType<GameController>();
    }

    
    void Update()
    {
        if (freezeTime > 0)
        {
            freezeTime -= Time.deltaTime;
            curSpeed = speed - 1;
            spriteRenderer.color = Color.blue;
            
        }
        else
        {
            
            curSpeed = speed;
            spriteRenderer.color = Color.white;
            
        }


        if (new Vector2(transform.position.x, transform.position.y) == gridSystem.end.worldPos)
        {
            Destroy(this.gameObject);
            GameController.EnemyAlive--;
            gc.health -= 1;
        }

        
        
        if ((curNode == null || Vector2.Distance(new Vector2(transform.position.x, transform.position.y), curNode.worldPos) <= 0.1f) && walkableNode.Count != 0)
        {
            curNode = walkableNode.Dequeue();
            Vector2 dis = curNode.worldPos - (Vector2)transform.position;
            float angle = Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg ;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Debug.Log("test");
            transform.position = Vector2.MoveTowards(transform.position, curNode.worldPos, curSpeed * Time.deltaTime);
        }
        
        if (health <= 0)
        {
            Destroy(this.gameObject);
            gc.money += bonus;
            GameController.EnemyAlive--;
        }

        
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(curNode.worldPos, new Vector2 (gridSystem.gridRadius * 2 - .1f, gridSystem.gridRadius * 2 - .1f));
    }

    public void SetFreezeEffect()
    {
        freezeTime = 2f;
    }
}
