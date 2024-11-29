using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ETeam
{
    None,
    Blue,
    Red
}

public enum EClass
{
    None,
    Sniper,
    Engineer,
    BomberMan,
    Healer

}

public class PlayerConfig
{
    public static string playerName;
    public static ETeam team;
    public static EClass playerClass;


    public static Color TeamToColor(ETeam team)
    {
        return team == ETeam.Blue ? Color.blue : Color.red;
    }

}

public class StartGameController : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_Dropdown classDropdown;
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
        PlayerConfig.playerClass = (EClass) classDropdown.value+1;
        PlayerConfig.playerName = nameInputField.text;
        if (PlayerConfig.playerName == "") { PlayerConfig.playerName = "Player "+ System.Guid.NewGuid().ToString().Split("-")[0]; }
        SceneManager.LoadScene(1);
    }

}
