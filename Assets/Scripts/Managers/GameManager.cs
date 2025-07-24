using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int level = 1;
    public int enemyAmount => 5 + (level * 2);
    public int enemyLeft;


    [SerializeField] EnemySpawner enemySpawner;

    public static GameManager Instance;
    void Awake()
    {
        // Makes sure only one isntance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            enemySpawner.SpawnEnemyAtEdge();
            spawnTimer = 0f;
        }
    }
}
