using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public delegate void LevelCompletedEvent();
    public event LevelCompletedEvent LevelCompleted;
    public delegate void LevelManagerDestroyedEvent(LevelManager destroyedManager);
    public event LevelManagerDestroyedEvent LevelManagerDestroyed;

    private List<Interactable> enemies;
    private List<Spawner> spawners;

    [Range(1, 10)]
    public int firstWaveEnemies = 1;

    [Range(0, 10)]
    public int additionalEnemiesPerWave = 0;





    void Start()
    {
        enemies = new List<Interactable>();
        Spawner.EnemySpawned += OnEnemySpawned;
        Spawner.NewSpawner += OnNewSpawner;

    }


    void Update()
    {
        
    }

    ~LevelManager()
    {
        LevelManagerDestroyed.Invoke(this);
    }


    private void OnEnemySpawned(Interactable spawnedEnemy)
    {
        enemies.Add(spawnedEnemy);
    }

    private void OnEnemyKilled(Interactable killedEnemy)
    {
        enemies.Remove(killedEnemy);
    }

    private void OnNewSpawner(Spawner newSpawner)
    {
        spawners.Add(newSpawner);
    }
}
