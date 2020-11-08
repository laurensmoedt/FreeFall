using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Ingame coin amount
    public int coinCount = 0;

    // Player score
    public int score = 0;
    private float scorePoint;

    private float movespeed = 5f;
    private float maxMoveSpeed = 9f;

    // RigidBody & Movement for the RB
    private Rigidbody rb;
    private Vector3 movement;

    // Set the gravity
    private float gravityScale = -8f;

    // Set the boundaries for the player relative to the camera screen
    public float minX, maxX, minZ, maxZ;
    private Vector3 CurrentCameraPos;

    // Magnet Character Power
    Transform[] transformCoins;
    private bool magnetPowerAvailable = true;
    private float magnetPowerTime = 5f;
    private float magnetTimer = 10f;
    private bool magnetPowerActive;

    // Time Character Power
    private float fixedDeltaTime;
    private float timepowerTime = 10f;
    private float timetimer = 1f;
    private bool timePowerAvailable = true;

    // UI
    private Slider powerBar;
    private float powerBarValue = 10f;
    private GameObject ingameUIObject;
    private IngameUI ingameUI;

    // Data Manager
    private GameObject dataManagerObject;
    private DataManager dataManager;
    private string currentCharacter;

    private void Start()
    {
        //UI
        ingameUIObject = GameObject.Find("UI");
        ingameUI = ingameUIObject.GetComponent<IngameUI>();

        //Data Manger initialized in class, because player is initialized after UI
        dataManagerObject = GameObject.Find("DataManager");
        dataManager = dataManagerObject.GetComponent<DataManager>();
        dataManager.Load();
        currentCharacter = dataManager.data.currentCharacter;

        rb = this.GetComponent<Rigidbody>();

        //Delta time for slowing down time with time character's power
        fixedDeltaTime = Time.fixedDeltaTime;

        //UI power bar
        powerBar = GameObject.Find("PowerBar").GetComponent<Slider>();
        powerBar.minValue = 0f;

        if (currentCharacter == "MagnetCharacter")
            powerBar.maxValue = 5f;
        else if (currentCharacter == "TimeCharacter")
            powerBar.maxValue = 10f;

        if (currentCharacter != "GlassCubeCharacter")
            powerBar.value = powerBarValue;
        else
        {
            powerBar.value = 0f;
            GameObject.FindGameObjectWithTag("PowerText").GetComponent<Text>().text = "";
        }

        //Player Score start count position
        scorePoint = this.transform.position.y - 50;

        Boundaries();
    }

    private void Update()
    {
        //Get the player input for the player movement
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Power(currentCharacter);
    }

    private void FixedUpdate()
    {
        //Falling speed increasing
        gravityScale -= 0.02f;
        transform.position -= Vector3.down * gravityScale * Time.deltaTime;

        Movement(movement);
        CalculateBoundries();
        AddScore();
    }

    private void Movement(Vector3 direction)
    {
        //Acceleration until max speed is reached
        if (rb.velocity.magnitude > maxMoveSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxMoveSpeed;
        }

        //Rotation of the player
        Vector3 torqueRotation = new Vector3(0, Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        rb.AddTorque(torqueRotation * -movespeed);

        //Movement of the player
        rb.AddForce(direction * movespeed);
    }

    private void OnCollisionEnter(Collision other)
    {
        //Check for collision with objects with tag
        if (other.gameObject.tag == "Dangerous_Object")
        {
            //Save data of this round and show restart screen
            SaveData();
            ingameUI.RestartScreen();
        }
    }

    private void CalculateBoundries()
    {
        CurrentCameraPos = Camera.main.transform.position;
        Vector3 currentPosition = transform.position;

        //Player cannot move beyond max and min value
        currentPosition.x = Mathf.Clamp(currentPosition.x, minX + (CurrentCameraPos.x), maxX + (CurrentCameraPos.x));
        currentPosition.z = Mathf.Clamp(currentPosition.z, minZ + (CurrentCameraPos.z), maxZ + (CurrentCameraPos.z));

        transform.position = currentPosition;
    }

    private void Boundaries()
    {
        //Get the distance between the player and camera
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);

        Vector3 bottomCorners = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, camDistance));
        Vector3 topCorners = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, camDistance));

        //Get the min and max value of the screen display
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
            if (Input.GetKeyDown(KeyCode.LeftShift) && magnetPowerAvailable == true)
            {
                magnetPowerActive = true;
            }
            if (magnetPowerActive == true)
            {
                GetCoinsInactiveInRadius();
                magnetPowerTime -= 0.2f * Time.fixedDeltaTime;       //Decrease power when using power
                magnetTimer = 10f;              //Set a timer for when magnet power is out of use
            }
            if (magnetPowerTime <= 0)
            {
                magnetPowerActive = false;      //Set active to false so that the player cannot activate the power when there is no power
                magnetPowerAvailable = false;
                magnetTimer -= 0.2f * Time.fixedDeltaTime;           //Decrease the timer
                if (magnetTimer <= 0)
                {
                    //Set the magnet power able to be activated again
                    magnetPowerAvailable = true;
                }
                if (magnetPowerTime < 5 && magnetPowerAvailable == true)
                {
                    //Set power time of magnet to max when magnet power is available again
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
            if (Time.timeScale == 0) { return; };
            if (Input.GetKey(KeyCode.LeftShift) && timepowerTime > 0)
            {
                //Slows down time by half
                if (Time.timeScale == 1.0f)
                    Time.timeScale /= 2;
                //Decrease power time when using
                timepowerTime -= 0.4f * Time.fixedDeltaTime;
                //Set Power recharge time
                timetimer = 1f;
            }
            else
            {
                //Set timescale to normal when not using power
                Time.timeScale = 1.0f;
                timePowerAvailable = false;
                timetimer -= 0.2f * Time.fixedDeltaTime;
                if (timetimer <= 0)
                {
                    //If recharge timer reaches the end, make the player able to use power again
                    timePowerAvailable = true;
                }
                if (timepowerTime < 10 && timePowerAvailable == true)
                {
                    //If recharge timer reaches the end, recharge the power by a certain amount
                    timepowerTime += 0.2f * Time.fixedDeltaTime;
                }
            }
            //Update the deltatime
            Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;

            //UI power bar
            powerBarValue = timepowerTime;
            powerBar.value = powerBarValue;
        }
    }

    private void GetCoinsInactiveInRadius()
    {
        //Find all coins with tag "Coin"
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

        //Make a transform array with all found coin gameobjects
        transformCoins = new Transform[coins.Length];

        //convert all gameobject coins into transforms
        for (int i = 0; i < coins.Length; i++)
            transformCoins[i] = coins[i].transform;

        //Calculate if a transform coin is in range of the player, so yes, move the coin towards the player
        foreach (Transform coin in transformCoins)
        {
            float distanceSqr = (transform.position - coin.position).sqrMagnitude;
            if (distanceSqr < 100f)
            {
                coin.position = Vector3.MoveTowards(coin.position, transform.position, -gravityScale * Time.fixedDeltaTime);
            }
        }
    }

    private void AddScore()
    {
        //If player reaches scorepoint, add a point to score
        if (this.transform.position.y < scorePoint)
        {
            score++;
            //Set the next score position
            scorePoint = this.transform.position.y - WorldGenerator.objectYdifference;
        }
    }

    private void SaveData()
    {
        // Save coins to json
        dataManager.data.coins += coinCount;

        // Save Highscore to json
        if (score > dataManager.data.highScore)
        {
            dataManager.data.highScore = score;
        }
        dataManager.Save();
    }
}
