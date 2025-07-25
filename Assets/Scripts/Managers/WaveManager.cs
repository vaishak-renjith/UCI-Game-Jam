using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Enemy Spawning")]
    public float spawnInterval = 2f;
    private float spawnTimer = 0f;

    public int wave = 1;
    public int currentEnemies = 0;
    public int spawnedEnemies = 0;
    public int enemyLimit = 5;
    public int enemyWave = 5;

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
            if (currentEnemies < enemyLimit && spawnTimer >= spawnInterval && spawnedEnemies < enemyWave)
            {
                enemySpawner.SpawnEnemyAtEdge();
                currentEnemies++;
                spawnedEnemies++;
                spawnTimer = 0f;
            }
        }
        
    }
    public void EnemyDeath()
    {
        currentEnemies--;
        if (spawnedEnemies >= enemyWave && currentEnemies <= 0)
        {
            EndWave();
        }
    }

    public void StartWave()
    {
        waveActive = true;
    }

    private void EndWave()
    {
        waveActive = false;
        GameManager.Instance.SpawnShop();

        RecalculateEnemies();

        wave++;
    }

    private void RecalculateEnemies()
    {
        spawnedEnemies = 0;
        enemyWave = wave * 2 + 5;
    }
}
