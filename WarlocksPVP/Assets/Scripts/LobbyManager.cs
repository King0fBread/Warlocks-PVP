using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
public class LobbyManager : MonoBehaviour
{
    [SerializeField] private LobbyUI _lobbyUI;
    public static LobbyManager Instance { get; private  set; }

    public event EventHandler<OnLobbyListChangedEventArgs> OnLobbyListChanged;
    public class OnLobbyListChangedEventArgs : EventArgs
    {
        public List<Lobby> lobbyList;
    }

    private Lobby _joinedLobby;
    private float _heartbeatTimer = 20f;
    private float _listLobbiesTimer;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeUnityAuthentication();
    }
    private void Update()
    {
        HandleLobbyHeartbeat();
        HandleListingActiveLobbies();
    }

    private void HandleListingActiveLobbies()
    {
        if(_joinedLobby == null && AuthenticationService.Instance.IsSignedIn)
        {
            _listLobbiesTimer -= Time.deltaTime;
            if(_listLobbiesTimer <= 0f)
            {
                float listLobbiesTimerMax = 2f;
                _listLobbiesTimer = listLobbiesTimerMax;
                ListLobbies();
            }
        }
    }

    private void HandleLobbyHeartbeat()
    {
        if (IsLobbyHost())
        {
            _heartbeatTimer -= Time.deltaTime;
            if(_heartbeatTimer <= 0)
            {
                float maxHeartbeatTimerValue = 20f;
                _heartbeatTimer = maxHeartbeatTimerValue;

                LobbyService.Instance.SendHeartbeatPingAsync(_joinedLobby.Id);
            }
        }
    }
    private async Task<Allocation> AllocateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);
            return allocation;
        }
        catch(RelayServiceException ex)
        {
            Debug.Log(ex);
            return default;
        }
    }
    private async Task<string> GetRelayJoinCode(Allocation allocation)
    {
        try
        {
            string relayJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            return relayJoinCode;
        }
        catch(RelayServiceException ex)
        {
            Debug.Log(ex);
            return default;
        }
    }
    private async Task<JoinAllocation> JoinRelay(string joinCode)
    {
        try
        {
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
            return joinAllocation;
        }
        catch(RelayServiceException ex)
        {
            Debug.Log(ex);
            return default;
        }
    }
    private bool IsLobbyHost()
    {
        return _joinedLobby != null && _joinedLobby.HostId == AuthenticationService.Instance.PlayerId;
    }
    private async void ListLobbies()
    {
        try
        {
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions
            {
                Filters = new List<QueryFilter>
                {
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT)
                }
            };
            QueryResponse queryResponse = await LobbyService.Instance.QueryLobbiesAsync(queryLobbiesOptions);
            OnLobbyListChanged?.Invoke(this, new OnLobbyListChangedEventArgs {
                lobbyList = queryResponse.Results
            });
        }
        catch(LobbyServiceException ex)
        {
            Debug.Log(ex);
        }
    }
    private async void InitializeUnityAuthentication()
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
        {
            InitializationOptions initializationOptions = new InitializationOptions();
            initializationOptions.SetProfile(UnityEngine.Random.Range(0, 10000).ToString());

            await UnityServices.InitializeAsync(initializationOptions);
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }
    public async void CreateLobby(string lobbyName, bool isPrivate)
    {
        _lobbyUI.DisplayConnectingMessage();
        try
        {
            _joinedLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, 2, new CreateLobbyOptions
            {
                IsPrivate = isPrivate
            });
            Allocation allocation = await AllocateRelay();

            string relayJoinCode = await GetRelayJoinCode(allocation);

            await LobbyService.Instance.UpdateLobbyAsync(_joinedLobby.Id, new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject> {
                    {"RelayJoinCode", new DataObject(DataObject.VisibilityOptions.Member, relayJoinCode) }
                }
            });

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));

            NetworkManager.Singleton.StartHost();
            SceneTransitions.LoadNetworkScene(SceneTransitions.Scene.ReadyScene);
        }
        catch (LobbyServiceException ex)
        {
            Debug.Log(ex);
        }
    }
    public async void DeleteLobby()
    {
        if(_joinedLobby != null)
        {
            try
            {
                await LobbyService.Instance.DeleteLobbyAsync(_joinedLobby.Id);
                _joinedLobby = null;
            }
            catch(LobbyServiceException ex)
            {
                Debug.Log(ex);
            }
        }
    }
    public async void LeaveLobby()
    {
        if(_joinedLobby != null)
        {
            try
            {
                await LobbyService.Instance.RemovePlayerAsync(_joinedLobby.Id, AuthenticationService.Instance.PlayerId);
                _joinedLobby = null;
            }
            catch(LobbyServiceException ex)
            {
                Debug.Log(ex);
            }
        }
    }
    public async void QuickJoin()
    {
        try
        {
            _joinedLobby = await LobbyService.Instance.QuickJoinLobbyAsync();

            string relayJoinCode = _joinedLobby.Data["RelayJoinCode"].Value;
            JoinAllocation joinAllocation = await JoinRelay(relayJoinCode);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));

            _lobbyUI.DisplayConnectingMessage();

            NetworkManager.Singleton.StartClient(); 
        }
        catch (LobbyServiceException ex)
        {
            Debug.Log(ex);
        }
    }
    public async void JoinWithId(string lobbyId)
    {
        _lobbyUI.DisplayConnectingMessage();
        try
        {
            _joinedLobby = await LobbyService.Instance.JoinLobbyByIdAsync(lobbyId);

            string relayJoinCode = _joinedLobby.Data["RelayJoinCode"].Value;
            JoinAllocation joinAllocation = await JoinRelay(relayJoinCode);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));

            NetworkManager.Singleton.StartClient();
        }
        catch(LobbyServiceException ex)
        {
            Debug.Log(ex);
        }
    }
    public Lobby GetJoinedLobby()
    {
        return _joinedLobby;
    }
}
