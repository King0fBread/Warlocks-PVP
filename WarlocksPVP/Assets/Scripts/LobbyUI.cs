using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private Button _createLobbyButton;
    [SerializeField] private Button _quickJoinButton;
    [SerializeField] private CreateLobbyUI _createLobbyUI;
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
    }
}
