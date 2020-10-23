using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(90 * Time.deltaTime, 0, 0);

        //Destroy object when position is higher than the camera position
        if (transform.position.y > Camera.main.transform.position.y)
        {
            DestroyObject();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            other.GetComponent<Player>().CoinCount++;
            Destroy(gameObject);
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
