using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerReadyUp : NetworkBehaviour
{
    public static PlayerReadyUp Instance { get; private set; }
    private Dictionary<ulong, bool> _playerReadyUpDictionary;
    private void Awake()
    {
        Instance = this;
        _playerReadyUpDictionary = new Dictionary<ulong, bool>();
    }
    public void SetPlayerAsReady()
    {
        SetPlayerAsReadyServerRpc();
    }
    [ServerRpc (RequireOwnership = false)]
    private void SetPlayerAsReadyServerRpc(ServerRpcParams serverRpcParams = default)
    {
        _playerReadyUpDictionary[serverRpcParams.Receive.SenderClientId] = true;
        bool allClientsAreReady = true;
        foreach(ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if(!_playerReadyUpDictionary.ContainsKey(clientId) || !_playerReadyUpDictionary[clientId])
            {
                allClientsAreReady = false;
                break;
            }
        }
        if (allClientsAreReady)
        {
            LobbyManager.Instance.DeleteLobby();
            SceneTransitions.LoadNetworkScene(SceneTransitions.Scene.ArenaScene);
        }
    }
}
