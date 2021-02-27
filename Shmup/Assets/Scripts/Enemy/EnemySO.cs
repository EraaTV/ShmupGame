using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scriptable object for creating enemy base stat presets
[CreateAssetMenu(fileName = "EnemyType", menuName = "ScriptableObjects/EnemyType", order = 1)]
public class EnemySO : ScriptableObject
{
    public string enemyName;

    public float enemyMaxHp = 3, fireRate = 1, enterSpeed = 1, moveSpeed = 1;
}
