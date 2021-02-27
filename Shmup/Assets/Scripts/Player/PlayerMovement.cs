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

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float moveX = 0f;
        float moveY = 0f;


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

        moveDir = new Vector3(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashButtonDown = true;
        }
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = moveDir * MOVE_SPEED;

        if (isDashButtonDown)
        {
            float dashAmount = 10f;
            rigidbody.MovePosition(transform.position + moveDir * dashAmount);
            isDashButtonDown = false;
        }
    }
}
