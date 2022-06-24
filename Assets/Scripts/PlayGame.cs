using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

    }
    // Start is called before the first frame update
    public void startGame()
    {
        Debug.Log("Here");
        SceneManager.LoadScene("GamePlayScreen");
    }
}
