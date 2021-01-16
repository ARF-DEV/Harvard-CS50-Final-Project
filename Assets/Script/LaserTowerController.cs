using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTowerController : TowerController
{
    LineRenderer lineRenderer;

    float laserTime;
    void Start()
    {
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        
    }
    public override void shoot()
    {
        laserTime -= Time.deltaTime;
        if (laserTime > 0)
        {
            lineRenderer.enabled = true;
            
        }
        else
        {
            lineRenderer.enabled = false;
        } 


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
                
            if (Vector2.Distance(target.transform.position, transform.position) <= range && delay <= 0)
            {

                
                lineRenderer.SetPosition(0, projPoint.position);
                lineRenderer.SetPosition(1, target.transform.position);
                laserTime = coolDown/4;
                target.SetFreezeEffect();
                target.health -= projectileDamage;
                
            }
            if (Vector2.Distance(target.transform.position, transform.position) > range)
            {
                target = null;
            }
            
            
            
        }

    }

    
}
