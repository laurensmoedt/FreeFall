using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public DataManager dataManager;

    public InputField playerName;

    public GameObject selectedCharacter;

    private void Start()
    {
        dataManager.Load();
        playerName.characterLimit = 14;
        playerName.text = dataManager.data.playerName;

        //Set game volume
        if (PlayerPrefs.HasKey("GameVolume"))
            AudioListener.volume = PlayerPrefs.GetFloat("GameVolume");

        //Set rotation character preview for better angle
        selectedCharacter.transform.Rotate(-45, 0, 0);
    }

    private void Update()
    {
        //Rotate player preview
        selectedCharacter.transform.Rotate(0, 45 * Time.deltaTime, 0);
    }

    public void StartGame()
    {
        //Cannot play the game if the player did not enter a username
        if (dataManager.data.playerName == "")
        {
            playerName.image.color = Color.red;
            Debug.Log("Add your name");
        }
        else
            SceneManager.LoadScene("Game");

        FindObjectOfType<AudioManager>().Play("StartButton");
    }
    public void QuitGame()
    {
        Application.Quit();
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
            //Reset score if entered a new playername
            dataManager.data.highScore = 0;
            dataManager.data.playerName = playerName.text;

            dataManager.Save();
        }
        if (dataManager.data.playerName != "")
        {
            playerName.image.color = Color.white;
        }
        FindObjectOfType<AudioManager>().Play("Button");
    }
}
