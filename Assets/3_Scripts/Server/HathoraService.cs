using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Hathora.Cloud.Sdk.Api;
using Hathora.Cloud.Sdk.Client;
using Hathora.Cloud.Sdk.Model;
using Hathora.Core.Scripts.Runtime.Client.ApiWrapper;
using Hathora.Core.Scripts.Runtime.Client.Config;
using Hathora.Core.Scripts.Runtime.Client.Models;

public class HathoraService : MonoBehaviour
{
    public struct RoomConfig
    {
        public string RoomName;
        public int MaxPlayers;
    }

    public struct RoomState
    {
        public int CurrentPlayers;
    }

    private static HathoraService _singleton;

    public static HathoraService Singleton => _singleton ??= FindObjectOfType<HathoraService>();

    [SerializeField] private HathoraClientConfig _clientConfig;
    [Space]
    [SerializeField] private HathoraClientAuthApi _authApi;
    [SerializeField] private HathoraClientLobbyApi _lobbyApi;
    [SerializeField] private HathoraClientRoomApi _roomApi;

    private string _authToken;

    private bool _apiReady;

    public bool APIReady => _apiReady;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Configuration sdkConfiguration = new Configuration();

        _authApi.Init(_clientConfig, sdkConfiguration);
        _lobbyApi.Init(_clientConfig, sdkConfiguration);
        _roomApi.Init(_clientConfig, sdkConfiguration);

        _apiReady = true;
    }

    public async void Login(Action OnSuccess = null, Action OnError = null)
    {
        try
        {
            AuthResult clientAuthAsync = await _authApi.ClientAuthAsync();
            _authToken = clientAuthAsync.PlayerAuthToken;

            Debug.Log($"[{GetType()}]: Auth token set to {_authToken}");
            OnSuccess?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogError($"[{GetType()}]: Error when attempting to login");
            Debug.LogError($"{e.Message}\n{e.StackTrace}");
            OnError?.Invoke();
        }
    }

    public async void GetPublicLobbies(Action<List<Lobby>> OnSuccess = null, Action OnError = null)
    {
        try
        {
            List<Lobby> publicLobbiesAsync = await _lobbyApi.ClientListPublicLobbiesAsync();

            OnSuccess?.Invoke(publicLobbiesAsync);
        }
        catch (Exception e)
        {
            Debug.LogError($"[{GetType()}]: Error when attempting to fetch public lobbies list");
            Debug.LogError($"{e.Message}\n{e.StackTrace}");
            OnError?.Invoke();
        }
    }

    public async void CreateLobby(RoomConfig initialConfig, Action<Lobby> OnSuccess = null, Action OnError = null)
    {
        try
        {
            string initialConfigJson = JsonUtility.ToJson(initialConfig);
            Lobby lobbyAsync = await _lobbyApi.ClientCreateLobbyAsync(_authToken, CreateLobbyRequest.VisibilityEnum.Public, Region.London, initialConfigJson);

            OnSuccess?.Invoke(lobbyAsync);
        }
        catch
        {
            Debug.LogError($"[{GetType()}]: Error when attempting to create lobby");
            OnError?.Invoke();
        }
    }

    public async void GetLobbyInfo(string roomID, Action<Lobby> OnSuccess = null, Action OnError = null)
    {
        try
        {
            Lobby lobbyInfoAsync = await _lobbyApi.ClientGetLobbyInfoAsync(roomID);

            OnSuccess?.Invoke(lobbyInfoAsync);
        }
        catch
        {
            Debug.LogError($"[{GetType()}]: Error when attempting to get lobby info for room {roomID}");
            OnError?.Invoke();
        }
    }

    public async void GetConnectionInfo(string roomID, Action<ConnectionInfoV2> OnSuccess = null, Action OnTimedOut = null, Action OnError = null)
    {
        try
        {
            ConnectionInfoV2 connectionInfoAsync = await _roomApi.ClientGetConnectionInfoAsync(roomID, 2, 20);

            if (connectionInfoAsync.Status == ConnectionInfoV2.StatusEnum.Active && connectionInfoAsync.ExposedPort != null)
            {
                OnSuccess?.Invoke(connectionInfoAsync);
                return;
            }

            OnTimedOut?.Invoke();
        }
        catch
        {
            Debug.LogError($"[{GetType()}]: Error when attempting to get connection info for room {roomID}");
            OnError?.Invoke();
        }
    }
}