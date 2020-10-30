using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenuUI : MonoBehaviour
{
    public DataManager dataManager;

    public Slider volume;
    public Text volumePercentage;

    void Start()
    {
        dataManager.Load();
        if (PlayerPrefs.HasKey("GameVolume"))
        {
            volume.value = PlayerPrefs.GetFloat("GameVolume");
        }
        else
            volume.value = AudioListener.volume;
    }

    private void Update()
    {
        volumePercentage.text = Mathf.Round(volume.value * 100).ToString();
        AudioListener.volume = volume.value;
    }

    public void Back()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        SceneManager.LoadScene("MainMenu");
        PlayerPrefs.SetFloat("GameVolume", AudioListener.volume);
    }

    public void Reset()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        dataManager.DeleteFile();
        PlayerPrefs.DeleteAll();
        Debug.Log("Your game has been reset");
    }
}
