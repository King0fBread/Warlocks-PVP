using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyButtonAssigner : MonoBehaviour
{
    [SerializeField] private Button _menuButton;
    private void Awake()
    {
        _menuButton.onClick.AddListener(() => LobbyManager.Instance.LeaveLobby());
    }
}
