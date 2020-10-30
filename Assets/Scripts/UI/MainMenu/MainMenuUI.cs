using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public DataManager dataManager;

    public InputField playerName;

    public GameObject selectedCharacter;

    void Start()
    {
        dataManager.Load();
        playerName.text = dataManager.data.playerName;
    }

    private void Update()
    {
        selectedCharacter.transform.Rotate(0, 45 * Time.deltaTime, 0);
    }

    public void StartGame()
    {
        if (dataManager.data.playerName == "")
        {
            playerName.image.color = Color.red;
            Debug.Log("Add your name");
        }
        else
            SceneManager.LoadScene("Game");

        FindObjectOfType<AudioManager>().Play("StartButton");
    }

    public void OpenShop()
    {
        SceneManager.LoadScene("Shop");
        FindObjectOfType<AudioManager>().Play("ShopSFX");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
        FindObjectOfType<AudioManager>().Play("Button");
    }

    public void SavePlayerName()
    {
        
        if (playerName.text != dataManager.data.playerName)
        {
            dataManager.data.highScore = 0;
            dataManager.data.playerName = playerName.text;
        }
        dataManager.Save();
        if (dataManager.data.playerName != "")
        {
            playerName.image.color = Color.white;
        }
        FindObjectOfType<AudioManager>().Play("Button");
    }
}
