using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public float spawnRate;
    public GameObject enemyPrefab;
    
    private int numberOfObjects = 3;
    private float yMin = -1.5f;
    private float yMax = 5.0f;
    private float timer = 0.0f;
    private float spawnTimeRandom = 5.0f;
    private float spawnTime;
    private float spawnTimer;


    void Start()
    {
        Spawn();
        if (timer > 40)
        {
            Spawn();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log("Time Elapsed: " + timer.ToString());

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0.0f)
        {
            Spawn();
            ResetSpawnTimer();
        }
    }
    
    void Spawn()
    {
        float yPos = Random.Range(yMin, yMax);
        Vector3 pos = new Vector3(15, yPos, 0);
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }

    void ResetSpawnTimer()
    {
        spawnTimer = (float) (spawnTime + Random.Range(0, spawnTimeRandom * 100) / 100.0);
        Debug.Log(spawnTimer.ToString());
    }
}
