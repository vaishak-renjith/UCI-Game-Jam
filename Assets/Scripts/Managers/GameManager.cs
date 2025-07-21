using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int level = 1;

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
    }

    void Update()
    {
        
    }
}
