using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody rb;
    
    private Vector2 input;
    private Vector3 moveDirection;

    public float walkSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        moveDirection = (moveHorizontal * transform.right + moveVertical * transform.forward).normalized;
        Debug.Log(moveDirection);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

    }

    void FixedUpdate() 
    {
        Move();
    }

    void Move()
    {
        Vector3 yVelocity = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = moveDirection * walkSpeed * Time.deltaTime;
        rb.velocity += yVelocity;
    }
}
