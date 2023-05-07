using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class TestingLobbyUI : MonoBehaviour
{
    [SerializeField] private Button _createLobbyButton;
    [SerializeField] private Button _joinLobbyButton;
    private void Awake()
    {
        _createLobbyButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
            SceneTransitions.LoadNetworkScene(SceneTransitions.Scene.ArenaScene); 
        });
        _joinLobbyButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
            SceneTransitions.LoadNetworkScene(SceneTransitions.Scene.ArenaScene);
        });
    }
}
