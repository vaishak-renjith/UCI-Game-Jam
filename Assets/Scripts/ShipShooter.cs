using UnityEngine;
using System.Collections.Generic;

public class ShipShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 30f;
    public Transform firePoint;
    public float screenBuffer = 1f;
    public float bulletOffset = 0.5f; // Distance behind the ship
    public float fireRate = 0.2f; // Seconds between shots
    [HideInInspector] public bool canShoot = false; // Only active ship can shoot

    private List<GameObject> bullets = new List<GameObject>();
    private float fireTimer = 0f;

    void Start()
    {
        // Initialize firePoint to the center if not set
        if (firePoint == null)
        {
            GameObject fp = new GameObject("FirePoint");
            fp.transform.parent = transform;
            fp.transform.localPosition = Vector3.zero;
            firePoint = fp.transform;
        }
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

        // Destroy bullets that go off screen
        for (int i = bullets.Count - 1; i >= 0; i--)
        {
            if (IsOffScreen((Vector2)bullets[i].transform.position))
            {
                Destroy(bullets[i]);
                bullets.RemoveAt(i);
            }
        }
    }

    void Shoot()
    {
        // Place bullet behind the ship (along negative up direction)
        Vector2 spawnPos = (Vector2)firePoint.position - (Vector2)transform.up * bulletOffset;
        GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.Euler(0, 0, transform.eulerAngles.z));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = (Vector2)transform.up * bulletSpeed;
        }
        bullets.Add(bullet);
    }

    bool IsOffScreen(Vector2 position)
    {
        Vector2 screenPos = Camera.main.WorldToViewportPoint(position);
        return screenPos.x < -screenBuffer || screenPos.x > 1 + screenBuffer ||
               screenPos.y < -screenBuffer || screenPos.y > 1 + screenBuffer;
    }
}