using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
