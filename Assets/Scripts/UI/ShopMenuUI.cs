using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopMenuUI : MonoBehaviour
{
    public DataManager dataManager;

    public Text coins;

    private int magnetCharacterCost = 100;
    private int TimeCharacterCost = 200;

    [SerializeField]
    Sprite[] allCharacters = new Sprite[3];
    private int currentSprite = 0;
    public Image character;

    // PopUp
    public GameObject popUpBox;

    void Start()
    {
        dataManager.Load();
        coins.text = dataManager.data.coins.ToString();
        // TODO: change image of character according to the current character
        character.sprite = allCharacters[currentSprite];

        BuyOrSelectButton();
    }

    public void Back()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        SceneManager.LoadScene("MainMenu");
    }

    public void SelectCharacter()
    {
        dataManager.data.currentCharacter = allCharacters[currentSprite].name;
        dataManager.Save();
        FindObjectOfType<AudioManager>().Play("Button");
        Debug.Log("current character: " + dataManager.data.currentCharacter);
    }

    public void BuyCharacter()
    {
        if (allCharacters[currentSprite].name == "MagnetCharacter" && dataManager.data.coins > magnetCharacterCost)
        {
            dataManager.data.coins -= magnetCharacterCost;
            dataManager.data.magnetCharacterUnloked = true;
            FindObjectOfType<AudioManager>().Play("BuyCharacter");
        }
        else if (allCharacters[currentSprite].name == "TimeCharacter" && dataManager.data.coins > TimeCharacterCost)
        {
            dataManager.data.coins -= TimeCharacterCost;
            dataManager.data.timeCharacterUnloked = true;
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
        // seelect or buy character
        if (allCharacters[currentSprite].name == "GlassBall")
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
        if (currentSprite < 2)
        {
            GameObject.Find("Next").transform.localScale = new Vector3(1, 1, 1);
        }
        else
            GameObject.Find("Next").transform.localScale = new Vector3(0, 0, 0);

        if (currentSprite > 0)
        {
            GameObject.Find("Previous").transform.localScale = new Vector3(1, 1, 1);
        }
        BuyOrSelectButton();
    }

    public void Previous()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        currentSprite -= 1;
        character.sprite = allCharacters[currentSprite];
        if (currentSprite > 0)
        {
            GameObject.Find("Previous").transform.localScale = new Vector3(1, 1, 1);
        } 
        else
            GameObject.Find("Previous").transform.localScale = new Vector3(0, 0, 0);

        if (currentSprite < 2)
        {
            GameObject.Find("Next").transform.localScale = new Vector3(1, 1, 1);
        }
        BuyOrSelectButton();
    }
}
