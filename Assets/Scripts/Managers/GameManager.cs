using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] WaveManager waveManager;
    [SerializeField] Shop shop;

    void Awake()
    {
        // Makes sure only one isntance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Update()
    {
        
    }

    public void StartWave()
    {
        waveManager.StartWave();
    }

    public void SpawnShop()
    {
        Vector2 playerPos = FindAnyObjectByType<PlayerStats>().transform.position;
        shop.DisplayShopItems(playerPos, 30.0f);
    }
}
