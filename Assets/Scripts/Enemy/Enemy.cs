using UnityEngine;

public class Enemy : Stats
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Collision damage based off speed
        if (collision.gameObject.GetComponent<PlayerStats>())
        {
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(Mathf.RoundToInt(rb.linearVelocity.magnitude));
        }
    }
}
