using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : Interactable
{
    public int health = 10;
    public int maxHealth = 10;
    [Range(0, 10f)]
    public float movementSpeed = 0.5f;

    public AudioSource soundSource;
    public AudioClip hitSound;
    public AudioClip dieSound;


    protected virtual void Start()
    {
        soundSource = transform.GetComponent<AudioSource>();
    }






    public virtual void GetHit(DamageHit hit)
    {
        if (soundSource != null && hitSound != null)
        {
            soundSource.PlayOneShot(hitSound);
        }
        health -= hit.Amount;
        if (health <= 0)
        {
            GetKilled();
        }
    }

    protected virtual void GetKilled()
    {
        //play a die sound that remains AFTER the object this belongs to is destroyed
        if(soundSource != null && hitSound != null)
        {
            soundSource.PlayOneShot(dieSound);
        }
        Destroy(gameObject);
    }
}
