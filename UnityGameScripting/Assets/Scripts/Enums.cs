using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SpawnerType
{
    None,
    Enemy
}

public enum Team
{
    None,
    Friendly,
    Enemy,
    Neutral
}

public enum EnemyType
{
    None,
    Ranged,
    Meelee
}

public enum OutOfBoundsAction
{
    None, 
    Despawn,
    Center
}

public enum UIElement
{
    None,
    PlayButton,
    QuitLevelButton,
    QuitGameButton,
    ReturnButton,
    ScoreCounter,
    WaveCounter,
    HealthCounter,
    GoEndlessButton,
    NextLevelButton,
    RestartLevelButton
}

