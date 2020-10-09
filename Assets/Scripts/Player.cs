using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public int CoinCount = 0;

    private float movementSpeed;

    public CharacterController controller;
    
    private Vector3 moveDirection;
    private float gravityScale;

    private float minX, maxX, minZ, maxZ;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 10f;

        controller = GetComponent<CharacterController>();

<<<<<<< HEAD
        gravityScale = 2f;

        Boundaries();
=======
        gravityScale = 1f;

        Boundries();
>>>>>>> f90b5b884b5cf03c356176a1c23d165b21b2c38d
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, 0f, Input.GetAxis("Vertical") * movementSpeed);

<<<<<<< HEAD
        gravityScale += 0.002f;
=======
>>>>>>> f90b5b884b5cf03c356176a1c23d165b21b2c38d
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);

        controller.Move(moveDirection * Time.deltaTime);

        CalculateBoundries();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dangerous_Object")
        {
            UnityEditor.EditorApplication.ExecuteMenuItem("Edit/Play");
        }
    }

    private void CalculateBoundries()
    {
        Vector3 currentPosition = transform.position;

        currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);
        currentPosition.z = Mathf.Clamp(currentPosition.z, minZ, maxZ);

        transform.position = currentPosition;
    }

<<<<<<< HEAD
    private void Boundaries()
=======
    private void Boundries()
>>>>>>> f90b5b884b5cf03c356176a1c23d165b21b2c38d
    {
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector3 bottomCorners = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, camDistance));
        Vector3 topCorners = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, camDistance));

        minX = bottomCorners.x;
        maxX = topCorners.x;

        minZ = bottomCorners.z;
        maxZ = topCorners.z;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Coins: " + CoinCount);
    }
}
