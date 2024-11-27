using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public UnityEvent<Ray> OnWeaponShoot;
    public int minDamage;
    public int maxDamage;
    
    public int GetRandomDamage()
    {
        return Random.Range(minDamage, maxDamage+1);
    }

    public void WeaponPlaySound()
    {
        GetComponent<AudioSource>().Play();
    }

    public void DefaultFire(Ray ray, int damage)
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit))
        {
            Character hitCharacter = raycastHit.collider.gameObject.GetComponent<Character>();
            if (hitCharacter != null)
            {
                hitCharacter.RPC_InflictDamage(damage);
                hitCharacter.HpChangedCallback(hitCharacter.Hp - damage);
            }
        }

    }
}
