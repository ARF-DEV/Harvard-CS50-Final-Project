using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public void SetStats(TowerObject stats)
    {
        projectileSpeed = stats.projectileSpeed;
        projectileDamage = stats.projectileDamage;
        coolDown = stats.coolDown;
        mode = stats.mode;
        range = stats.range;
    }
    public Firemode mode;
    public float range = 10f;
    public float coolDown;
    public ProjectileController projectile;
    public float projectileSpeed;
    public int projectileDamage;

    public Transform projPoint;
    protected float delay = 0;

    protected EnemyController[] enemy;
    protected EnemyController target;
    

    // Update is called once per frame
    void Update()
    {
        enemy = FindObjectsOfType<EnemyController>();
        delay -= Time.deltaTime;

        shoot();

        if (delay <= 0)
        {
            delay = coolDown;
        }

    }

    public virtual void shoot()
    {
        
        if (target == null)
        {   
            if (enemy.Length == 0)
                return;

            foreach (EnemyController i in enemy)
            {
                if (Vector2.Distance(i.transform.position, transform.position) <= range)
                {
                    target = i;
                }
            }
        }
        else
        {
            Vector3 dis = target.transform.position - transform.position;
            float angle = Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (delay <= 0 && Vector2.Distance(target.transform.position, transform.position) <= range)
            {
                ProjectileController proj = Instantiate(projectile, projPoint.position, Quaternion.identity);
                proj.damage = projectileDamage;
                proj.speed = projectileSpeed;
                proj.target = target;
                    
            }
            if (Vector2.Distance(target.transform.position, transform.position) > range)
            {
                target = null;
            }
            
        }


    }  

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    
}
