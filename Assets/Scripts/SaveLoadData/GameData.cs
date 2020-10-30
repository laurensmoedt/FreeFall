using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public string playerName = "";
    public string currentCharacter = "GlassBallCharacter";
    
    public int coins = 0;
    public int highScore = 0;

    public bool magnetCharacterUnloked = false;
    public bool timeCharacterUnloked = false;
}
