using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Ingame coin amount
    public int CoinCount = 0;

    // Player score
    public int score = 0;
    private float scorePoint;

    private float Movespeed = 2f;
    private float maxMoveSpeed = 5f;

    private Rigidbody rb;
    private Vector3 movement;

    private float gravityScale = -2f;

    public float minX, maxX, minZ, maxZ;
    private Vector3 CurrentCameraPos;

    // Magnet Character Power
    GameObject[] Coins;
    Transform[] transformCoins;
    private bool MagnetPowerAvailable = true;
    private float magnetPowerTime = 5f;
    private float magnetTimer = 10f;
    private bool MagnetPowerActive;

    // Time Character Power
    private float fixedDeltaTime;
    private float TimepowerTime = 10f;
    private float Timetimer = 1f;
    private bool TimePowerAvailable = true;

    // UI
    private Slider powerBar;
    private float powerBarValue = 10f;
    private GameObject IngameUIObject;
    public IngameUI IngameUI;

    // Data Manager
    private GameObject dataManagerObject;
    public DataManager dataManager;
    private string currentCharacter;

    void Start()
    {
        //UI
        IngameUIObject = GameObject.Find("UI");
        IngameUI = IngameUIObject.GetComponent<IngameUI>();

        //Data Manger
        dataManagerObject = GameObject.Find("DataManager");
        dataManager = dataManagerObject.GetComponent<DataManager>();

        rb = this.GetComponent<Rigidbody>();

        this.fixedDeltaTime = Time.fixedDeltaTime;

        dataManager.Load();
        currentCharacter = dataManager.data.currentCharacter;

        // UI power bar
        powerBar = GameObject.Find("PowerBar").GetComponent<Slider>();
        powerBar.minValue = 0f;

        if (currentCharacter == "MagnetCharacter")
            powerBar.maxValue = 5f;
        else if (currentCharacter == "TimeCharacter")
            powerBar.maxValue = 10f;
        powerBar.value = powerBarValue;

        // Player Score
        scorePoint = this.transform.position.y - 50;

        Boundries();
    }

    void Update()
    {
        gravityScale -= 0.005f;
        transform.position -= Vector3.down * gravityScale * Time.deltaTime;

        // Get the player input for the player movement
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Player movement
        Movement(movement);

        // Calculate player movement boundries on screen
        CalculateBoundries();

        // Controls the power of the character that is selected
        Power(currentCharacter);

        // Add a Score
        AddScore();
    }

    void Movement(Vector3 direction)
    {
        if (rb.velocity.magnitude > maxMoveSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxMoveSpeed;
        }
        rb.AddForce(direction * Movespeed);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Dangerous_Object")
        {
            SaveData();
            IngameUI.RestartScreen();
        }
    }

    private void CalculateBoundries()
    {
        CurrentCameraPos = Camera.main.transform.position;
        Vector3 currentPosition = transform.position;

        currentPosition.x = Mathf.Clamp(currentPosition.x, minX + (CurrentCameraPos.x), maxX + (CurrentCameraPos.x));
        currentPosition.z = Mathf.Clamp(currentPosition.z, minZ + (CurrentCameraPos.z), maxZ + (CurrentCameraPos.z));

        transform.position = currentPosition;
    }

    private void Boundries()
    {
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector3 bottomCorners = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, camDistance));
        Vector3 topCorners = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, camDistance));

        minX = bottomCorners.x;
        maxX = topCorners.x;

        minZ = bottomCorners.z;
        maxZ = topCorners.z;
    }

    private void Power(string CurrentCharacter)
    {
        // Magnet Power for the Magnet Character
        if (CurrentCharacter == "MagnetCharacter")
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && MagnetPowerAvailable == true)
            {
                MagnetPowerActive = true;
            }
            if (MagnetPowerActive == true)
            {
                GetCoinsInactiveInRadius();
                magnetPowerTime -= 0.01f;
                magnetTimer = 10f;
            }
            if (magnetPowerTime <= 0)
            {
                MagnetPowerActive = false;
                MagnetPowerAvailable = false;
                magnetTimer -= 0.01f;
                if (magnetTimer <= 0)
                {
                    MagnetPowerAvailable = true;
                }
                if (magnetPowerTime < 5 && MagnetPowerAvailable == true)
                {
                    magnetPowerTime = 5f;
                }
            }
            // UI power bar
            powerBarValue = magnetPowerTime;
            powerBar.value = powerBarValue;
        }

        // Time Power for the Time Character
        else if (CurrentCharacter == "TimeCharacter")
        {
            if (Input.GetKey(KeyCode.LeftShift) && TimepowerTime > 0)
            {
                if (Time.timeScale == 1.0f)
                    Time.timeScale /= 2;
                TimepowerTime -= 0.01f;
                Timetimer = 1f;
            }
            else
            {
                Time.timeScale = 1.0f;
                TimePowerAvailable = false;
                Timetimer -= 0.01f;
                if (Timetimer <= 0)
                {
                    TimePowerAvailable = true;
                }
                if (TimepowerTime < 10 && TimePowerAvailable == true)
                {
                    TimepowerTime += 0.01f;
                }
            }
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;

            // UI power bar
            powerBarValue = TimepowerTime;
            powerBar.value = powerBarValue;
        }
    }

    private void GetCoinsInactiveInRadius()
    {
        Coins = GameObject.FindGameObjectsWithTag("Coin");

        transformCoins = new Transform[Coins.Length];
        for (int i = 0; i < Coins.Length; i++)
        {
            transformCoins[i] = Coins[i].transform;
        }

        foreach (Transform coin in transformCoins)
        {
            float distanceSqr = (transform.position - coin.position).sqrMagnitude;
            if (distanceSqr < 50f)
            {
                coin.position = Vector3.MoveTowards(coin.position, transform.position, 0.5f);
            }
        }
    }

    private void AddScore()
    {
        if (this.transform.position.y < scorePoint)
        {
            score++;
            scorePoint = this.transform.position.y - WorldGenerator.objectY;
        }
    }

    private void SaveData()
    {
        // Save coins to json
        dataManager.data.coins += CoinCount;

        // Save Highscore to json
        if (score > dataManager.data.highScore)
        {
            dataManager.data.highScore = score;
            Debug.Log("new highscore!!!!" + score);
        }

        dataManager.Save();
    }
}
