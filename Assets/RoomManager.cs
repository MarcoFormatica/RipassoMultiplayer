using Fusion;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : NetworkBehaviour
{


    [Networked, OnChangedRender(nameof(OnTimeLeftChanged))]
    public int TimeLeft { get; set; }

    [Networked, OnChangedRender(nameof(OnWinnerTextChanged))]
    public NetworkString<_64> WinnerText { get; set; }

    public void OnTimeLeftChanged()
    {
        FindObjectOfType<MultiplayerManager>().timeText.text = TimeLeft.ToString();
    }
    public void OnWinnerTextChanged()
    {
        FindObjectOfType<MultiplayerManager>().winnerText.text = WinnerText.ToString();

        if (WinnerText != "")
        {
            foreach (ThirdPersonController thirdPersonController in FindObjectsOfType<ThirdPersonController>())
            {
                thirdPersonController.enabled = false;
            }

            Invoke(nameof(ReturnToMainScreen), 5);
        }
    }

    public void ReturnToMainScreen()
    {
        SceneManager.LoadScene(0);
    }

    private void Awake()
    {

        InitializeRoomManager();
    }


    public override void Spawned()
    {
        base.Spawned();

        OnTimeLeftChanged();
        OnWinnerTextChanged();

    }

    public void InitializeRoomManager()
    {
        InvokeRepeating(nameof(DecreaseTime), 1, 1);
    }
    public void DecreaseTime()
    {
        if (!HasStateAuthority)
        {
            return;
        }
        if (TimeLeft > 0)
        {
            TimeLeft = TimeLeft - 1;
        }
        else
        {
            CancelInvoke(nameof(DecreaseTime));
            ElectWinner();
        }
    }

    private void ElectWinner()
    {
        string winnerTeam = ExtractWinner().ToString();
        WinnerText = winnerTeam + " Team Wins";
    }

    public static ETeam ExtractWinner()
    {
        List<Character> characterList = new List<Character>(FindObjectsOfType<Character>());
        List<Character> aliveCharacters = characterList.FindAll(x => x.Hp > 0 && x.isAPlayer==true);
        int aliveRedNumber = aliveCharacters.FindAll(x => x.Team == ETeam.Red).Count;
        int aliveBlueNumber = aliveCharacters.FindAll(x => x.Team == ETeam.Blue).Count;

        ETeam winnerTeam = (aliveRedNumber > aliveBlueNumber) ? ETeam.Red : ETeam.Blue;
        return winnerTeam;
    }
}
