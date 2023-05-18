using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies.Models;
using TMPro;

public class SingleLobbyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lobbyNameText;
    private Lobby lobby;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            LobbyManager.Instance.JoinWithId(lobby.Id);
        });
    }
    public void SetLobbyInfo(Lobby lobby)
    {
        this.lobby = lobby;
        _lobbyNameText.text = lobby.Name;
    }
}
