using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreText;
    private string playerScoreString = "playerScore";

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        scoreText.text = "Your score is - " + getScoreData();
    }
    private int getScoreData()
    {
        return PlayerPrefs.GetInt(playerScoreString);
    }
}
