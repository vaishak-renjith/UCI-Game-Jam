using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] private float speed;
    private GameObject[] playerObjects;
    private GameObject target;

    void Start()
    {
        playerObjects = GameObject.FindGameObjectsWithTag("Player");
        target = GetClosestPlayer();
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public GameObject GetClosestPlayer()
    {
        GameObject closestPlayer = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject player in playerObjects)
        {
            float distance = Vector2.Distance(player.transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player;
            }
        }

        return closestPlayer;
    }
}
