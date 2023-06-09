using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerRoomTransitions : NetworkBehaviour
{
    public event EventHandler OnSwitchedToArenaRoom;
    public event EventHandler OnSwitchedToDeckRoom;

    [SerializeField] private Transform _arenaRoom;
    [SerializeField] private Transform _deckSelectionRoom;
    public static PlayerRoomTransitions Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void MovePlayerToArenaRoom(Transform cameraTransform)
    {
        cameraTransform.position = _arenaRoom.position;
        OnSwitchedToArenaRoom?.Invoke(this, EventArgs.Empty);
    }
    public void MovePlayerToDeckRoom(Transform cameraTransfrom)
    {
        cameraTransfrom.position = _deckSelectionRoom.position;
        OnSwitchedToDeckRoom?.Invoke(this, EventArgs.Empty);
    }
    [ServerRpc (RequireOwnership = false)]
    public void MoveBothPlayersToDeckRoomServerRpc()
    {
        MoveBothPlayersToDeckRoomClientRpc();
    }
    [ClientRpc]
    public void MoveBothPlayersToDeckRoomClientRpc()
    {
        MovePlayerToDeckRoom(Camera.main.transform);
    }

}
