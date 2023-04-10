using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TestingNetcodeUI : MonoBehaviour
{
    [SerializeField] private Button _hostButton;
    [SerializeField] private Button _clientButton;

    private void Awake()
    {
        _hostButton.onClick.AddListener(() => {
            Debug.Log("starting host");
            NetworkManager.Singleton.StartHost();
            Hide();
        });

        _clientButton.onClick.AddListener(() =>
        {
            Debug.Log("starting client");
            NetworkManager.Singleton.StartClient();
            Hide();
        });
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
