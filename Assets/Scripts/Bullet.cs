using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public int damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Stats stats = collision.GetComponent<Stats>();
        stats.TakeDamage(damage);

        Destroy(gameObject);
    }
}
