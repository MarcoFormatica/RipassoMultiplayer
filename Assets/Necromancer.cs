using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : NetworkBehaviour
{

    public NetworkObject zombiePrefab;
    public override void Spawned()
    {
        base.Spawned();
        if (HasStateAuthority)
        {

            if (PlayerConfig.playerClass == EClass.Necromancer)
            {

                GetComponentInParent<Character>().OnSpecialPowerActivate.AddListener(NecromancerSpecialPower);
                GetComponentInParent<Character>().InitializeSpecialPower(1);
            }
        }
    }

    private void NecromancerSpecialPower()
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, 5))
        {
            Character candidateCharacter = collider.GetComponent<Character>();
            if (candidateCharacter != null)
            {
                if (candidateCharacter.Hp <= 0)
                {
                    candidateCharacter.RPC_Despawn();
                    NetworkObject zombieNO = Runner.Spawn(zombiePrefab, candidateCharacter.transform.position, Quaternion.identity);
                    zombieNO.GetComponent<Character>().Team = GetComponentInParent<Character>().Team;
                }
            }
        }
    }
}
