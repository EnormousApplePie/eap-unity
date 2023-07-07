using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : Interactable
{
    public int health = 10;
    public int maxHealth = 10;
    [Range(0, 10f)]
    public float movementSpeed = 0.5f;










    public void GetHit(DamageHit hit)
    {

    }
}
