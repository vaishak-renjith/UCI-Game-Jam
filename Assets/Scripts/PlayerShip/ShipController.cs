using UnityEngine;


public class ShipController : MonoBehaviour
{
    [Header("Ship Settings")]
    [SerializeField] StatsSO playerStats;

    //local vars
    float forwardInput;
    float steeringInput;
    float rotationAngle;

    //Component
    Rigidbody2D shipRB;
    GameObject gunRB;

    [HideInInspector] public bool canMove;

    private void Awake()
    {
        shipRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!canMove) { forwardInput = steeringInput = 0; return; }

        ProcessInput();
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        ApplyEngineForce();
        killOrthogonalVelocity();
        ApplySteering();
    }

    void ApplyEngineForce()
    {
        if (shipRB.linearVelocity.sqrMagnitude < playerStats.maxSpeed * playerStats.maxSpeed)
        {
            Vector2 engineForceVector = transform.up * playerStats.acceleration * forwardInput;

            shipRB.AddForce(engineForceVector, ForceMode2D.Force);
        }
    }

    void ApplySteering()
    {
        rotationAngle -= steeringInput * playerStats.turnSpeed;

        shipRB.MoveRotation(rotationAngle);
    }
    void killOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(shipRB.linearVelocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(shipRB.linearVelocity, transform.right);

        shipRB.linearVelocity = forwardVelocity + rightVelocity * playerStats.driftFactor;
    }

    private void ProcessInput()
    {
        Vector2 inputVector = Vector2.zero;
        forwardInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");
    }
}