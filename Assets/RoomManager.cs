using Fusion;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
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
        List<Character> characterList = new List<Character>(FindObjectsOfType<Character>());    
        List<Character> aliveCharacters = characterList.FindAll(x => x.Hp > 0);
        int aliveRedNumber = aliveCharacters.FindAll(x => x.Team == ETeam.Red).Count;
        int aliveBlueNumber = aliveCharacters.FindAll(x => x.Team == ETeam.Blue).Count;
        string winnerTeam = (aliveRedNumber > aliveBlueNumber) ? "Red" : "Blue";
        WinnerText = winnerTeam + " Team Wins";
    }
}
