using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Interactable
{

    public Transform shotBy = null;
    public bool hitsOwnTeam = false;
    public float speed = 0;
    private int strength = 1;   //damage upon hit




    private void Start()
    {
        outOfBoundsAction = OutOfBoundsAction.Despawn;
    }

    private void FixedUpdate()
    {
        transform.position.Set(transform.position.x + speed / 50, transform.position.y, transform.position.z);
        //if out of bounds, despawn
        //if collision, do OnHit()
    }

    public static Projectile Create(Transform shotByWho, Quaternion direction, float speed, int strength)
    {
        GameObject spawnedProjectile = new GameObject();
        Projectile projectileComponent = (Projectile)spawnedProjectile.AddComponent(typeof(Projectile));
        spawnedProjectile.transform.rotation = direction;
        projectileComponent.speed = speed;
        projectileComponent.shotBy = shotByWho;
        projectileComponent.strength = strength;

        return (projectileComponent);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform == shotBy) return;
        Interactable interactableComponent = (Interactable)collision.gameObject.GetComponent(typeof(Interactable));
        if (interactableComponent == null) return;  //don't do anything when colliding with a non-interactable object, maybe later change this to destroy the projectile
        if (hitsOwnTeam && interactableComponent.team == team) return; //no friendly fire

        if (interactableComponent is Creature creatureCollision && creatureCollision != null)
        {
            creatureCollision.GetHit(new DamageHit(this, strength));
        }

        Destroy(this);
    }
}
