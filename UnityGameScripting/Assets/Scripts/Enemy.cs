using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{
    protected Rigidbody rb;
    public delegate void EnemyKilledDelegate(Enemy killedEnemy);
    public static EnemyKilledDelegate EnemyKilledEvent;

    public EnemyType enemyType = EnemyType.None;

    [Range(0, 250)]
    public int movementCooldownOnAttack = 40;
    protected int currentMovementCooldown = 0;

    public int attackStrength = 10;
    public int scoreValue = 1;

    protected bool isCollidingWithPlayer = false;

    protected override void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        if(rb == null)
        {
            rb = new Rigidbody();
            rb.useGravity = false;
        }
        base.Start();
    }

    private void FixedUpdate()
    {
        if (transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        if (PlayerController.currentPlayer == null)
        {
            return;
        }
        TurnToOther(PlayerController.currentPlayer.transform);
        if (currentMovementCooldown > 0)
        {
            --currentMovementCooldown;
        }
        else
        {
            Vector3 desiredVelocity = PlayerController.currentPlayer.transform.position - transform.position;
            desiredVelocity.y = 0;
            desiredVelocity.Normalize();
            desiredVelocity *= movementSpeed;
            rb.velocity = desiredVelocity;
            //transform.position.Set(transform.position.x + movementSpeed / 50, transform.position.y, transform.position.z);
            if (isCollidingWithPlayer)
            {
                PlayerController.currentPlayer.GetHit(new DamageHit(this, attackStrength));
                currentMovementCooldown = movementCooldownOnAttack;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.transform == null) return;
        if(PlayerController.currentPlayer != null && collision.transform == PlayerController.currentPlayer.transform)
        {
            isCollidingWithPlayer = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //if (collision.transform == null) return;
        if (PlayerController.currentPlayer != null && collision.transform == PlayerController.currentPlayer.transform)
        {
            isCollidingWithPlayer = false;
        }
    }

    protected override void GetKilled()
    {
        EnemyKilledEvent.Invoke(this);
        base.GetKilled();
    }

    private void MeeleeAttack()
    {

    }

    private void RangedAttack()
    {

    }
}
