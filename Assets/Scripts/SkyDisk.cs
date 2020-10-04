using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyDisk : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, GameObject.Find("Player").transform.position.y -50, transform.position.z);
    }
}
