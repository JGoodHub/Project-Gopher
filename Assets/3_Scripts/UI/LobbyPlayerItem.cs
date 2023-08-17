using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyPlayerItem : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _playerNameText;

    public void Initialise(string playerName)
    {
        _playerNameText.text = playerName;
    }

}