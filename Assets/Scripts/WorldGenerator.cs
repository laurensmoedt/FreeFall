using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameObject[] AllGameObjects = new GameObject[4];

    private GameObject randomObject;

    public int numberOfObjects; //number of objects spawned in 100f on Y axis

    private float firstObjectY;
    private float objectY;
    private float objectStep;

    private float spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        spawnLocation = 50;
    }

    // Update is called once per frame
    void Update()
    {
        //spawns objects when player reaches a certain point on Y axis
        if (GameObject.Find("Player").transform.position.y < spawnLocation)
        {
            objectStep = 0; //resets Y value for the next random object
            firstObjectY = GameObject.Find("Player").transform.position.y - 50; //the first location for the objects to spawn
            numberOfObjects = 20;
            objectY = 100 / numberOfObjects;    //object devided between 100f on Y axis
            for (int i = 0; i < numberOfObjects; i++)
            {
                InstantiateObject();
            }
            spawnLocation -= 100; // sets new spawn location 
        }
    }

    private void InstantiateObject()
    {
        randomObject = AllGameObjects[Random.Range(0, AllGameObjects.Length)]; //picks a random object out of the list

        //sets a new Vector3 for the position of the random choosen object
        float x = Random.Range(-10 + randomObject.transform.localScale.x / 2, 10 + randomObject.transform.localScale.x / 2);    
        float y = firstObjectY - objectStep;
        float z = Random.Range(-10 + randomObject.transform.localScale.z / 2, 10 + randomObject.transform.localScale.z / 2);
        Vector3 pos = new Vector3(x, y, z); 

        //instantiate the random object with position and rotation onto the scene
        Instantiate(randomObject, pos, Quaternion.identity);
        objectStep += objectY; // adds Y value for the next random object
    }
}
