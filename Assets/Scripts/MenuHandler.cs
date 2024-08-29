using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif


// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuHandler : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_Text highScoreName;
    public TMP_Text highScoreNumber;
    public TMP_Text alertText;
    public GameObject highScoreDisplay;
    private string playerName = "";

    private void Start()
    {
        // clear alert
        alertText.text = "";
        // clear player name
        PlayerPrefs.SetString("PlayerName", playerName);
        // display high score only if there is one
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if(highScore > 0)
        {
            highScoreName.text = PlayerPrefs.GetString("HighScorePlayerName", "None");
            highScoreNumber.text = highScore.ToString();
            highScoreDisplay.SetActive(true);
        }
    }

    public void OnNameInputChange()
    {
        playerName = nameInput.text;
    }

    public void OnNameSubmit()
    {
        // Save name
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
        // Load Main Scene
        if(playerName != "")
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            alertText.text = "You need to enter a name";
        }
        //Debug.Log(playerName);
    }

    public void OnClearScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.SetString("HighScorePlayerName", "");
        highScoreName.text = "";
        highScoreNumber.text = "";
        highScoreDisplay.SetActive(false);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode(); // testing quit app
        #else
            Application.Quit(); // production quit app
        #endif
    }
}
