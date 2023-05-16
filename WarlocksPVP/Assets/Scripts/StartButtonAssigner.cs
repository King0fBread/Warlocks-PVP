using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonAssigner : MonoBehaviour
{
    private Button _startButton;
    private void Awake()
    {
        _startButton = GetComponent<Button>();
        _startButton.onClick.AddListener(() => SceneTransitions.LoadSceneLocally(SceneTransitions.Scene.LobbyScene));
    }
}
