using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Stats))]
public class ShipShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 30f; // Might be good to set it faster than ships max speed
    public Transform[] firePoints; // Multiple Firepoints can now be added
    public float screenBuffer = 1f;
    public float bulletOffset = 0.5f; // Distance behind the ship
    public float fireRate = 0.2f; // Seconds between shots
    [HideInInspector] public bool canShoot = false; // Only active ship can shoot

    private Rigidbody2D shipRB;

    private List<GameObject> bullets = new List<GameObject>();
    private float fireTimer = 0f;

    public Stats stats;

    void Awake()
    {
        // Initialize firePoint to the center if not set
        //if (firePoint == null)
        //{
        //    GameObject fp = new GameObject("FirePoint");
        //    fp.transform.parent = transform;
        //    fp.transform.localPosition = Vector3.zero;
        //    firePoint = fp.transform;
        //}

        shipRB = GetComponentInParent<Rigidbody2D>();
        stats = GetComponent<Stats>();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (canShoot && Input.GetKey(KeyCode.Space))
        {
            if (fireTimer >= fireRate)
            {
                Shoot();
                fireTimer = 0f;
            }
        }
        else if (!Input.GetKey(KeyCode.Space))
        {
            fireTimer = fireRate; // Ready to shoot immediately when pressed again
        }
    }

    void Shoot()
    {
        // Place bullet behind the ship (along negative up direction)
        // multiple firepoints can be added
        foreach (Transform fp in firePoints)
        {
            Vector2 spawnPos = (Vector2)fp.position - (Vector2)transform.up * bulletOffset;
            GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.Euler(0, 0, transform.eulerAngles.z));

            bullet.GetComponent<Bullet>().damage = stats.damage;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Adds ship's velocity to keep bullet infront 
                rb.linearVelocity = (Vector2)transform.up * bulletSpeed + shipRB.linearVelocity;
            }
            bullets.Add(bullet);
        }
    }

    bool IsOffScreen(Vector2 position)
    {
        Vector2 screenPos = Camera.main.WorldToViewportPoint(position);
        return screenPos.x < -screenBuffer || screenPos.x > 1 + screenBuffer ||
               screenPos.y < -screenBuffer || screenPos.y > 1 + screenBuffer;
    }
}