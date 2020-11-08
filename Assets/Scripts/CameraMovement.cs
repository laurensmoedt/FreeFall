using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform lookAt;
    private GameObject playerObject;

    private Vector3 offset = new Vector3(0,8,0);

    private void Awake()
    {
        //Initialize a gameobject, then make a transform out of that gameobject
        playerObject = GameObject.FindGameObjectWithTag("Player");
        lookAt = playerObject.GetComponent<Transform>();
    }
    private void Update()
    {
        //Move the camera with the player position
        Vector3 playerPos = lookAt.transform.position + offset;
        transform.position = new Vector3(playerPos.x / 5 , playerPos.y, playerPos.z / 5);
    }
}
