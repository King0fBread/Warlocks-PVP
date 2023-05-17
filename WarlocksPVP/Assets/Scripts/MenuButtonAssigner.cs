using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class MenuButtonAssigner : NetworkBehaviour
{
    [SerializeField] private Button _menuButton;
    public override void OnNetworkSpawn()
    {
        _menuButton.onClick.AddListener(() => 
        {
            if (!IsServer)
            {
                LoadMenuSceneServerRpc();
            }
            else
            {
                LobbyManager.Instance.DeleteLobby();
                SceneTransitions.LoadNetworkScene(SceneTransitions.Scene.MenuScene);
            }
        });
    }
    [ServerRpc (RequireOwnership = false)]
    private void LoadMenuSceneServerRpc()
    {
        LobbyManager.Instance.DeleteLobby();
        SceneTransitions.LoadNetworkScene(SceneTransitions.Scene.MenuScene);
    }
}
