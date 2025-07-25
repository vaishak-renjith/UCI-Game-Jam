using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyConeShooter2D : MonoBehaviour
{
    [Header("Vision")]
    public float viewDistance = 8f;
    [Range(0, 360)] public float viewAngle = 60f;
    public int raySegments = 15;
    public LayerMask targetMask;     // Player layer
    public LayerMask obstacleMask;   // e.g. Walls, asteroids

    [Header("Shooting")]
    private Rigidbody2D shipRB;
    public Transform[] firePoints;
    public GameObject bulletPrefab;
    public float bulletSpeed = 30f; // Might be good to set it faster than ships max speed
    [HideInInspector] public bool canShoot = false; // Only active ship can shoot
    private float fireTimer = 0f;
    
    private Stats stats;

    Transform currentTarget;


    private void Awake()
    {
        stats = GetComponent<Stats>();
        shipRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ScanForTarget();

        fireTimer += Time.deltaTime;

        if (currentTarget != null && fireTimer >= stats.fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }

    }

    void ScanForTarget()
    {
        currentTarget = null;

        // Grab all candidates in range first (cheap circle check)
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, viewDistance, targetMask);
        foreach (var hit in hits)
        {
            Vector2 dirToTarget = (hit.transform.position - transform.position);
            float dist = dirToTarget.magnitude;

            // Angle check
            if (Vector2.Angle(transform.up, dirToTarget) <= viewAngle * 0.5f)
            {
                // Line of sight check
                if (!Physics2D.Raycast(transform.position, dirToTarget.normalized, dist, obstacleMask))
                {
                    currentTarget = hit.transform;
                    return;
                }
            }
        }
    }

    void Shoot()
    {
        // Place bullet behind the ship (along negative up direction)
        // multiple firepoints can be added
        foreach (Transform fp in firePoints)
        {
            Vector2 spawnPos = (Vector2)fp.position - (Vector2)transform.up;
            GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.Euler(0, 0, transform.eulerAngles.z));

            bullet.GetComponent<Bullet>().damage = stats.damage;
            bullet.GetComponent<Bullet>().owner = gameObject.tag;



            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Adds ship's velocity to keep bullet infront 
                rb.linearVelocity = (Vector2)transform.up * bulletSpeed + shipRB.linearVelocity;
            }
        }
    }

    // ===== Debug Cone Drawing =====
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Vector2 leftDir = DirFromAngle(-viewAngle / 2f);
        Vector2 rightDir = DirFromAngle(viewAngle / 2f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)leftDir * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)rightDir * viewDistance);

        // Rays to approximate the cone
        Gizmos.color = new Color(0, 1, 1, 0.25f);
        for (int i = 0; i < raySegments; i++)
        {
            float t = (raySegments == 1) ? 0.5f : i / (float)(raySegments - 1);
            float angle = Mathf.Lerp(-viewAngle / 2f, viewAngle / 2f, t);
            Vector2 dir = DirFromAngle(angle);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewDistance, obstacleMask);
            Vector3 endPoint = hit ? (Vector3)hit.point : transform.position + (Vector3)dir * viewDistance;
            Gizmos.DrawLine(transform.position, endPoint);
        }
    }

    Vector2 DirFromAngle(float angle)
    {
        float rad = (transform.eulerAngles.z + angle + 90f) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }
}
