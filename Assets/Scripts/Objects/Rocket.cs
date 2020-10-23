using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(0, 5 * Time.deltaTime, 0, Space.Self);

        DestroyObject();
    }

    private void DestroyObject()
    {
        //Destroy object when position is higher than the camera position
        if (transform.position.y > Camera.main.transform.position.y)
        {
            Destroy(gameObject);
        }
    }
}
