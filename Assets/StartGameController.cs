using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ETeam
{
    Blue,
    Red
}

public class PlayerConfig
{
    public static string playerName;
    public static ETeam team;


    public static Color TeamToColor(ETeam team)
    {
        return team == ETeam.Blue ? Color.blue : Color.red;
    }

}

public class StartGameController : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button startGameButtonRed;
    public Button startGameButtonBlue;

    void Start()
    {
        startGameButtonRed.onClick.AddListener(StartGameRed);
        startGameButtonBlue.onClick.AddListener(StartGameBlue);


    }

    public void StartGameRed() { 
        PlayerConfig.team = ETeam.Red;
        StartGame();
    }
    public void StartGameBlue() {
        PlayerConfig.team = ETeam.Blue;
        StartGame();
    }

    public void StartGame()
    {
        PlayerConfig.playerName = nameInputField.text;
        if (PlayerConfig.playerName == "") { PlayerConfig.playerName = "Player "+ System.Guid.NewGuid().ToString().Split("-")[0]; }
        SceneManager.LoadScene(1);
    }

}
