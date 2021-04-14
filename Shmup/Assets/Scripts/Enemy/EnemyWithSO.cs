using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWithSO : MonoBehaviour
{
    public EnemySO EnemyType;

    public GameObject Bullet;
    public BulletSO BulletType;

    [SerializeField]
    GameObject FiringNode;

    // Health values
    public float currentHp, maxHp = 3;

    // Movement variables
    [SerializeField]
    GameObject[] PathNodes = new GameObject[0];
    [SerializeField]
    int nodePos = -1;
    [SerializeField]
    Vector3 TargetLoc;

    [SerializeField]
    float enterSpeed = 4f, moveSpeed = 2f;
    [SerializeField]
    PathMode CurrentPath = PathMode.Patrol;

    // Shooting variables
    float fireRate = 1f;

    // Movement options for different path types
    // These modes can safely be switched en route between nodes or even after finishing a path
    enum PathMode
    {
        GoStay,
        Patrol
    }

    // Access Analytics Manager in Level1
    public GameObject analyticsManager;

    private void Start()
    {
        if (EnemyType)
        {
            enterSpeed = EnemyType.enterSpeed;
            moveSpeed = EnemyType.moveSpeed;
            fireRate = EnemyType.fireRate;
            maxHp = EnemyType.maxHp;
        }

        // Set health value
        currentHp = maxHp;

        if (PathNodes.Length > 0)
        {
            TargetLoc = PathNodes[0].transform.position;
        }
        else
        {
            TargetLoc = transform.position;
        }

        // Regularly shoot (if bullet object assigned)
        InvokeRepeating("Fire", fireRate, fireRate);
    }

    void Update()
    {
        if (currentHp <= 0)
        {
            analyticsManager.GetComponent<Analytics_EnemyDefeated>().enemyDefeatedNum += 1;

            // Enemy death at 0 hp
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        // In case no nodes are assigned
        if (PathNodes.Length > 0)
        {
            //Linear, node-to-node movement
            if (transform.position != TargetLoc)
            {
                // Update target position in case node moves
                if (nodePos < PathNodes.Length && nodePos > -1)
                {
                    TargetLoc = PathNodes[nodePos].transform.position;
                }

                // Entry speed is used to travel to the first node
                if (nodePos < 0)
                {
                    // Entry speed (moving to first node) is independent of the unit's general move speed
                    transform.position = Vector3.MoveTowards(transform.position, TargetLoc, enterSpeed * Time.deltaTime);
                }
                // Normal speed is used to travel between nodes thereafter
                else
                {
                    // Move towards target node
                    transform.position = Vector3.MoveTowards(transform.position, TargetLoc, moveSpeed * Time.deltaTime);
                }
            }
            // If node is reached, move to next node in list
            else
            {
                if (nodePos < PathNodes.Length - 1)
                {
                    // Queue next node as target
                    nodePos++;
                    TargetLoc = PathNodes[nodePos].transform.position;
                }
                else if (nodePos == PathNodes.Length - 1 && CurrentPath == PathMode.Patrol)
                {
                    // Reset to first node
                    nodePos = 0;
                    TargetLoc = PathNodes[nodePos].transform.position;
                }
            }
        }
    }

    public void Fire()
    {
        if (Bullet != null && BulletType != null)
        {
            // Instantiate bullet at firing node
            GameObject TempBullet = Instantiate(Bullet, FiringNode.transform.position, FiringNode.transform.rotation);
            // Assign current enemy bullet type to instantiated bullet
            TempBullet.GetComponent<BulletWithSO>().BulletType = BulletType;
        }
    }
}
