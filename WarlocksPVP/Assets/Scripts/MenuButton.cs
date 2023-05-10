using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    private Button _toMenuButton;
    private void OnEnable()
    {
        _toMenuButton = GetComponent<Button>();
        _toMenuButton.onClick.AddListener(() => {
            SceneTransitions.LoadSceneLocally(SceneTransitions.Scene.LobbyScene);
        });
    }
}
