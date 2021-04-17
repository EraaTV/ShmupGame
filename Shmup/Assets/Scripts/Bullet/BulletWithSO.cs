using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class BulletWithSO : MonoBehaviour
{
    public BulletSO BulletType;

    // Bullet properties
    public float bltSpd, bltAccel, bltDmg, bltLifetime, bltCreation, bltDeathTime;

    // Get component(s)
    Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bltCreation = Time.fixedTime;

        if (BulletType)
        {
            bltSpd = BulletType.bltSpd;
            bltAccel = BulletType.bltAccel;
            bltDmg = BulletType.bltDmg;
            bltLifetime = BulletType.bltLifetime;
            bltDeathTime = bltCreation + bltLifetime;
        }
    }

    private void FixedUpdate()
    {
        Profiler.BeginSample("BulletWithSO_FixedUpdate");

        // Move bullet by factor of bullet speed and bullet acceleration each frame
        rb.MovePosition((Vector3)rb.position + (transform.up * bltSpd) * Time.deltaTime);

        // Add acceleration
        bltSpd += bltAccel;

        // Enforce bullet lifetime
        if (Time.fixedTime > bltDeathTime)
        {
            Destroy(gameObject);
        }

        Profiler.EndSample();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy on collision
        Destroy(gameObject);
    }
}
