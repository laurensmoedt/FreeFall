using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class IngameUI : MonoBehaviour
{
    public DataManager dataManager;
    public HighscoreTable highscoreTable;

    // Ingame overlay
    private Player player;
    private GameObject playerObject;

    public Text coins;
    public Text score;

    // Restart sceen
    public Canvas restartCanvas;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
        dataManager.Load();
        restartCanvas.GetComponent<CanvasScaler>().scaleFactor = 0;
    }

    private void Update()
    {
        coins.text = player.CoinCount.ToString();
        score.text = player.score.ToString();
    }

    public void RestartScreen()
    {
        if (dataManager.data.currentCharacter == "GlassBallCharacter")
        {
            FindObjectOfType<AudioManager>().Play("BrokenGlass");
        }
        else
            FindObjectOfType<AudioManager>().Play("Impact");
        restartCanvas.GetComponent<CanvasScaler>().scaleFactor = 1;
        highscoreTable.AddHighscoreEntry(dataManager.data.highScore, dataManager.data.playerName);
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
