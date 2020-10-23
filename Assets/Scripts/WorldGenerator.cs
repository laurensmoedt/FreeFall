using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject[] AllGameObjects = new GameObject[4];

    [SerializeField]
    GameObject[] Islands = new GameObject[4];

    private GameObject randomObject;
    private GameObject randomIsland;
    public GameObject Player;

    private float firstObjectY;
    private float objectY;
    private float objectStep;

    private float spawnLocation = 50;

    private float X, Y, Z; // For the spawnable objects

    void Start()
    {

    }

    void Update()
    {
        //spawns objects when player reaches a certain point on Y axis
        if (GameObject.Find("Player").transform.position.y < spawnLocation)
        {
            objectStep = 0; //resets Y value for the next random object
            firstObjectY = GameObject.Find("Player").transform.position.y - 50; //the first location for the objects to spawn
            int numberOfObjects = 20;
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

        if (randomObject == AllGameObjects[2]) // Spawn coins only in reach of the player, not outside the boundries
        {
            X = Random.Range(Player.GetComponent<Player>().minX, Player.GetComponent<Player>().maxX);
            Z = Random.Range(Player.GetComponent<Player>().minZ, Player.GetComponent<Player>().maxZ);
        }
        if (randomObject == AllGameObjects[0])// check if the random object is an Island
        {
            randomIsland = Islands[Random.Range(0, Islands.Length)];
            randomObject = randomIsland;
        }
        else
        {
            //sets a new Vector3 for the position of the random choosen object
            X = Random.Range(-10 + randomObject.transform.localScale.x / 2, 10 + randomObject.transform.localScale.x / 2);
            Z = Random.Range(-10 + randomObject.transform.localScale.z / 2, 10 + randomObject.transform.localScale.z / 2);
        }

        Y = firstObjectY - objectStep;

        Vector3 pos = new Vector3(X, Y, Z);

        //instantiate the random object with position and rotation onto the scene
        Instantiate(randomObject, pos, Quaternion.identity);
        objectStep += objectY; // adds Y value for the next random object
    }
}
