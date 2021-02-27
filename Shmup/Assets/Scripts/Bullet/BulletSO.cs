using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scriptable object for creating enemy base stat presets
[CreateAssetMenu(fileName = "BulletType", menuName = "ScriptableObjects/BulletType", order = 1)]
public class BulletSO : ScriptableObject
{
    // Bullet properties
    public float bltSpd, bltAccel, bltDmg, bltLifetime;
}
