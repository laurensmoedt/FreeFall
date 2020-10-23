using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    Transform lookAt = null;

    private Vector3 offset = new Vector3(0,8,0);

    void Update()
    {
        Vector3 playerPos = lookAt.transform.position + offset;

        transform.position = new Vector3(playerPos.x / 5 , playerPos.y, playerPos.z / 5);
    }
}
