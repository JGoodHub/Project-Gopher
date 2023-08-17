using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyRoomItem : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _roomNameText;
    [SerializeField] private TextMeshProUGUI _playerCountText;
    [SerializeField] private Button _joinRoomButton;

    public void Initialise(Action joinCallback)
    {
        _joinRoomButton.onClick.AddListener(() => joinCallback());
    }

}