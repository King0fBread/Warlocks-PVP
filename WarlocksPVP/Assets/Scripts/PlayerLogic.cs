using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerLogic : NetworkBehaviour
{
    private void Awake()
    {
        Debug.Log("spawned player");
    }
}
