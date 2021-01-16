using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower Object", menuName = "Arif/Tower Defense/Tower")]
public class TowerObject : ScriptableObject
{
    [Range(2, 10)]
    public float range;
    [Range(0, 10)]
    public float coolDown;
    [Range(1, 10)]
    public float projectileSpeed;
    [Range(1, 10)]
    public int projectileDamage;
    [Range(0, 10)]
    public int price;
    public Firemode mode;

    
}
