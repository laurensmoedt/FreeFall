using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IngameUI : MonoBehaviour
{
    [SerializeField]
    DataManager dataManager = null;

    [SerializeField]
    HighscoreTable highscoreTable = null;

    // Ingame overlay
    private Player player;
    private GameObject playerObject;

    public Text coins = null;

    public Text score = null;

    // Restart sceen
    [SerializeField]
    GameObject restartCanvas = null;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();

        dataManager.Load();

        //Deactivate restart menu overlay
        restartCanvas.SetActive(false);
    }

    private void Update()
    {
        coins.text = "COINS: " + player.coinCount.ToString();
        score.text = "SCORE: " + player.score.ToString();
    }

    public void RestartScreen()
    {
        if (dataManager.data.currentCharacter == "GlassCubeCharacter")
        {
            FindObjectOfType<AudioManager>().Play("BrokenGlass");
        }
        else
            FindObjectOfType<AudioManager>().Play("Impact");

        restartCanvas.SetActive(true);
        highscoreTable.AddHighscoreEntry(dataManager.data.highScore, dataManager.data.playerName);
        //Stop the game from playing
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void Shop()
    {
        FindObjectOfType<AudioManager>().Play("ShopSFX");
        SceneManager.LoadScene("Shop");
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
