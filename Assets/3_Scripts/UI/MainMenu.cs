using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public enum MenuPanel
    {

        Play,
        LobbySelection,
        CreateLobby,
        RoomViewer,
        LoadingMatch

    }

    [Header("Panels")]
    [SerializeField] private GameObject _splashPanel;
    [SerializeField] private GameObject _lobbySelectionPanel;
    [SerializeField] private GameObject _lobbyCreatorPanel;
    [SerializeField] private GameObject _lobbyViewerPanel;

    [Header("Splash")]
    [SerializeField] private Button _startButton;

    [Header("Lobby Selection")]
    [SerializeField] private RectTransform _lobbyItemsContainer;
    [SerializeField] private GameObject _lobbyItemPrefab;
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

    // Start is called before the first frame update
    private void Start()
    {
        _startButton.onClick.AddListener(GoToLobbySelectionPanel);
        _openLobbyCreatorButton.onClick.AddListener(GetToLobbyCreationPanel);
    }

    private void GoToLobbySelectionPanel()
    {
        SetActivePanel(MenuPanel.LobbySelection);

        // Code here to create a lobby item for each lobby on the server
        // Pass in the JoinLobbyRoom as a callback
    }

    private void JoinRoom()
    {
        SetActivePanel(MenuPanel.RoomViewer);

        // Code here to create a player item for each person in the lobby
    }

    private void GetToLobbyCreationPanel()
    {
        SetActivePanel(MenuPanel.CreateLobby);
    }

    private void SetActivePanel(MenuPanel menuPanel)
    {
        _splashPanel.SetActive(menuPanel == MenuPanel.Play);
        _lobbySelectionPanel.SetActive(menuPanel == MenuPanel.LobbySelection);
        _lobbyCreatorPanel.SetActive(menuPanel == MenuPanel.CreateLobby);
        _lobbyViewerPanel.SetActive(menuPanel == MenuPanel.RoomViewer);
    }

    private void LoadArena()
    {
        // TODO Add code for joining a room here
        SceneManager.LoadScene("0_Scenes/Arena"); // REQUIRES THE SCENE TO BE IN  THE BUILD FILE > BUILD SETTINGS > SCENES
    }

}