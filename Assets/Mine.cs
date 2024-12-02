using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnTeamMineChanged))]
    public ETeam MineTeam { get; set; }

    public GameObject explosionPrefab;
    public int minDamage;
    public int maxDamage;

    public override void Spawned()
    {
        base.Spawned();
        OnTeamMineChanged();
    }

    public void OnTeamMineChanged()
    {
        GetComponent<MeshRenderer>().material.color = PlayerConfig.TeamToColor(MineTeam);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(MineTeam == ETeam.None)
        {
            return;
        }
        Character enteredCharacter = other.GetComponent<Character>();
        if(enteredCharacter.Team != MineTeam ) 
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            int damage = Random.Range(minDamage, maxDamage);
            enteredCharacter.RPC_InflictDamage(damage);
            RPC_PerformExplosionEffects();
            RPC_DestroyMine();
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
        GameObject explosionGO = Instantiate(explosionPrefab,transform.position,Quaternion.identity);

        foreach (ParticleSystem ps in explosionGO.GetComponentsInChildren<ParticleSystem>())
        {
            ps.Play();
        }

        Destroy(explosionGO, 5);

    }
}
