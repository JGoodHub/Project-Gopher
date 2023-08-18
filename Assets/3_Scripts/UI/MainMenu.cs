using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Hathora.Cloud.Sdk.Model;
using Hathora.Core.Scripts.Runtime.Client.Models;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public enum MenuPanel
    {

        Splash,
        LobbySelection,
        LobbyCreator,
        LobbyLoading,
        LobbyViewer,
        MatchLoading

    }

    [Header("Panels")]
    [SerializeField] private GameObject _splashPanel;
    [SerializeField] private GameObject _lobbySelectionPanel;
    [SerializeField] private GameObject _lobbyCreatorPanel;
    [SerializeField] private GameObject _lobbyLoadingPanel;
    [SerializeField] private GameObject _lobbyViewerPanel;

    [Header("Splash")]
    [SerializeField] private Button _startButton;
    [SerializeField] private TMP_InputField _playerNameField;

    [Header("Lobby Selection")]
    [SerializeField] private RectTransform _lobbyItemsContainer;
    [SerializeField] private GameObject _lobbyItemPrefab;
    [SerializeField] private Button _refreshLobbiesButton;
    [SerializeField] private Button _openLobbyCreatorButton;

    [Header("Lobby Creator")]
    [SerializeField] private TMP_InputField _roomNameField;
    [SerializeField] private TMP_InputField _maxPlayersField;
    [SerializeField] private Button _createLobbyButton;
    [SerializeField] private Button _backToLobbySelectionButton;

    [Header("Lobby Viewer")]
    [SerializeField] private RectTransform _playerItemsContainer;
    [SerializeField] private GameObject _playerItemPrefab;
    [SerializeField] private Button _leaveLobbyButton;
    [SerializeField] private Button _startLobbyButton;
    [SerializeField] private TextMeshProUGUI _lobbyPlayerCountText;

    private void Awake()
    {
        if (Preloader.NetworkManager == null)
            SceneManager.LoadScene("Preloader");
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetActivePanel(MenuPanel.Splash);

        _startButton.onClick.AddListener(GoToLobbySelectionPanel);

        _refreshLobbiesButton.onClick.AddListener(GoToLobbySelectionPanel);

        _openLobbyCreatorButton.onClick.AddListener(GoToLobbyCreationPanel);

        _backToLobbySelectionButton.onClick.AddListener(() => SetActivePanel(MenuPanel.LobbySelection));

        _createLobbyButton.onClick.AddListener(CreateLobby);

        Login();
    }

    private void Login()
    {
        _startButton.interactable = false;

        HathoraService.Singleton.Login(() =>
        {
            _startButton.interactable = true;
        });
    }

    private void GoToLobbySelectionPanel()
    {
        SetActivePanel(MenuPanel.LobbySelection);

        // Code here to create a lobby item for each lobby on the server
        // Pass in the JoinLobbyRoom as a callback

        foreach (Transform child in _lobbyItemsContainer)
        {
            Destroy(child.gameObject);
        }

        HathoraService.Singleton.GetPublicLobbies((lobbies =>
        {
            foreach (Lobby lobby in lobbies)
            {
                CreateLobbyItem(lobby);
            }
        }));
    }

    private void CreateLobbyItem(Lobby lobby)
    {
        LobbyRoomItem lobbyItem = Instantiate(_lobbyItemPrefab, _lobbyItemsContainer).GetComponent<LobbyRoomItem>();

        HathoraService.RoomConfig roomConfig = JsonUtility.FromJson<HathoraService.RoomConfig>(lobby.InitialConfig.ToString());

        lobbyItem.Initialise(lobby.RoomId, roomConfig.RoomName, roomConfig.MaxPlayers, JoinRoom);
    }

    private void JoinRoom(string roomID)
    {
        SetActivePanel(MenuPanel.LobbyLoading);

        HathoraService.Singleton.GetConnectionInfo(roomID, connectionInfo =>
        {
            JoinRoom(roomID, connectionInfo);
        });
    }

    private void JoinRoom(string roomID, ConnectionInfoV2 connectionInfo)
    {
        SetActivePanel(MenuPanel.LobbyLoading);

        // Code here to connect to the unity server running on the room instance
        UnityTransport unityTransport = Preloader.NetworkManager.GetComponent<UnityTransport>();

        IPAddress[] hostAddresses = Dns.GetHostAddresses(connectionInfo.ExposedPort.Host);

        if (hostAddresses.Length == 0)
        {
            Debug.LogError($"[{GetType()}]: Failed to resolve host name to ip address");
        }

        unityTransport.SetConnectionData(hostAddresses[0].ToString(), (ushort) connectionInfo.ExposedPort.Port);

        Preloader.NetworkManager.OnClientConnectedCallback += ClientConnectedToRoomServer;

        Preloader.NetworkManager.StartClient();
    }

    private void ClientConnectedToRoomServer(ulong clientID)
    {
        Debug.Log("Connector to the server");
    }

    private void GoToLobbyCreationPanel()
    {
        SetActivePanel(MenuPanel.LobbyCreator);
    }

    private void CreateLobby()
    {
        string roomName = _roomNameField.text;

        int maxPlayers = 10;

        if (int.TryParse(_maxPlayersField.text, out int inputMaxPlayers))
            maxPlayers = inputMaxPlayers;

        HathoraService.Singleton.CreateLobby(new HathoraService.RoomConfig
        {
            RoomName = roomName,
            MaxPlayers = maxPlayers
        }, lobby =>
        {
            SetActivePanel(MenuPanel.LobbyLoading);

            HathoraService.Singleton.GetConnectionInfo(lobby.RoomId, connectionInfo =>
            {
                JoinRoom(connectionInfo.RoomId, connectionInfo);
            });
        });
    }

    private void SetActivePanel(MenuPanel menuPanel)
    {
        _splashPanel.SetActive(menuPanel == MenuPanel.Splash);
        _lobbySelectionPanel.SetActive(menuPanel == MenuPanel.LobbySelection);
        _lobbyCreatorPanel.SetActive(menuPanel == MenuPanel.LobbyCreator);
        _lobbyLoadingPanel.SetActive(menuPanel == MenuPanel.LobbyLoading);
        _lobbyViewerPanel.SetActive(menuPanel == MenuPanel.LobbyViewer);
    }

    private void LoadArena()
    {
        // TODO Add code for joining a room here
        StartNetworkAsHost();
        SceneManager.LoadScene("0_Scenes/Arena"); // REQUIRES THE SCENE TO BE IN  THE BUILD FILE > BUILD SETTINGS > SCENES
    }

    private void StartNetworkAsHost()
    {
        NetworkManager.Singleton.StartHost();
    }

}