using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public delegate void LevelCompletedEvent();
    public event LevelCompletedEvent LevelCompleted;
    public delegate void LevelManagerDestroyedEvent(LevelManager destroyedManager);
    public event LevelManagerDestroyedEvent LevelManagerDestroyed;

    private List<Enemy> enemies;
    private List<Spawner> spawners;

    [Range(1, 25)]
    public int amountOfWavesInLevel = 5;

    [Range(1, 10)]
    public int firstWaveEnemies = 1;

    [Range(0, 10)]
    public int additionalEnemiesPerWave = 0;

    [Range(0, 750)]
    public int coolDownAfterWaveEnd = 100;   //in physics frames (50 per second)
    private int currentCooldown = 0;


    private bool waveActive = false;
    private int currentWave = 0;

    void Start()
    {
        enemies = new List<Enemy>();
        Spawner.EnemySpawned += OnEnemySpawned;
        Spawner.NewSpawner += OnNewSpawner;
        Enemy.EnemyKilledEvent += OnEnemyKilled;
    }


    void Update()
    {
        if (!waveActive)
        {
            if (currentWave < amountOfWavesInLevel)
            {
                --currentCooldown;
                if (currentCooldown <= 0)
                {
                    StartWave();
                    
                }
            }
        }
    }

    ~LevelManager()
    {
        LevelManagerDestroyed.Invoke(this);
    }


    private void OnEnemySpawned(Enemy spawnedEnemy)
    {
        enemies.Add(spawnedEnemy);
    }

    private void OnEnemyKilled(Enemy killedEnemy)
    {
        enemies.Remove(killedEnemy);
        if(enemies.Count == 0)
        {
            WaveBeaten();
        }
    }

    private void OnNewSpawner(Spawner newSpawner)
    {
        spawners.Add(newSpawner);
    }

    private void WaveBeaten()
    {
        waveActive = false;
        currentCooldown = coolDownAfterWaveEnd;
        ++currentWave;
        if (currentWave >= amountOfWavesInLevel)
        {
            LevelCompleted.Invoke();
        }
    }


    private void StartWave()
    {
        int enemiesToSpawn = firstWaveEnemies + currentWave * additionalEnemiesPerWave;
        int remainingRandom = enemiesToSpawn % spawners.Count;
        if (enemiesToSpawn >= spawners.Count)
        {
            int enemiesPerSpawner = (enemiesToSpawn - remainingRandom) / spawners.Count;
            for (int i = 0; i < spawners.Count; ++i)
            {
                spawners[i].SpawnEnemies(enemiesPerSpawner);
            }
        }
        if(remainingRandom > 0)
        {
            for(int i = remainingRandom; i > 0; --i)
            {
                spawners[Random.Range(0, spawners.Count)].SpawnEnemies(1);
            }
        }
    }
}
