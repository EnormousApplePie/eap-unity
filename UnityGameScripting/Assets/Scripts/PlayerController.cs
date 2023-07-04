using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera m_Camera;
    Rigidbody rb;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float movementSpeed = 20f;
    GameObject floorPlane;
    // Start is called before the first frame update
    void Start()
    {
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
            Vector3 playerPos = transform.position;
            Vector2 difference = new Vector2(hit.point.x - playerPos.x, hit.point.z - playerPos.z);
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            rotationZ -= 90;
            transform.rotation = Quaternion.Euler(0.0f, -rotationZ, 0.0f);
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
}
