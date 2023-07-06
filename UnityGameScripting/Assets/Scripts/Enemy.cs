using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable
{

    public delegate void EnemyKilledDelegate(Enemy killedEnemy);
    public static EnemyKilledDelegate EnemyKilledEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemyKilled()
    {
        EnemyKilledEvent.Invoke(this);
    }
}