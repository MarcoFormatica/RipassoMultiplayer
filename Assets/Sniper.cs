using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : NetworkBehaviour
{
    public override void Spawned()
    {
        base.Spawned();
        if (HasStateAuthority)
        {
            if (PlayerConfig.playerClass == EClass.Sniper)
            {

                GetComponentInParent<Character>().OnSpecialPowerActivate.AddListener(SniperSpecialPower);
                GetComponentInParent<Character>().InitializeSpecialPower(20);
            }
        }
    }


    private void SniperSpecialPower()
    {
        ((RaycastWeapon) GetComponentInParent<Character>().GetHeldWeapon()).minDamage *= 3;
        ((RaycastWeapon)GetComponentInParent<Character>().GetHeldWeapon()).maxDamage *= 5;
        Invoke(nameof(RestoreWeaponNormalStat), 5);
    }

    public void RestoreWeaponNormalStat()
    {
        ((RaycastWeapon)GetComponentInParent<Character>().GetHeldWeapon()).minDamage /= 3;
        ((RaycastWeapon)GetComponentInParent<Character>().GetHeldWeapon()).maxDamage /= 5;

    }

}
