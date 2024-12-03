using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : NetworkBehaviour
{
    public float timeToLive;
    public float radius;
    public int minDamage;
    public int maxDamage;
    public GameObject explosionFxPrefab;

    public override void Spawned()
    {
        base.Spawned();
        if (HasStateAuthority)
        {

            Invoke(nameof(Explode), timeToLive);
        }
    }

    public void Explode()
    {
        foreach(Collider collider in Physics.OverlapSphere(transform.position, radius))
        {
            Character character = collider.GetComponent<Character>();
            if(character != null)
            {
                if (character.Hp > 0)
                {
                    int damage = Random.Range(minDamage, maxDamage + 1);
                    character.RPC_InflictDamage(damage);
                    character.HpChangedCallback(character.Hp - damage);
                }
            }
        }
        RPC_ExplosionAesthetic();
        Runner.Despawn(GetComponent<NetworkObject>());
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    internal void RPC_ExplosionAesthetic()
    {
      GameObject explosionFx =   Instantiate(explosionFxPrefab, transform.position, Quaternion.identity);
      Destroy(explosionFx, 5);
    }

}
