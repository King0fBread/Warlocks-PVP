using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance { get; private  set; }

    private Lobby _joinedLobby;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeUnityAuthentication();
    }
    private async void InitializeUnityAuthentication()
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
        {
            InitializationOptions initializationOptions = new InitializationOptions();
            initializationOptions.SetProfile(Random.Range(0, 10000).ToString());

            await UnityServices.InitializeAsync(initializationOptions);
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }
    public async void CreateLobby(string lobbyName, bool isPrivate)
    {
        try
        {
            _joinedLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, 2, new CreateLobbyOptions
            {
                IsPrivate = isPrivate
            });
            NetworkManager.Singleton.StartHost();
            SceneTransitions.LoadNetworkScene(SceneTransitions.Scene.ReadyScene);
        }
        catch (LobbyServiceException ex)
        {
            Debug.Log(ex);
        }
    }
    public async void QuickJoin()
    {
        try
        {
            _joinedLobby = await LobbyService.Instance.QuickJoinLobbyAsync();
            NetworkManager.Singleton.StartClient(); 
        }
        catch (LobbyServiceException ex)
        {
            Debug.Log(ex);
        }
    }
    public Lobby GetJoinedLobby()
    {
        return _joinedLobby;
    }
}
