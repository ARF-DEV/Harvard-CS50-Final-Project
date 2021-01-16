using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Settings", menuName = "Arif/Tower Defense/Enemy")]
public class EnemyObject : ScriptableObject{
    [Range(0, 20)]
    public float speed;
    [Range(0, 10)]
    public int health;
    [Range(0, 10)]
    public int bonus;
}