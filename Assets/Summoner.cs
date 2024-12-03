using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner :   NetworkBehaviour
{
    public NetworkObject creaturePrefab;
    public override void Spawned()
    {
        base.Spawned();
        if (HasStateAuthority)
        {

            if (PlayerConfig.playerClass == EClass.Summoner)
            {

                GetComponentInParent<Character>().OnSpecialPowerActivate.AddListener(SummonerSpecialPower);
                GetComponentInParent<Character>().InitializeSpecialPower(1);
            }
        }
    }

    private void SummonerSpecialPower()
    {
        NetworkObject creatureNO = Runner.Spawn(creaturePrefab, transform.position + transform.forward, Quaternion.identity);
        creatureNO.GetComponent<Character>().Team = GetComponentInParent<Character>().Team;
    }
}
