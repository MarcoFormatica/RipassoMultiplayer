using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MagicCube magicCube = other.GetComponent<MagicCube>();
        if(magicCube != null)
        {
            if (Runner.IsSharedModeMasterClient && HasStateAuthority)
            {
                Runner.Despawn(magicCube.GetComponent<NetworkObject>());
            }
        }
    }
}
