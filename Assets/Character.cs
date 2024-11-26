using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : NetworkBehaviour
{
    internal void Fire(Ray ray)
    {
       

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
}
