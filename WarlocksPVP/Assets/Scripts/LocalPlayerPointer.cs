using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class LocalPlayerPointer : NetworkBehaviour
{
    [SerializeField] private GameObject _leftPlayerPointer;
    [SerializeField] private GameObject _rightPlayerPointer;
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            _leftPlayerPointer.SetActive(true);
        }
        else
        {
            _rightPlayerPointer.SetActive(true);
        }
    }
}
