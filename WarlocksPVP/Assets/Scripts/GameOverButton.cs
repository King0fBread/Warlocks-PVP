using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class GameOverButton : MonoBehaviour
{
    private Button _toMenuButton;
    private void OnEnable()
    {
        _toMenuButton = GetComponent<Button>();
        _toMenuButton.onClick.AddListener(() => {
            if (NetworkManager.Singleton != null)
            {
                Destroy(NetworkManager.Singleton.gameObject);
            }
            if (LobbyManager.Instance != null)
            {
                Destroy(LobbyManager.Instance.gameObject);
            }

            SceneTransitions.LoadSceneLocally(SceneTransitions.Scene.MenuScene);
        });
    }
}
