using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : NetworkBehaviour
{
    public override void Spawned()
    {
        base.Spawned();
        if (HasStateAuthority)
        {
            if (PlayerConfig.playerClass == EClass.Priest)
            {

                GetComponentInParent<Character>().OnSpecialPowerActivate.AddListener(PriestSpecialPower);
                GetComponentInParent<Character>().InitializeSpecialPower(1);
            }
        }
    }

    private void PriestSpecialPower()
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, 2))
        {
            Character character = collider.GetComponent<Character>();
            if (character != null)
            {
                character.RPC_SetTeam(GetComponentInParent<Character>().Team);
            }
        }
    }
}
