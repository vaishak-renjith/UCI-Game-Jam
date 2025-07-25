using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Ship Settings")]
    [SerializeField] StatsSO playerStats;
    private Stats stats;

    [Header("Sprites")]
    [SerializeField] Sprite[] forms;  // [0] normal, [1] astral

    float forwardInput, steeringInput;
    float rotationAngle;
    Rigidbody2D shipRB;

    Collider2D shipCollider;
    SpriteRenderer spriteRenderer;

    [HideInInspector] public bool isGhosted = false;
    private float abilityTimer = 0f;
    private bool onCooldown = false;
    private float cooldownTimer = 0f;
    private float cooldownDuration = 0f;
    public bool canMove = true;

    private void Awake()
    {
        shipRB = GetComponent<Rigidbody2D>();
        shipCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stats = GetComponent<Stats>();

        isGhosted = false;
        abilityTimer = 0f;
        onCooldown = false;
        cooldownTimer = cooldownDuration = 0f;
        shipCollider.enabled = true;
        spriteRenderer.sprite = forms[0];
        rotationAngle = shipRB.rotation;
    }

    private void Update()
    {
        if (isGhosted)
        {
            abilityTimer += Time.deltaTime;
            if (abilityTimer >= stats.abilityDuration)
                DeactivateAstralForm();
        }
        else if (onCooldown)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldownDuration)
                onCooldown = false;
        }

        if (!canMove)
            return;

        ProcessInput();
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        if (shipRB.linearVelocity.sqrMagnitude < stats.maxSpeed * stats.maxSpeed)
            shipRB.AddForce(transform.up * stats.acceleration * forwardInput, ForceMode2D.Force);

        Vector2 forwardVel = transform.up * Vector2.Dot(shipRB.linearVelocity, transform.up);
        Vector2 rightVel = transform.right * Vector2.Dot(shipRB.linearVelocity, transform.right);
        shipRB.linearVelocity = forwardVel + rightVel * stats.driftFactor;

        rotationAngle -= steeringInput * stats.turnSpeed * Time.fixedDeltaTime;
        shipRB.MoveRotation(rotationAngle);
    }

    private void ProcessInput()
    {
        forwardInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isGhosted && !onCooldown)
                ActivateAstralForm();
            else if (isGhosted)
                DeactivateAstralForm();
        }
    }

    private void ActivateAstralForm()
    {
        isGhosted = true;
        shipCollider.enabled = false;
        spriteRenderer.sprite = forms[1];
    }

    private void DeactivateAstralForm()
    {
        bool fullUsed = abilityTimer >= stats.abilityDuration;

        isGhosted = false;
        shipCollider.enabled = true;
        spriteRenderer.sprite = forms[0];

        onCooldown = true;
        cooldownDuration = fullUsed ? 3f : 1.5f;
        cooldownTimer = 0f;

        abilityTimer = 0f;
    }
}
