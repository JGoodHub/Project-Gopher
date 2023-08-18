using System;
using System.Collections;
using System.Collections.Generic;
using Hathora.Cloud.Sdk.Model;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LobbyRoomItem : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _roomNameText;
    [SerializeField] private TextMeshProUGUI _playerCountText;
    [SerializeField] private Button _joinRoomButton;

    private Lobby _lobby;
    
    public void Initialise(Lobby lobby, HathoraService.RoomConfig roomConfig, Action<Lobby> joinCallback)
    {
        _roomNameText.text = roomConfig.RoomName;

        _playerCountText.text = $"Players: 0 / {roomConfig.MaxPlayers}";

        _joinRoomButton.onClick.AddListener(() => joinCallback(lobby));
    }

}