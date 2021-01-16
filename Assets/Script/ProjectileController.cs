using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public EnemyController target;
    public float speed;
    public int damage;
    void Start()
    {
        Debug.Log(speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        else 
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            Vector2 dis = target.transform.position - transform.position;
            float angle = Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        EnemyController enemy = col.gameObject.GetComponent<EnemyController>(); 
        if (enemy != null)
        {
            enemy.health -= damage;
            Destroy(this.gameObject);
        }
    }
}
