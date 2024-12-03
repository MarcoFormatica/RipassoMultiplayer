using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Creature : NetworkBehaviour
{
    public float secondsBetweenAttacks;
    public float attackRadius;
    public int minDamage;
    public int maxDamage;
    public NavMeshAgent navMeshAgent;
    public Character followingCharacter;
    public override void Spawned()
    {
        base.Spawned();
        InvokeRepeating(nameof(Attack), secondsBetweenAttacks, secondsBetweenAttacks);
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (HasStateAuthority)
        {
            GetComponent<Character>().OnCharacterDeath.AddListener(OnCharacterDeathCallback);
            InvokeRepeating(nameof(SetDestination), 0, 1);
        }
        
    }

    public void SetDestination()
    {
        if(followingCharacter == null || followingCharacter.Hp <=0) 
        {
            List<Character> validCharacters = new List<Character>(FindObjectsOfType<Character>());
            validCharacters = validCharacters.FindAll(x => x.Hp > 0 && x.isAPlayer == true && x.Team != GetComponent<Character>().Team);
            if (validCharacters.Count > 0) 
            {
                validCharacters.OrderBy(x => Vector3.Distance(transform.position, x.transform.position));
                followingCharacter = validCharacters.First();
            }

        }
        if (followingCharacter != null) 
        {
            navMeshAgent.destination = followingCharacter.transform.position;
        }
    }

    private void OnCharacterDeathCallback()
    {
        if (HasStateAuthority)
        {
            Runner.Despawn(GetComponent<NetworkObject>());
        }
    }


    public void Attack()
    {
        if (HasStateAuthority == false) { return; }

        foreach (Collider collider in Physics.OverlapSphere(transform.position, attackRadius))
        {
            Character hitCharacter = collider.GetComponent<Character>();
            if (hitCharacter != null)
            {
                if (hitCharacter.Hp > 0)
                {
                    if (hitCharacter.Team != GetComponent<Character>().Team)
                    {
                        int damage = UnityEngine.Random.Range(minDamage, maxDamage + 1);
                        hitCharacter.RPC_InflictDamage(damage);
                        hitCharacter.HpChangedCallback(hitCharacter.Hp - damage);
                    }
                }
            }
        }
    }
}
