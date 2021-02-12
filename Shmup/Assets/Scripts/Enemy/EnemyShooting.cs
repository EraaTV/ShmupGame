using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject BulletType;

    [SerializeField]
    GameObject FiringNode;

    public void Fire()
    {
        if (BulletType != null)
        {
            // Instantiate bullet at firing node
            Instantiate(BulletType, FiringNode.transform.position, transform.rotation);
        }
    }
}
