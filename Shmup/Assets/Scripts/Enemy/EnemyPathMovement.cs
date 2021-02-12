using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A rudimentary script to move enemy units between nodes
// Will most likely be replaced with more advanced behaviour later on
public class EnemyPathMovement : MonoBehaviour
{
    // Other components
    EnemyShooting ShootScript;

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
    float firingRate = 1f;

    // Movement options for different path types
    // These modes can safely be switched en route between nodes or even after finishing a path
    enum PathMode
    {
        GoStay,
        Patrol
    }

    private void Awake()
    {
        if (GetComponent<EnemyShooting>())
        {
            ShootScript = GetComponent<EnemyShooting>();
        }

        if (PathNodes.Length > 0)
        {
            TargetLoc = PathNodes[0].transform.position;
        }
        else
        {
            TargetLoc = transform.position;
        }

        // Regularly shoot (if bullet object assigned)
        ShootScript.InvokeRepeating("Fire", firingRate, firingRate);
    }

    void FixedUpdate()
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
