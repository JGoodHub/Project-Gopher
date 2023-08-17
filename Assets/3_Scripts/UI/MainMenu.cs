using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Button _playButton;

    // Start is called before the first frame update
    private void Start()
    {
        _playButton.onClick.AddListener(JoinMultiplayerGame);
    }

    private void JoinMultiplayerGame()
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