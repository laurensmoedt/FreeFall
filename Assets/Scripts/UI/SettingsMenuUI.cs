using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenuUI : MonoBehaviour
{
    public DataManager dataManager;

    [SerializeField]
    Slider volume =null;

    [SerializeField]
    Text volumePercentage = null;

    private void Start()
    {
        dataManager.Load();

        //Check if the volume has already been changed before
        if (PlayerPrefs.HasKey("GameVolume"))
        {
            volume.value = PlayerPrefs.GetFloat("GameVolume");
        }
        else
            volume.value = AudioListener.volume;
    }

    private void Update()
    {
        volumePercentage.text = Mathf.Round(volume.value * 100).ToString() + "%";
        AudioListener.volume = volume.value;
    }

    public void Back()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        SceneManager.LoadScene("MainMenu");
        PlayerPrefs.SetFloat("GameVolume", AudioListener.volume);
    }

    public void ResetGame()
    {
        //Resets all the game data and all the player prefs
        FindObjectOfType<AudioManager>().Play("Button");
        dataManager.DeleteFile();
        PlayerPrefs.DeleteAll();
    }
}
