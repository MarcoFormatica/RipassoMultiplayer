using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : NetworkBehaviour
{
    [Networked]
    public ETeam MineTeam { get; set; }
    public int minDamage;
    public int maxDamage;

    private void OnTriggerEnter(Collider other)
    {
        if(MineTeam == ETeam.None)
        {
            return;
        }
        Character enteredCharacter = other.GetComponent<Character>();
        if(enteredCharacter.Team != MineTeam ) 
        {
            enteredCharacter.RPC_InflictDamage(Random.Range(minDamage, maxDamage));
            RPC_DestroyMine();
            RPC_PerformExplosionEffects();
        }
    }


    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_DestroyMine()
    {
        Runner.Despawn(GetComponent<NetworkObject>());
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_PerformExplosionEffects()
    {
        foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
        {
            ps.Play();
        }
        GetComponent<AudioSource>()?.Play();

    }
}
