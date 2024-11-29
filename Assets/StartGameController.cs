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
    public static string username;
    public static ETeam team;
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
        PlayerConfig.username = nameInputField.text;
        if (PlayerConfig.username == "") { PlayerConfig.username = "Player "+ System.Guid.NewGuid().ToString().Split("-")[0]; }
        SceneManager.LoadScene(1);
    }

}
