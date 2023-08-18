using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LobbyRoomItem : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _roomNameText;
    [SerializeField] private TextMeshProUGUI _playerCountText;
    [SerializeField] private Button _joinRoomButton;

    public void Initialise(string roomID, string roomName, int maxPlayers, Action<string> joinCallback)
    {
        _roomNameText.text = roomName;

        _playerCountText.text = $"Players: 0 / {maxPlayers}";

        _joinRoomButton.onClick.AddListener(() => joinCallback(roomID));
    }

}