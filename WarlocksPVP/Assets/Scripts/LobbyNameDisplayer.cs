using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;

public class LobbyNameDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lobbyNameText;
    private void Start()
    {
        Lobby joinedLobby = LobbyManager.Instance.GetJoinedLobby();
        _lobbyNameText.text = "In lobby: " + joinedLobby.Name;
    }
}
