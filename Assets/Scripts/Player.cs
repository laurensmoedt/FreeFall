using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float movementSpeed;
    float increasingFallingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += transform.TransformDirection(Vector3.down) * 5.0f;
        increasingFallingSpeed = 0.05f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        transform.position += transform.TransformDirection(Vector3.down) * Time.deltaTime * increasingFallingSpeed;

        if (Input.GetKey("w")) 
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        if (Input.GetKey("s"))
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        if (Input.GetKey("a"))
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        if (Input.GetKey("d"))
            transform.position -= transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        
    }
}
