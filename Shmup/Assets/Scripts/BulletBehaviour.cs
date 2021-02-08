using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Bullet properties
    public float bltSpd, bltAccel;

    // Get component(s)
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Move bullet by factor of bullet speed and bullet acceleration each frame
        rb.MovePosition(rb.position + (Vector2.up * bltSpd) * Time.deltaTime);

        bltSpd += bltAccel;
    }
}
