using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerRoomTransitions : MonoBehaviour
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
    private void Start()
    {
        CardsAttackExecution.Instance.OnEachPlayerHasAttacked += MoveToDeckRoom_OnEachPlayerHasAttacked;
    }

    private void MoveToDeckRoom_OnEachPlayerHasAttacked(object sender, EventArgs e)
    {
        MoveToDeckRoomAfterRoundServerRpc();
    }
    [ServerRpc (RequireOwnership = false)]
    private void MoveToDeckRoomAfterRoundServerRpc()
    {
        MoveToDeckRoomAfterRoundClientRpc();
    }
    [ClientRpc]
    private void MoveToDeckRoomAfterRoundClientRpc()
    {
        Camera playerCamera = Camera.main;
        MovePlayerToDeckRoom(playerCamera.transform);
        OnSwitchedToDeckRoom?.Invoke(this, EventArgs.Empty);
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

}
