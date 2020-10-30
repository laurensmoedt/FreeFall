using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraMovement : MonoBehaviour
{
    private Transform lookAt;
    private GameObject playerObject;

    private Vector3 offset = new Vector3(0,8,0);

    private void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        lookAt = playerObject.GetComponent<Transform>();
    }
    void Update()
    {
        Vector3 playerPos = lookAt.transform.position + offset;

        transform.position = new Vector3(playerPos.x / 5 , playerPos.y, playerPos.z / 5);
    }
}
