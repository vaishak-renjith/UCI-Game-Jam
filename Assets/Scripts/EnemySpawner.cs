using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public CinemachineCamera virtualCamera;
    public float spawnInterval = 2f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemyAtEdge();
            timer = 0f;
        }
    }

    void SpawnEnemyAtEdge()
    {
        if (enemyPrefabs.Count == 0 || virtualCamera == null) return;

        // Get camera bounds in world space
        Camera cam = Camera.main;
        Vector3 camPos = virtualCamera.Follow != null ? virtualCamera.Follow.position : cam.transform.position;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Pick a random edge: 0=top, 1=bottom, 2=left, 3=right
        int edge = Random.Range(0, 4);
        Vector2 spawnPos = camPos;

        switch (edge)
        {
            case 0: // Top
                spawnPos += new Vector2(Random.Range(-camWidth / 2, camWidth / 2), camHeight / 2);
                break;
            case 1: // Bottom
                spawnPos += new Vector2(Random.Range(-camWidth / 2, camWidth / 2), -camHeight / 2);
                break;
            case 2: // Left
                spawnPos += new Vector2(-camWidth / 2, Random.Range(-camHeight / 2, camHeight / 2));
                break;
            case 3: // Right
                spawnPos += new Vector2(camWidth / 2, Random.Range(-camHeight / 2, camHeight / 2));
                break;
        }

        // Spawn a random enemy prefab
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}