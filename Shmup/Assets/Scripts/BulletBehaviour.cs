using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Bullet properties
    public float bltSpd, bltAccel, bltDmg, bltLifetime, bltCreation;

    // Get component(s)
    Rigidbody2D rb;

    private void Awake()
    {
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
}
