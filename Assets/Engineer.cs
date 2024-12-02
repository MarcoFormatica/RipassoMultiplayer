using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer : NetworkBehaviour
{
    public NetworkObject minePrefab;

    public override void Spawned()
    {
        base.Spawned();
        if (HasStateAuthority)
        {
            if (PlayerConfig.playerClass == EClass.Engineer)
            {

                GetComponentInParent<Character>().OnSpecialPowerActivate.AddListener(EngineerSpecialPower);
                GetComponentInParent<Character>().InitializeSpecialPower(3);
            }
        }
    }

    private void EngineerSpecialPower()
    {
      NetworkObject mineGO =   Runner.Spawn(minePrefab, transform.position, Quaternion.identity);
        mineGO.GetComponent<Mine>().MineTeam = GetComponentInParent<Character>().Team;
    }
}
