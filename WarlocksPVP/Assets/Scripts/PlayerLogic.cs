using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerLogic : NetworkBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        Debug.Log("spawned player");
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void OnNetworkSpawn()
    {
        transform.position = PlayerVisualsAssigner.Instance.GetTransformByID((int)OwnerClientId).position;
        _spriteRenderer.sprite = PlayerVisualsAssigner.Instance.GetSpriteByID((int)OwnerClientId);
        if (!IsServer)
        {
            MovePlayersToDeckRoomServerRpc();
        }
    }
    //private void Update()
    //{
    //    if (!IsServer)
    //    {
    //        return;
    //    }

    //}
    [ServerRpc (RequireOwnership = false)]
    private void MovePlayersToDeckRoomServerRpc()
    {
        MovePlayersToDeckRoomClientRpc();
    }
    [ClientRpc]
    private void MovePlayersToDeckRoomClientRpc()
    {
        Camera playerCamera = Camera.main;
        PlayerRoomTransitions.Instance.MovePlayerToDeckRoom(playerCamera.transform);
    }
}