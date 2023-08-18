using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkManager))]
[DisallowMultipleComponent]
public class ServerStarter : MonoBehaviour
{

    private NetworkManager _networkManager;

    private void Start()
    {
        _networkManager = GetComponent<NetworkManager>();

        // TODO Set the address and port of the server

        _networkManager.StartServer();
    }

}