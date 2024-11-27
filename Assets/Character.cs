using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character : NetworkBehaviour
{

    [Networked, OnChangedRender(nameof(OnHpChanged))]
    public int Hp { get; set; }

    [Networked]
    public int HpMax { get; set; }

    public TextMeshPro textHp;


    internal void Fire(Ray ray)
    {
        RaycastHit raycastHit;
        if(Physics.Raycast(ray, out raycastHit))
        {
            Character hitCharacter = raycastHit.collider.gameObject.GetComponent<Character>();
            if (hitCharacter!=null)
            {
                hitCharacter.RPC_InflictDamage(10);
            }
        }

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
        OnHpChanged();
    }


    public void OnHpChanged()
    {
        if (Hp < 0)
        {
            textHp.text = "Morto";
        }
        else
        {
            textHp.text = Hp.ToString() + " / " + HpMax.ToString();
        }
    }

    [Rpc(RpcSources.All,RpcTargets.StateAuthority)]
    public void RPC_InflictDamage(int damage)
    {
        Debug.Log(nameof(RPC_InflictDamage) + " CALLED");
        Hp = Hp - damage;
    }
}
