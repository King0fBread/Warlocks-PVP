using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public static class SceneTransitions
{
    public enum Scene{
        LobbyScene,
        ReadyScene,
        ArenaScene
    }
    public static void LoadNetworkScene(Scene scene)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
    }
}
