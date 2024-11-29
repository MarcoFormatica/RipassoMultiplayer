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

    [Networked, OnChangedRender(nameof(OnPlayerNameChanged))]
    public NetworkString<_32> PlayerName { get; set; }

 

    [Networked, OnChangedRender(nameof(OnTeamChanged))]
    public ETeam Team { get; set; }


    public TextMeshPro textHp;
    public TextMeshPro textName;

    public UnityEvent OnCharacterDeath;


    public void OnPlayerNameChanged()
    {
        textName.text = PlayerName.Value;
    }
    public void OnTeamChanged()
    {
        textName.color = PlayerConfig.TeamToColor(Team);
    }

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
        if (HasStateAuthority)
        {
            Team = PlayerConfig.team;
            PlayerName = PlayerConfig.playerName;
        }
        

        OnHpChanged();
        OnTeamChanged();
        OnPlayerNameChanged();

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

        RaycastHit raycastHit;
        Vector3 hitPoint = Vector3.zero;
        if (Physics.Raycast(ray, out raycastHit))
        {
            hitPoint= raycastHit.point;
        }
        RPC_WeaponAestheticShoot(hitPoint);

        GetHeldWeapon().OnWeaponShoot.Invoke(ray);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    internal void RPC_WeaponAestheticShoot(Vector3 hitPoint)
    {
        GetHeldWeapon().WeaponPlaySound();
        GetHeldWeapon().WeaponPlayParticleEffect(hitPoint);
    }

    public Weapon GetHeldWeapon()
    {
        return GetComponentInChildren<Weapon>();
    }
}
