using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField]
    DataManager dataManager = null;

    [SerializeField]
    GameObject[] allCharacters = null;

    [SerializeField]
    GameObject[] allGameObjects = null;

    [SerializeField]
    GameObject[] islands = null;

    private GameObject player;

    private GameObject randomObject;
    private GameObject randomIsland;

    private float firstObjectY;
    public static float objectYdifference;
    private float objectStep;
    private readonly int spawnRange = 100;

    private float spawnLocation = 50;

    private float X, Y, Z; // Position for the spawnable objects

    private void Awake()
    {
        //load data from data manager
        dataManager.Load();

        //Check for which character is active, then instantiate them onto the screen
        if (dataManager.data.currentCharacter == "GlassCubeCharacter")
        {
            Instantiate(allCharacters[0], new Vector3(0, 100, 0), Quaternion.identity);
        }
        else if (dataManager.data.currentCharacter == "MagnetCharacter")
        {
            Instantiate(allCharacters[1], new Vector3(0, 100, 0), Quaternion.identity);
        }
        else if(dataManager.data.currentCharacter == "TimeCharacter")
        {
            Instantiate(allCharacters[2], new Vector3(0, 100, 0), Quaternion.identity);
        }

        // Find player object
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        //spawn a certain amount of objects when player reaches spawnLocation
        if (player.transform.position.y < spawnLocation)
        {
            objectStep = 0f; //resets Y value for when a new spawn point has been reached
            firstObjectY = player.transform.position.y - 100f; //the first location for the objects to spawn
            int numberOfObjects = 20;
            objectYdifference = spawnRange / numberOfObjects;    //object devided between 100f on Y axis
            for (int i = 0; i < numberOfObjects; i++)
            {
                InstantiateObject();
            }
            spawnLocation -= spawnRange; // sets new spawn location 
        }
    }

    private void InstantiateObject()
    {
        randomObject = allGameObjects[Random.Range(0, allGameObjects.Length)]; //picks a random object out of the list

        if (randomObject == allGameObjects[2]) // Spawn coins only in reach of the player, not outside the boundries
        {
            X = Random.Range(player.GetComponent<Player>().minX, player.GetComponent<Player>().maxX);
            Z = Random.Range(player.GetComponent<Player>().minZ, player.GetComponent<Player>().maxZ);
        }
        else
        {
            //sets new X and Z value for the position of the random choosen object
            X = Random.Range(player.GetComponent<Player>().minX * 2, player.GetComponent<Player>().maxX * 2);
            Z = Random.Range(player.GetComponent<Player>().minZ * 2, player.GetComponent<Player>().maxZ * 2);
        }
        if (randomObject == allGameObjects[0])// check if the random object is an Island
        {
            randomIsland = islands[Random.Range(0, islands.Length)];
            randomObject = randomIsland;
        }
        
        Y = firstObjectY - objectStep;

        Vector3 pos = new Vector3(X, Y, Z);

        //instantiate the random object with position and rotation onto the scene
        Instantiate(randomObject, pos, Quaternion.identity);
        objectStep += objectYdifference; // adds Y value for the next random object
    }
}
