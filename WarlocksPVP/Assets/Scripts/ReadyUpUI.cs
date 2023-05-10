using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class ReadyUpUI : NetworkBehaviour
{
    [SerializeField] private Button _readyButton;
    [SerializeField] private TextMeshProUGUI _readyUpSceneText;
    private void Awake()
    {
        _readyButton.onClick.AddListener(() => PlayerReadyUp.Instance.SetPlayerAsReady());
    }
    private void Start()
    {
        //_readyButton.gameObject.SetActive(false);
        //_readyUpSceneText.text = "Waiting for your opponent to connect";
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
        _readyButton.gameObject.SetActive(true);
        _readyUpSceneText.text = "Waiting for everyoone to ready up";
    }
    [ClientRpc]
    private void DisableReadyUIClientRpc()
    {
        _readyUpSceneText.text = "Waiting for your opponent to connect";
        _readyButton.gameObject.SetActive(false);
    }
}
