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

        gravityScale = 1f;

        Boundries();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, 0f, Input.GetAxis("Vertical") * movementSpeed);

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);

        controller.Move(moveDirection * Time.deltaTime);

        CalculateBoundries();
    }

    private void CalculateBoundries()
    {
        Vector3 currentPosition = transform.position;

        currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);
        currentPosition.z = Mathf.Clamp(currentPosition.z, minZ, maxZ);

        transform.position = currentPosition;
    }

    private void Boundries()
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
