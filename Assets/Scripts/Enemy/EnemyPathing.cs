using Unity.VisualScripting;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    Rigidbody2D shipRB;
    Transform player;
    Vector2 directionToPlayer;

    Stats stats;

    void Awake()
    {
        shipRB = GetComponent<Rigidbody2D>();
        stats = GetComponent<Stats>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyEngineForce();
        killOrthogonalVelocity();
        UpdateTargetDirection();
        RotateTowardsTarget();

    }

    void ApplyEngineForce()
    {
        if (shipRB.linearVelocity.sqrMagnitude < stats.maxSpeed)
        {
            Vector2 engineForceVector = transform.up * stats.acceleration;

            shipRB.AddForce(engineForceVector, ForceMode2D.Force);
        }
    }

    void killOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(shipRB.linearVelocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(shipRB.linearVelocity, transform.right);

        shipRB.linearVelocity = forwardVelocity + rightVelocity * stats.driftFactor;
    }

    void UpdateTargetDirection()
    {
        Vector2 enemyToPlayer = player.position - transform.position;
        directionToPlayer = enemyToPlayer.normalized;
    }

    void RotateTowardsTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, directionToPlayer);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, stats.turnSpeed * Time.deltaTime);

        shipRB.SetRotation(rotation);
    }
}
