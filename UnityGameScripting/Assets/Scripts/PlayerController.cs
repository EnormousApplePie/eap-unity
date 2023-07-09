using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Creature
{
    public static PlayerController currentPlayer { get; private set; }

    public float projectileSpeed = 20;
    public int projectileStrength = 10;
    public int shootCooldown = 25;
    public int currentShootCooldown = 0;
    [Range(0, 50)]
    public float acceleration = 15;

    public AudioClip shootSound;

    public GameObject attackProjectile;
    [Range(0.1f, 2)]
    public float shootHeight = 0.4f;
    

    private Camera m_Camera;
    Rigidbody rb;
    [SerializeField] float rotationSpeed = 1f;
    //[SerializeField] float movementSpeed = 20f;
    GameObject floorPlane;
    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = this;
        rb = GetComponent<Rigidbody>();
        m_Camera = Camera.main;
        floorPlane = GameObject.Find("Ground");
        currentShootCooldown = shootCooldown;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        --currentShootCooldown;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //ignore the ignore raycast layer
       
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            TurnToPosition(hit.point);
            if (currentShootCooldown <= 0 && Input.GetMouseButton(0))
            {
                Shoot(hit.point);
            }
        }

        PlayerMovement();

        
    }

    private void Shoot(Vector3 shootingTowards)
    {
        currentShootCooldown = shootCooldown;   
        Projectile shotProjectile = Projectile.Create(attackProjectile, transform, LayerMask.NameToLayer("Player"), shootingTowards, projectileSpeed, shootHeight, projectileStrength);
        //shotProjectile.gameObject.layer = LayerMask.NameToLayer("Player");
        soundSource.PlayOneShot(shootSound);
    }


    private void PlayerMovement()
    {
        Vector3 movementGoal = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            movementGoal.z = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movementGoal.z = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementGoal.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movementGoal.x = 1;
        }
        if(movementGoal.magnitude != 0)
        {
            movementGoal.Normalize();
            movementGoal *= acceleration;
            rb.AddForce(movementGoal);
            if(rb.velocity.magnitude > movementSpeed)
            {
                rb.velocity *= movementSpeed / rb.velocity.magnitude;
            }
        }
        else
        {
            rb.velocity *= 0.85f;
        }
    }

    
}
