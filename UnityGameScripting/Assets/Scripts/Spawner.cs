using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Spawner : MonoBehaviour
{
    public delegate void EnemySpawnedEvent(Enemy spawnedEnemy);
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
        NewSpawner.Invoke(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies(int count)
    {
        
        for(int i = 0; i < count; ++i)
        {
            float spawnX = transform.position.x + Random.Range(-SpawnRangeX, SpawnRangeX);
            float spawnZ = transform.position.z + Random.Range(-SpawnRangeZ, SpawnRangeZ);
            SpawnEnemy(spawnX, spawnZ);
        }
    }
    
    public void SpawnEnemy(float x, float z)
    {
        GameObject spawnedEnemy = Instantiate(spawnerType, new Vector3(x, transform.position.y, z), Quaternion.identity);
        Enemy enemyComponent = (Enemy)spawnedEnemy.GetComponent(typeof(Enemy));
        if(enemyComponent == null)
        {
            Debug.LogError("Newly spawned enemy with name \"" + spawnedEnemy.name + "\" does not have a component of type Enemy, destroying...");
            Destroy(spawnedEnemy);
            return;
        }
        EnemySpawned.Invoke(enemyComponent);
    }
}
