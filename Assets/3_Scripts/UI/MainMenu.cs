using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

}