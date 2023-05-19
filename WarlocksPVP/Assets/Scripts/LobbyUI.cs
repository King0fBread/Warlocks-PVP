using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies.Models;
public class LobbyUI : MonoBehaviour
{
    [SerializeField] private Button _createLobbyButton;
    [SerializeField] private Button _quickJoinButton;
    [SerializeField] private CreateLobbyUI _createLobbyUI;

    [SerializeField] private Transform _lobbyContainer;
    [SerializeField] private Transform _lobbyTemplate;

    [SerializeField] private Transform _connectingToLobbyMessageObject;
    private void Awake()
    {
        _createLobbyButton.onClick.AddListener(() => 
        {
            _createLobbyUI.gameObject.SetActive(true);
        });
        _quickJoinButton.onClick.AddListener(() =>
        {
            LobbyManager.Instance.QuickJoin();
        });

        _lobbyTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        LobbyManager.Instance.OnLobbyListChanged += LobbyManager_OnLobbyListChanged;
        UpdateLobbyList(new List<Lobby>());
    }

    private void LobbyManager_OnLobbyListChanged(object sender, LobbyManager.OnLobbyListChangedEventArgs e)
    {
        UpdateLobbyList(e.lobbyList);
    }
    private void UpdateLobbyList(List<Lobby> lobbiesList)
    {
        foreach(Transform child in _lobbyContainer)
        {
            if (child == _lobbyTemplate)
                continue;
            Destroy(child.gameObject);
        }

        foreach(Lobby lobby in lobbiesList)
        {
            Transform lobbyTransform = Instantiate(_lobbyTemplate, _lobbyContainer);
            lobbyTransform.gameObject.SetActive(true);
            lobbyTransform.GetComponent<SingleLobbyUI>().SetLobbyInfo(lobby);
        }
    }
    public void DisplayConnectingMessage()
    {
        _connectingToLobbyMessageObject.gameObject.SetActive(true);
    }
}
