using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] StatsSO stats;

    [Header("UnitName")]
    public string Name;

    [Header("Defense")]
    public int currentHealth = 100;

    [Header("Offense")]
    public int damage = 10;
    public int rammingDamage = 5;
    public float fireRate = 0.2f;

    [Header("Movement")]
    public float turnSpeed = 2f;
    public float maxSpeed = 100;
    public float acceleration = 20;
    public float driftFactor = .95f;

    [Header("Reward")]
    public int currency = 0;

    [SerializeField] private Healthbar healthbar;

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
        healthbar.UpdateHealth(stats.maxHealth, currentHealth);

        damage = stats.damage;
        rammingDamage = stats.rammingDamage;
        fireRate = stats.fireRate;

        currency = stats.currency;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(damage);
        currentHealth -= damage;
        healthbar.UpdateHealth(stats.maxHealth, currentHealth);
            
        if (currentHealth <= 0) Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
