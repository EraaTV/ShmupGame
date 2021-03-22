using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

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
        if (currentHp <= 0)
        {
            // Analytics event
            AnalyticsResult temp = Analytics.CustomEvent("PlayerDies" + SceneManager.GetActiveScene());
            Debug.Log("PlayerDies event status: " + temp);
            
            // Load main menu on death
            SceneManager.LoadScene("MainMenu");
        }

        FireOnKeyPress();
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
}
