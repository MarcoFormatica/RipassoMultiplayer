using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessIsland : RaycastWeapon
{
    private void Awake()
    {
        OnWeaponShoot.AddListener(ChessIslandShoot);
    }

    private void ChessIslandShoot(Ray ray)
    {
        DefaultFire(ray, GetRandomDamage());
    }
}
