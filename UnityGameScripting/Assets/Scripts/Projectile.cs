using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Interactable
{

    public Transform shotBy = null;
    private Rigidbody rb;
    public bool hitsOwnTeam = false;
    public float speed = 0;
    private int strength = 1;   //damage upon hit




    private void Start()
    {
        outOfBoundsAction = OutOfBoundsAction.Despawn;
        rb = GetComponent<Rigidbody>();
        if(rb == null)
        {
            Debug.LogError("Projectile with name \"" + gameObject.name + "\"spawned without a RigidBody, adding one...");
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }
    }

    private void FixedUpdate()
    {
        //transform.position.Set(transform.position.x + speed / 50, transform.position.y, transform.position.z);
        //if out of bounds, despawn
        //if collision, do OnHit()
        //Debug.Log("Velocity: " + rb.velocity);
        //Debug.Log(transform.position);
    }

    public static Projectile Create(GameObject whatToShoot, Transform shotByWho, int shotAtWhatLayer, Vector3 direction, float speed, float shootHeight, int strength)
    {
        direction.y = 0;
        GameObject spawnedProjectile = Instantiate(whatToShoot, shotByWho.position + shotByWho.forward, Quaternion.identity);
        Projectile projectileComponent = spawnedProjectile.gameObject.GetComponent<Projectile>();
        if (projectileComponent == null)
        {
            Debug.LogError("Projectile with name \"" + spawnedProjectile.name + "\" spawned without a Projectile component, creating one...");
            projectileComponent = (Projectile)spawnedProjectile.AddComponent(typeof(Projectile));
        }
        
        projectileComponent.rb = spawnedProjectile.GetComponent<Rigidbody>();
        if (projectileComponent.rb == null)
        {
            Debug.LogError("Projectile with name \"" + spawnedProjectile.name + "\"spawned without a RigidBody, adding one...");
            projectileComponent.rb = spawnedProjectile.AddComponent<Rigidbody>();
            projectileComponent.rb.useGravity = false;
        }
        
        
        
        
        spawnedProjectile.transform.position = new Vector3(spawnedProjectile.transform.position.x, spawnedProjectile.transform.position.y + shootHeight, spawnedProjectile.transform.position.z);
        //spawnedProjectile.transform.rotation = direction;

        
        spawnedProjectile.layer = shotAtWhatLayer;
        projectileComponent.shotBy = shotByWho;
        projectileComponent.strength = strength;
        Vector3 desiredVelocity = direction.normalized * speed;
        //projectileComponent.rb.velocity = desiredVelocity;


        spawnedProjectile.transform.rotation = shotByWho.rotation;
        Vector3 desiredVelocity2 = new Vector3(0, 0, speed);
        projectileComponent.rb.velocity = direction.normalized * speed;




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
