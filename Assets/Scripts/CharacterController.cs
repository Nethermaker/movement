using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider col;
    
    private Vector2 input;
    private Vector3 moveDirection;

    public float walkSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;
        //TODO: Adding Vector3.up is a band-aid solution
        if (CanMove(transform.right * Input.GetAxisRaw("Horizontal") + Vector3.up))
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal");
        }
        if (CanMove(transform.forward * Input.GetAxisRaw("Vertical") + Vector3.up))
        {
            moveVertical = Input.GetAxisRaw("Vertical");    
        }
        moveDirection = (moveHorizontal * transform.right + moveVertical * transform.forward).normalized;
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

    bool CanMove(Vector3 direction)
    {
        float distanceToPoints = col.height / 2 - col.radius;

        Vector3 point1 = transform.position + col.center + Vector3.up * distanceToPoints;
        Vector3 point2 = transform.position + col.center - Vector3.up * distanceToPoints;

        float radius = col.radius * 0.95f; //Don't want ground to count as a wall
        float castDistance = 1f;

        RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, radius, direction, castDistance);

        foreach (RaycastHit objectHit in hits) 
        {
            if (objectHit.transform.tag == "Wall")
            {
                return false;
            }
        }

        return true;
    }
}
