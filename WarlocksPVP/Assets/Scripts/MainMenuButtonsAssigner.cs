using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonsAssigner : MonoBehaviour
{
    public void StartButton()
    {
        SceneTransitions.LoadSceneLocally(SceneTransitions.Scene.LobbyScene);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
