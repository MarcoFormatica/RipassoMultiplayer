using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranateWeapon : ProjectileWeapon
{
    private void Awake()
    {
        OnWeaponShoot.AddListener(ShootProjectile);
    }
}
