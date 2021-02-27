using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWithSO : MonoBehaviour
{
    public BulletSO BulletType;

    // Bullet properties
    public float bltSpd, bltAccel, bltDmg, bltLifetime, bltCreation;

    // Get component(s)
    Rigidbody2D rb;

    private void Start()
    {
        if (BulletType)
        {
            bltSpd = BulletType.bltSpd;
            bltAccel = BulletType.bltAccel;
            bltDmg = BulletType.bltDmg;
            bltLifetime = BulletType.bltLifetime;
        }

        rb = GetComponent<Rigidbody2D>();
        bltCreation = Time.fixedTime;
    }

    private void FixedUpdate()
    {
        // Move bullet by factor of bullet speed and bullet acceleration each frame
        rb.MovePosition((Vector3)rb.position + (transform.up * bltSpd) * Time.deltaTime);

        bltSpd += bltAccel;

        // Enforce bullet lifetime
        if (Time.fixedTime > bltCreation + bltLifetime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Temporary damage applier code
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<PlayerShoot>().currentHp -= bltDmg;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyWithSO>().currentHp -= bltDmg;
        }

        Destroy(gameObject);
    }
}
