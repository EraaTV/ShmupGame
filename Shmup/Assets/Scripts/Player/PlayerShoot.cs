﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using UnityEngine.Profiling;

public class PlayerShoot : MonoBehaviour
{
    public GameObject Bullet;
    public BulletSO BulletType;

    [SerializeField]
    GameObject FiringNode;

    // Temporarily track player health in this script until appropriate script is created
    public float currentHp, maxHp = 5;

    private void Start()
    {
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        Profiler.BeginSample("PlayerShoot_Update");

        FireOnKeyPress();

        Profiler.EndSample();
    }

    // Fire on left-mouse press
    public void FireOnKeyPress()
    {
        if (Bullet != null && BulletType != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Instantiate bullet at firing node
                GameObject TempBullet = Instantiate(Bullet, FiringNode.transform.position, FiringNode.transform.rotation);
                // Assign current enemy bullet type to instantiated bullet
                TempBullet.GetComponent<BulletWithSO>().BulletType = BulletType;
            }
        }
    }

    void TakeDamage(float damageTaken)
    {
        currentHp -= damageTaken;

        // Check if dead
        if (currentHp <= 0)
        {
            // Analytics event
            AnalyticsResult temp = Analytics.CustomEvent("player_died", new Dictionary<string, object>
            {
                { "scene_name", SceneManager.GetActiveScene().name },
                { "time_elapsed", Time.timeSinceLevelLoad }
            });
            Debug.Log("player_died event status: " + temp);

            // Load main menu on death
            SceneManager.LoadScene("MainMenu");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Bullet collision reaction
        if (collision.gameObject.layer == 8)
        {
            TakeDamage(collision.gameObject.GetComponent<BulletWithSO>().bltDmg);
        }
    }
}
