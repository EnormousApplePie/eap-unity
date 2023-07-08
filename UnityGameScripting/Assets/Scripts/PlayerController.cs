using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Creature
{
    public static PlayerController currentPlayer { get; private set; }

    public float projectileSpeed = 2;
    public int projectileStrength = 10;

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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //ignore the ignore raycast layer
       
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            TurnToPosition(hit.point);
        }

        if (Input.GetKey(KeyCode.W))
        {
            //add some force to the rigidbody
            rb.AddForce(transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //add some force to the rigidbody
            rb.AddForce(-transform.forward * movementSpeed);
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //ignore the ignore raycast layer

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Projectile.Create(transform, GetDirectionTowardsPoint(hit.point), projectileSpeed, projectileStrength);
        }
    }
}
