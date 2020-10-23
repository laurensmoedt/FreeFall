using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        SpawnObject();
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

    private void SpawnObject()
    {
        // spawn tree and rock on Island with random location

    }
}
