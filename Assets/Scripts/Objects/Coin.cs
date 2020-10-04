using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
