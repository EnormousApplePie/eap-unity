using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHit
{
    public Interactable Source { get; private set; }
    public int Amount { get; private set; }

    public DamageHit(Interactable damageSource, int damageAmount)
    {
        Source = damageSource;
        Amount = damageAmount;
    }
}
