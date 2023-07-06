using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Spawner : MonoBehaviour
{
    public delegate void EnemySpawnedEvent(Interactable spawnedEnemy);
    public static EnemySpawnedEvent EnemySpawned;
    public delegate void NewSpawnerEvent(Spawner newSpawner);
    public static NewSpawnerEvent NewSpawner;

    public GameObject spawnerType;
    public Team spawnerTeam = Team.None;

    [Range(0, 10)]
    public float SpawnRangeX = 1;
    [Range(0, 10)]
    public float SpawnRangeZ = 1;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemies(int count)
    {
        
        for(int i = 0; i < count; ++i)
        {
            float spawnX = transform.position.x + Random.Range(-SpawnRangeX, SpawnRangeX);
            float spawnZ = transform.position.z + Random.Range(-SpawnRangeZ, SpawnRangeZ);
            SpawnEnemy(spawnX, spawnZ);
        }
    }
    
    void SpawnEnemy(float x, float z)
    {
        Instantiate(spawnerType, new Vector3(x, transform.position.y, z), Quaternion.identity);
    }
}
