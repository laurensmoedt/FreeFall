using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public DataManager dataManager;

    [SerializeField]
    GameObject[] AllCharacters = new GameObject[3];

    [SerializeField]
    GameObject[] AllGameObjects = new GameObject[4];

    [SerializeField]
    GameObject[] Islands = new GameObject[4];

    private GameObject randomObject;
    private GameObject randomIsland;
    private GameObject player;

    private float firstObjectY;
    public static float objectY;
    private float objectStep;
    private int spawnRange = 100;

    private float spawnLocation = 50;

    private float X, Y, Z; // Position for the spawnable objects

    private void Awake()
    {
        // Instantiate Player object
        dataManager.Load();
        if (dataManager.data.currentCharacter == "GlassBallCharacter")
        {
            Instantiate(AllCharacters[0], new Vector3(0, 100, 0), Quaternion.identity);
        }
        else if (dataManager.data.currentCharacter == "MagnetCharacter")
        {
            Instantiate(AllCharacters[1], new Vector3(0, 100, 0), Quaternion.identity);
        }
        else if(dataManager.data.currentCharacter == "TimeCharacter")
        {
            Instantiate(AllCharacters[2], new Vector3(0, 100, 0), Quaternion.identity);
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //spawns objects when player reaches a certain point on Y axis
        if (player.transform.position.y < spawnLocation)
        {
            objectStep = 0; //resets Y value for the next random object
            firstObjectY = player.transform.position.y - 50; //the first location for the objects to spawn
            int numberOfObjects = 20;
            objectY = spawnRange / numberOfObjects;    //object devided between 100f on Y axis
            for (int i = 0; i < numberOfObjects; i++)
            {
                InstantiateObject();
            }
            spawnLocation -= spawnRange; // sets new spawn location 
        }
    }

    private void InstantiateObject()
    {
        randomObject = AllGameObjects[Random.Range(0, AllGameObjects.Length)]; //picks a random object out of the list

        if (randomObject == AllGameObjects[2]) // Spawn coins only in reach of the player, not outside the boundries
        {
            X = Random.Range(player.GetComponent<Player>().minX, player.GetComponent<Player>().maxX);
            Z = Random.Range(player.GetComponent<Player>().minZ, player.GetComponent<Player>().maxZ);
        }
        else
        {
            //sets a new Vector3 for the position of the random choosen object
            X = Random.Range(-15 + randomObject.transform.localScale.x / 2, 15 + randomObject.transform.localScale.x / 2);
            Z = Random.Range(-15 + randomObject.transform.localScale.z / 2, 15 + randomObject.transform.localScale.z / 2);
        }
        if (randomObject == AllGameObjects[0])// check if the random object is an Island
        {
            randomIsland = Islands[Random.Range(0, Islands.Length)];
            randomObject = randomIsland;
        }
        

        Y = firstObjectY - objectStep;

        Vector3 pos = new Vector3(X, Y, Z);

        //instantiate the random object with position and rotation onto the scene
        Instantiate(randomObject, pos, Quaternion.identity);
        objectStep += objectY; // adds Y value for the next random object
    }
}
