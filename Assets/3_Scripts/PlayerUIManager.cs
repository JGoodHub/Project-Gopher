using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class PlayerUIManager : NetworkBehaviour
{
    private static PlayerUIManager instance;
    [SerializeField] private bool _startGameAsClient;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    void Update()
    {
        StartGameAsClient();
    }
    
    private void StartGameAsClient()
    {
        if (_startGameAsClient)
        {
            _startGameAsClient = false;
            NetworkManager.Singleton.Shutdown();
            NetworkManager.Singleton.StartClient();
        }
    }
    
    
}
