using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : NetworkBehaviour
{
    public override void Spawned()
    {
        base.Spawned();
        if (HasStateAuthority)
        {
            if (PlayerConfig.playerClass == EClass.Healer)
            {

                GetComponentInParent<Character>().OnSpecialPowerActivate.AddListener(HealerSpecialPower);
                GetComponentInParent<Character>().InitializeSpecialPower(2);
            }
        }
    }

    private void HealerSpecialPower()
    {
        foreach(Collider collider in Physics.OverlapSphere(transform.position, 2))
        {
            Character character = collider.gameObject.GetComponent<Character>();
            if (character != null)
            {
                if (character.Team == GetComponentInParent<Character>().Team && character.Hp > 0)
                {

                    character.RPC_InflictDamage(-10);
                }
            }

            
        }
    }
}
