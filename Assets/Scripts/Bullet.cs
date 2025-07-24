using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public int damage;
    public string owner;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
        gameObject.tag = owner;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(owner) || collision.GetComponent<Bullet>()) return;

        Debug.Log(damage);
        Stats stats = collision.GetComponent<Stats>();
        stats.TakeDamage(damage);

        Destroy(gameObject);
    }
}
