using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    void Start()
    {

        //random rotation
        int randomDegree = Random.Range(0,360);
        transform.Rotate(0, randomDegree, 0);

        //set enemy backwards relative to the rotation
        transform.Translate(0, 0, -20);
    }

    void Update()
    {
        if(GameObject.Find("Player").transform.position.y < this.transform.position.y + 50)
        {
            MoveObject();
        }
        
        DestroyObject();
    }

    public void MoveObject()
    {
        //move forward relative to rotation
        transform.Translate(0, 0, 5 * Time.deltaTime, Space.Self);
    }

    private void DestroyObject()
    {
        //Destroy object when position is higher than the camera position
        if(transform.position.y > Camera.main.transform.position.y)
        {
            Destroy(gameObject);
        }
    }
}
