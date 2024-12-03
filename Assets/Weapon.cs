using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public UnityEvent<Ray> OnWeaponShoot;


    private void Awake()
    {

    }
    public void WeaponPlayParticleEffect(Vector3 hitPoint)
    {

        foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
        {
            if(hitPoint != Vector3.zero)
            {

                ps.transform.forward = (hitPoint - ps.transform.position);
            }
            ps.Play();
        }
    }


    public void WeaponPlaySound()
    {
        GetComponent<AudioSource>().Play();
    }

}
