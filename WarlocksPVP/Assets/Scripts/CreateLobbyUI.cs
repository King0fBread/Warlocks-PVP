using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateLobbyUI : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _createPublicLobbyButton;
    [SerializeField] private TMP_InputField _lobbyNameInputField;
    [SerializeField] private TextMeshProUGUI _emptyNameExceptionText;

    private void Awake()
    {
        _createPublicLobbyButton.onClick.AddListener(()=> {
            TryCreateLobby();
        });
        _backButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
        });
    }
    private void TryCreateLobby()
    {
        if(_lobbyNameInputField.text != "")
        {
            LobbyManager.Instance.CreateLobby(_lobbyNameInputField.text, false);
        }
        else
        {
            _emptyNameExceptionText.gameObject.SetActive(true);
        }
    }
}
