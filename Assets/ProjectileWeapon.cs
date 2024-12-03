using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public NetworkObject projectilePrefab;
    public float ejectionSpeed;


    public void ShootProjectile(Ray ray) 
    {
       NetworkObject projectile = GetComponentInParent<Character>().Runner.Spawn(projectilePrefab, GetComponentInChildren<SpawnProjectilePoint>().gameObject.transform.position, Quaternion.identity); ;
       projectile.GetComponent<Rigidbody>().AddForce(ray.direction * ejectionSpeed);
    }
}
