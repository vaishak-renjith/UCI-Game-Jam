using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float moveForce = 10f;
    public float turnSpeed = 200f;
    public float driftDamp = 2f;
    private Rigidbody2D rb;
    [HideInInspector] public bool canMove = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = driftDamp; 
    }

    void Update()
    {
        if (!canMove)
        {
            rb.angularVelocity = 0f;
            return;
        }

        float moveInput = Input.GetAxisRaw("Vertical");
        float turnInput = -Input.GetAxisRaw("Horizontal");

        if (moveInput != 0)
            rb.AddForce(transform.up * moveInput * moveForce);

        if (turnInput != 0)
        {
            float rotation = rb.rotation + turnInput * turnSpeed * Time.deltaTime;
            rb.MoveRotation(rotation);
        }
        else
        {
            rb.angularVelocity = 0f; // No rotation if no input
        }
    }
}