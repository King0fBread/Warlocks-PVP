using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class ReadyUpUI: NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI _readyUpSceneText;
    [SerializeField] private Button _readyButton;
    [SerializeField] private Button _backButton;

    [SerializeField] private Button _helpPanelButton;
    [SerializeField] private Image _helpPanel;
    private void Awake()
    {
        _backButton.onClick.AddListener(() =>
        {
            LobbyManager.Instance.LeaveLobby();
        });
    }
    private void Update()
    {
        if (!IsServer)
            return;

        if(NetworkManager.Singleton.ConnectedClientsIds.Count == 2 && !_readyButton.gameObject.activeSelf)
        {
            EnableReadyUIClientRpc();
        }
        else if(NetworkManager.Singleton.ConnectedClientsIds.Count < 2 && _readyButton.gameObject.activeSelf)
        {
            DisableReadyUIClientRpc();
        }
    }
    [ClientRpc]
    private void EnableReadyUIClientRpc()
    {
        _helpPanelButton.gameObject.SetActive(false);
        _helpPanel.gameObject.SetActive(false);

        _readyButton.gameObject.SetActive(true);
        _readyButton.onClick.AddListener(() => PlayerReadyUp.Instance.SetPlayerAsReady());
        _readyUpSceneText.text = "Waiting for everyoone to ready up";
    }
    [ClientRpc]
    private void DisableReadyUIClientRpc()
    {
        _helpPanelButton.gameObject.SetActive(true);

        _readyButton.gameObject.SetActive(false);
        _readyUpSceneText.text = "Waiting for your opponent to connect";
    }
}
