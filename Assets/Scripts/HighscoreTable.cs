using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        //Initialize transforms
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        //Make base entry template inactive
        entryTemplate.gameObject.SetActive(false);

        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //Returns noting if there are no highscores yet
        if (highscores == null)
        {
            return;
        }

        // Sort entry list by Score
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    // Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                    if (highscores.highscoreEntryList.Count > 10)
                        highscores.highscoreEntryList.Remove(highscores.highscoreEntryList[j]);
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        //Create a new entry for highscore that is in the list
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;

        //instantiate a new template
        Transform entryTransform = Instantiate(entryTemplate, container);
        //Get rect transform from entry transform
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

        //Change position of transfrom by the amount of highscores
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        //Make rank count diffrent depending on the position of the highscore rank
        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        //Make every odd rank background inactive
        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);

        //Change all the values of the highscore
        entryTransform.Find("posText").GetComponent<Text>().text = rankString;
        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();
        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;

        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(int score, string name)
    {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            // There's no stored table, initialize
            highscores = new Highscores()
            {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }

        if (highscores.highscoreEntryList.Count == 0)
        {
            highscores.highscoreEntryList.Add(highscoreEntry);
        }
        else
        {
            bool isNotInList = false;
            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                if (highscores.highscoreEntryList[i].name == name)
                {
                    if (score == 0)
                    {
                        return;
                    }
                    else if (score > highscores.highscoreEntryList[i].score)
                    {
                        highscores.highscoreEntryList[i].score = score;
                        isNotInList = false;
                        break;
                    }
                }
                else
                    isNotInList = true;
            }
            if (isNotInList)
            {
                highscores.highscoreEntryList.Add(highscoreEntry);
            }
        }

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    
    //Represents a single High score entry
    [System.Serializable]
    public class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
