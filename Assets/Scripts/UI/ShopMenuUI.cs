using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopMenuUI : MonoBehaviour
{
    public DataManager dataManager;

    //Coin amount of the player
    public Text coins;

    //Price of the characters
    private int magnetCharacterCost = 50;
    private int timeCharacterCost = 200;

    [SerializeField]
    Sprite[] allCharacters = new Sprite[3];
    private int currentSprite = 0;
    public Image character;

    // PopUp
    public GameObject popUpBox;

    private void Start()
    {
        //Get available data from data manager
        dataManager.Load();
        coins.text = dataManager.data.coins.ToString();
        
        character.sprite = allCharacters[currentSprite];

        NextPreviousSetActive();
        BuyOrSelectButton();
    }

    public void Back()
    {
        dataManager.Save();
        FindObjectOfType<AudioManager>().Play("Button");
        SceneManager.LoadScene("MainMenu");
    }

    public void SelectCharacter()
    {
        //Set the current character in game data to the selected character
        dataManager.data.currentCharacter = allCharacters[currentSprite].name;
        FindObjectOfType<AudioManager>().Play("Button");
        dataManager.Save();
        SceneManager.LoadScene("MainMenu");
    }

    public void BuyCharacter()
    {
        //Check for character name and if the player has enough coins
        if (allCharacters[currentSprite].name == "MagnetCharacter" && dataManager.data.coins >= magnetCharacterCost)
        {
            dataManager.data.coins -= magnetCharacterCost;
            dataManager.data.magnetCharacterUnloked = true;
            coins.text = dataManager.data.coins.ToString();
            dataManager.data.currentCharacter = allCharacters[currentSprite].name;
            FindObjectOfType<AudioManager>().Play("BuyCharacter");
        }
        else if (allCharacters[currentSprite].name == "TimeCharacter" && dataManager.data.coins >= timeCharacterCost)
        {
            dataManager.data.coins -= timeCharacterCost;
            dataManager.data.timeCharacterUnloked = true;
            coins.text = dataManager.data.coins.ToString();
            dataManager.data.currentCharacter = allCharacters[currentSprite].name;
            FindObjectOfType<AudioManager>().Play("BuyCharacter");
        }
        else
        {
            popUpBox.SetActive(true);
        }
        BuyOrSelectButton();
    }

    public void ClosePopUpBox()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        popUpBox.SetActive(false);
    }

    private void BuyOrSelectButton()
    {
        //Check if the player has the character already unlocked, with that information the player gets a select/buy button
        if (allCharacters[currentSprite].name == "GlassCubeCharacter")
        {
            GameObject.Find("Buy").transform.localScale = new Vector3(0, 0, 0);
            GameObject.Find("Select").transform.localScale = new Vector3(1, 1, 1);
        }
        else if (allCharacters[currentSprite].name == "MagnetCharacter" && dataManager.data.magnetCharacterUnloked == true)
        {
            GameObject.Find("Buy").transform.localScale = new Vector3(0, 0, 0);
            GameObject.Find("Select").transform.localScale = new Vector3(1, 1, 1);
        }
        else if (allCharacters[currentSprite].name == "TimeCharacter" && dataManager.data.timeCharacterUnloked == true)
        {
            GameObject.Find("Buy").transform.localScale = new Vector3(0, 0, 0);
            GameObject.Find("Select").transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            GameObject.Find("Buy").transform.localScale = new Vector3(1, 1, 1);
            GameObject.Find("Select").transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public void Next()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        currentSprite += 1;
        character.sprite = allCharacters[currentSprite];

        NextPreviousSetActive();
        BuyOrSelectButton();
    }

    public void Previous()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        currentSprite -= 1;
        character.sprite = allCharacters[currentSprite];

        NextPreviousSetActive();
        BuyOrSelectButton();
    }

    private void NextPreviousSetActive()
    {
        //Set next/previous button active when reaching the end or beginning of the amount of characters

        if (currentSprite > 0)
        {
            GameObject.Find("Previous").transform.localScale = new Vector3(1, 1, 1);
        }
        else
            GameObject.Find("Previous").transform.localScale = new Vector3(0, 0, 0);


        if (currentSprite < allCharacters.Length -1)
        {
            GameObject.Find("Next").transform.localScale = new Vector3(1, 1, 1);
        }
        else
            GameObject.Find("Next").transform.localScale = new Vector3(0, 0, 0);

    }
}
