using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] StatsSO stats;
    public string Name;
    public float turnSpeed = 2f;
    public float maxSpeed = 100;
    public float acceleration = 20;
    public float driftFactor = .95f;

    public int currentHealth = 100;

    public int damage = 10;
    public float fireRate = 0.2f;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        
    }

    public void Initialize()
    {
        turnSpeed = stats.turnSpeed;
        maxSpeed = stats.maxSpeed;
        acceleration = stats.acceleration;
        driftFactor = stats.driftFactor;

        currentHealth = stats.maxHealth;

        damage = stats.damage;
        fireRate = stats.fireRate;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(damage);
        currentHealth -= damage;

        if (currentHealth < 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
