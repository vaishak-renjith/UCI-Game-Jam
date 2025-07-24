using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Enemy Spawning")]
    public float spawnInterval = 2f;
    private float spawnTimer = 0f;

    private int spawnedEnemies = 0;

    public bool waveActive = false;

    [SerializeField] EnemySpawner enemySpawner;

    void Awake()
    {
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }

    void Update()
    {
        if (waveActive)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                enemySpawner.SpawnEnemyAtEdge();
                spawnTimer = 0f;
            }
        }
        
    }
}
