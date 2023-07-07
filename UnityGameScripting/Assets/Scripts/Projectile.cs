using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Interactable
{

    public Transform shotBy = null;
    public float speed = 0;

    
    
    

    private void FixedUpdate()
    {
        transform.position.Set(transform.position.x + speed / 50, transform.position.y, transform.position.z);
        //if out of bounds, despawn
        //if collision, do OnHit()
    }

    public static Projectile Create(Transform shotByWho, Quaternion direction, float speed)
    {
        GameObject spawnedProjectile = new GameObject();
        Projectile projectileComponent = (Projectile)spawnedProjectile.AddComponent(typeof(Projectile));
        spawnedProjectile.transform.rotation = direction;
        projectileComponent.speed = speed;
        projectileComponent.shotBy = shotByWho;

        return (projectileComponent);
    }

    private bool OnHit(GameObject other)
    {
        //make this
    }
}
