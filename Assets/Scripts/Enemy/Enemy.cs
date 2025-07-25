using UnityEngine;
using UnityEngine.Events;

public class Enemy : Stats
{
    Rigidbody2D rb;

    public UnityEvent onDeath = new UnityEvent();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        base.Initialize();

        onDeath.AddListener(FindAnyObjectByType<WaveManager>().EnemyDeath);
    }
    void Update()
    {
        
    }

    public override void Die()
    {
        onDeath.Invoke();
        base.Die();
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
