using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberMan : NetworkBehaviour
{
    public NetworkObject normalGranade;
    public NetworkObject specialGranade;
    public override void Spawned()
    {
        base.Spawned();
        if (HasStateAuthority)
        {

            if (PlayerConfig.playerClass == EClass.BomberMan)
            {

                GetComponentInParent<Character>().OnSpecialPowerActivate.AddListener(BomberManSpecialPower);
                GetComponentInParent<Character>().InitializeSpecialPower(1);
            }
        }
    }

    public void BomberManSpecialPower()
    {
        ((GranateWeapon)GetComponentInParent<Character>().GetHeldWeapon()).projectilePrefab = specialGranade;
        Invoke(nameof(BomberManRestoreNormalGranade), 5);
    }
    public void BomberManRestoreNormalGranade()
    {
        ((GranateWeapon)GetComponentInParent<Character>().GetHeldWeapon()).projectilePrefab = normalGranade;

    }
}
