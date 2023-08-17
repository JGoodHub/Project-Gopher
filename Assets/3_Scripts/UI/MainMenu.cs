using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject _playPanel;
    [SerializeField] private GameObject _lobbySelectionPanel;
    [SerializeField] private GameObject _createLobbyPanel;
    [SerializeField] private GameObject _lobbyRoomPanel;

    [Header("Buttons")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _createLobbyButton;
    [SerializeField] private Button _leaveLobbyButton;
    [SerializeField] private Button _backToLobbySelectionButton;

    [Header("Prefabs")]
    [SerializeField] private GameObject _lobbyItemPrefab;
    [SerializeField] private GameObject _playerItemPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        _playButton.onClick.AddListener(ShowLobbySelectionPanel);
        _createLobbyButton.onClick.AddListener(CreateNewLobby);
    }

    private void ShowLobbySelectionPanel()
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

    private void CreateNewLobby()
    {
        SetActivePanel(MenuPanel.CreateLobby);
    }

    private void SetActivePanel(MenuPanel menuPanel)
    {
        _playPanel.SetActive(menuPanel == MenuPanel.Play);
        _lobbySelectionPanel.SetActive(menuPanel == MenuPanel.LobbySelection);
        _createLobbyPanel.SetActive(menuPanel == MenuPanel.CreateLobby);
        _lobbyRoomPanel.SetActive(menuPanel == MenuPanel.RoomViewer);
    }

    private void LoadArena()
    {
        // TODO Add code for joining a room here
        SceneManager.LoadScene("0_Scenes/Arena"); // REQUIRES THE SCENE TO BE IN  THE BUILD FILE > BUILD SETTINGS > SCENES
    }

}