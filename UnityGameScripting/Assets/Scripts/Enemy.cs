using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{

    public delegate void EnemyKilledDelegate(Enemy killedEnemy);
    public static EnemyKilledDelegate EnemyKilledEvent;

    public EnemyType enemyType = EnemyType.None;

    [Range(0, 250)]
    public int movementCooldownOnAttack = 40;
    protected int currentMovementCooldown = 0;

    public int attackStrength = 10;
    public int scoreValue = 1;

    protected bool isCollidingWithPlayer = false;



    private void FixedUpdate()
    {
        TurnToOther(PlayerController.currentPlayer.transform);
        if (currentMovementCooldown > 0)
        {
            --currentMovementCooldown;
        }
        else
        {
            transform.position.Set(transform.position.x + movementSpeed / 50, transform.position.y, transform.position.z);
            if (isCollidingWithPlayer)
            {
                PlayerController.currentPlayer.GetHit(new DamageHit(this, attackStrength));
                currentMovementCooldown = movementCooldownOnAttack;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform == PlayerController.currentPlayer.transform)
        {
            isCollidingWithPlayer = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform == PlayerController.currentPlayer.transform)
        {
            isCollidingWithPlayer = false;
        }
    }

    private void EnemyKilled()
    {
        EnemyKilledEvent.Invoke(this);
    }

    private void MeeleeAttack()
    {

    }

    private void RangedAttack()
    {

    }
}
