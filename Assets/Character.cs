using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Character : NetworkBehaviour
{


    [Networked, OnChangedRender(nameof(OnHpChanged))]
    public int Hp { get; set; }

    [Networked]
    public int HpMax { get; set; }



    public TextMeshPro textHp;

    public UnityEvent OnCharacterDeath;





    private void OnTriggerEnter(Collider other)
    {
        MagicCube magicCube = other.GetComponent<MagicCube>();
        if(magicCube != null)
        {
            if (magicCube.GetComponent<NetworkObject>().HasStateAuthority)
            {
               Runner.Despawn(magicCube.GetComponent<NetworkObject>());
            }
            else
            {
                magicCube.gameObject.SetActive(false);
            }
        }
    }

    public override void Spawned()
    {
        base.Spawned();

        OnHpChanged();
    }


    public void OnHpChanged()
    {
        HpChangedCallback(Hp);
    }

    public void HpChangedCallback(int hp)
    {
        if (hp <= 0)
        {
            textHp.text = "";
            OnCharacterDeath.Invoke();
        }
        else
        {
            textHp.text = hp.ToString() + " / " + HpMax.ToString();
        }
    }

    [Rpc(RpcSources.All,RpcTargets.StateAuthority)]
    public void RPC_InflictDamage(int damage)
    {
        Debug.Log(nameof(RPC_InflictDamage) + " CALLED");
        Hp = Hp - damage;
    }

    internal void CharacterFire(Ray ray)
    {
        RPC_WeaponAestheticShoot();

        GetHeldWeapon().OnWeaponShoot.Invoke(ray);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    internal void RPC_WeaponAestheticShoot()
    {
        GetHeldWeapon().WeaponPlaySound();
        GetHeldWeapon().WeaponPlayParticleEffect();
    }

    public Weapon GetHeldWeapon()
    {
        return GetComponentInChildren<Weapon>();
    }
}
