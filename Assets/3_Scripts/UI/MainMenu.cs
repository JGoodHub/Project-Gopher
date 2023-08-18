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
    [SerializeField] private TextMeshProUGUI _roomNameText;
    [SerializeField] private RectTransform _playerItemsContainer;
    [SerializeField] private GameObject _playerItemPrefab;
    [SerializeField] private Button _leaveLobbyButton;
    [SerializeField] private Button _startLobbyButton;
    [SerializeField] private TextMeshProUGUI _lobbyPlayerCountText;

    private Lobby _currentLobby;
    private ConnectionInfoV2 _currentConnectionInfo;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        if (ServerHandler.RunningAsServer)
            yield break;

        yield return new WaitUntil(() => HathoraService.Singleton.APIReady);

        SetActivePanel(MenuPanel.Splash);

        _startButton.onClick.AddListener(GoToLobbySelectionPanel);

        _refreshLobbiesButton.onClick.AddListener(GoToLobbySelectionPanel);

        _openLobbyCreatorButton.onClick.AddListener(GoToLobbyCreationPanel);

        _backToLobbySelectionButton.onClick.AddListener(() => SetActivePanel(MenuPanel.LobbySelection));

        _createLobbyButton.onClick.AddListener(CreateLobby);

        _startLobbyButton.onClick.AddListener(StartCurrentLobbyMatch);

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

        lobbyItem.Initialise(lobby, roomConfig, JoinRoom);
    }

    private void JoinRoom(Lobby lobby)
    {
        SetActivePanel(MenuPanel.LobbyLoading);

        HathoraService.Singleton.GetConnectionInfo(lobby.RoomId, connectionInfo =>
        {
            JoinRoom(lobby, connectionInfo);
        });
    }

    private void JoinRoom(Lobby lobby, ConnectionInfoV2 connectionInfo)
    {
        SetActivePanel(MenuPanel.LobbyLoading);

        _currentLobby = lobby;
        _currentConnectionInfo = connectionInfo;

        ServerHandler.Singleton.SetupAsClient(connectionInfo.ExposedPort.Host, (ushort) connectionInfo.ExposedPort.Port, ClientConnectedToRoomServer);
    }

    private void ClientConnectedToRoomServer(ulong clientID)
    {
        Debug.Log("Connector to the server");

        SetActivePanel(MenuPanel.LobbyViewer);

        HathoraService.RoomConfig roomConfig = JsonUtility.FromJson<HathoraService.RoomConfig>(_currentLobby.InitialConfig.ToString());

        _roomNameText.text = roomConfig.RoomName;
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
                JoinRoom(lobby, connectionInfo);
            });
        });
    }

    private void StartCurrentLobbyMatch()
    {
        Debug.Log("Starting match clicked");
        ServerHandler.Singleton.StartMatchServerRpc();
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