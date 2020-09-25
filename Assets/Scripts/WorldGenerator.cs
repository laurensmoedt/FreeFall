using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    public GameObject IslandPrefab;
    public GameObject CoinPrefab;

    public int numberOfIsland;
    public int numberOfCoins;

    private float islandY;
    private float coinY;

    // Start is called before the first frame update
    void Start()
    {
        
        numberOfIsland = Random.Range(5, 10);
        numberOfCoins = Random.Range(5, 15);

        islandY = 0;
        coinY = 0;
        InstantiateObject();
    }

    // Update is called once per frame
    void Update()
    {
        // call this when player reaches certain point
        //InstantiateObject();
    }

    private void InstantiateObject()
    {
        for (int i = 0; i < numberOfIsland; i++)
        {
            float x = Random.Range(-25, 25);
            float y = Random.Range(0 + islandY, 10 + islandY);
            float z = Random.Range(-25, 25);
            Vector3 pos = transform.position + new Vector3(x, y, z);

            Instantiate(IslandPrefab, pos, Quaternion.identity);
            islandY += 10;
        }

        for (int i = 0; i < numberOfCoins; i++)
        {
            float x = Random.Range(-6, 6);
            float y = Random.Range(0 + coinY, 10 + coinY);
            float z = Random.Range(-4, 4);
            Vector3 pos = transform.position + new Vector3(x, y, z);

            Instantiate(CoinPrefab, pos, Quaternion.identity);
            coinY += 10;
        }
    }
}
