using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public delegate void LevelCompletedEvent();
    public event LevelCompletedEvent LevelCompleted;
    public delegate void LevelManagerDestroyedEvent(LevelManager destroyedManager);
    public event LevelManagerDestroyedEvent LevelManagerDestroyed;

    private List<Enemy> enemies = new List<Enemy>();
    private List<Spawner> spawners = new List<Spawner>();

    private TextMeshProUGUI scoreCounter;
    private TextMeshProUGUI waveCounter;
    private TextMeshProUGUI playerHealthCounter;

    [Range(1, 25)]
    public int amountOfWavesInLevel = 5;

    [Range(1, 10)]
    public int firstWaveEnemies = 1;

    [Range(0, 10)]
    public int additionalEnemiesPerWave = 0;

    [Range(0, 750)]
    public int coolDownAfterWaveEnd = 100;   //in physics frames (50 per second)
    public int currentCooldown = 0;

    private int currentScore = 0;


    private bool waveActive = false;
    private int currentWave = 0;
    private bool isEndless = false;

    void Awake()
    {
        Debug.Log("An create yes");
        if(CentralManager.ExistingManager == null)
        {
            GameObject managerObject = new GameObject();
            managerObject.AddComponent<CentralManager>();
        }
        enemies = new List<Enemy>();
        Spawner.EnemySpawned += OnEnemySpawned;
        Spawner.NewSpawner += OnNewSpawner;
        Enemy.EnemyKilledEvent += OnEnemyKilled;
        //waveCounter.text = "0 / " + amountOfWavesInLevel;
        currentCooldown = coolDownAfterWaveEnd;
        Debug.Log(currentCooldown + ", " + coolDownAfterWaveEnd);
        CentralManager.OnLevelCreated(this);
    }


    void FixedUpdate()
    {
        if (!waveActive)
        {
            if (currentWave < amountOfWavesInLevel)
            {
                --currentCooldown;
                if (currentCooldown <= 0)
                {
                    StartWave();
                    Debug.Log("Starting wave...");
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
        currentScore += killedEnemy.scoreValue;
        if (scoreCounter != null)
        {
            scoreCounter.text = "score: " + currentScore;
        }
        if(enemies.Count == 0)
        {
            WaveBeaten();
        }
    }

    private void OnNewSpawner(Spawner newSpawner)
    {
        spawners.Add(newSpawner);
        Debug.Log("Added spawner to list of usable spawners");
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
        Debug.Log("spawning new wave with " + (firstWaveEnemies + currentWave * additionalEnemiesPerWave) + " enemies.");
        if(spawners.Count == 0)
        {
            Debug.LogError("No spawners present while attempting to start wave of enemies");
            currentCooldown = coolDownAfterWaveEnd;
            return;
        }
        waveActive = true;
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
        if (waveCounter != null)
        {
            if (!isEndless)
            {
                waveCounter.text = "wave " + currentWave + 1 + " / " + amountOfWavesInLevel;
            }
            else
            {
                waveCounter.text = "wave " + currentWave + 1 + "";
            }
        }
    }

    public void RegisterUIButton(Button addingButton, UIElement typeOfButton)   //might remove this later as buttons should generally handle themselves
    {

    }

    public void RegisterUIText(TextMeshProUGUI addingText, UIElement typeOfText)
    {
        switch (typeOfText)
        {
            case UIElement.ScoreCounter:
                scoreCounter = addingText;
                break;
            case UIElement.WaveCounter:
                waveCounter = addingText;
                break;
            case UIElement.HealthCounter:
                playerHealthCounter = addingText;
                PlayerController.healthCounter = addingText;
                break;



            default:
                Debug.LogError("Invalid text element \"" + typeOfText + "\" attempted to load into a level");
                break;
        }
    }



    
}
