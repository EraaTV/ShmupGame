using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float MOVE_SPEED = 60f;

    private Rigidbody rigidbody;
    private Vector3 moveDir;
    private bool isDashButtonDown;

    float moveX = 0f;
    float moveY = 0f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        

        //Get Directional Input
        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A)){
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }

        //Direction of movement
        moveDir = new Vector3(moveX, moveY).normalized;

        //Additional command to make the player dash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashButtonDown = true;
        }
    }

    private void FixedUpdate()
    {
        //Move the player object in game
        rigidbody.velocity = moveDir * MOVE_SPEED;

        if (isDashButtonDown)
        {
            float dashAmount = 10f;
            rigidbody.MovePosition(transform.position + moveDir * dashAmount);
            isDashButtonDown = false;
        }
    }


}
