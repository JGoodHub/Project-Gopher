using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{
    private static NetworkManager _networkManager;

    public static NetworkManager NetworkManager => _networkManager;

    private static bool _isServer;

    public static bool IsServer => _isServer;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _networkManager = GetComponent<NetworkManager>();
    }

    private void Start()
    {
        Dictionary<string, string> args = GetCommandlineArgs();

        if (args.TryGetValue("-mode", out string mode) && mode == "server")
        {
            Debug.Log($"[{GetType()}]: Starting application in server mode");
            _networkManager.StartServer();
            _isServer = true;
            return;
        }

        Debug.Log($"[{GetType()}]: Waiting to starting application in client mode");
    }

    private static Dictionary<string, string> GetCommandlineArgs()
    {
        Dictionary<string, string> argDictionary = new Dictionary<string, string>();

        string[] args = Environment.GetCommandLineArgs();

        for (int i = 0; i < args.Length; ++i)
        {
            string arg = args[i].ToLower();
            if (arg.StartsWith("-"))
            {
                string value = i < args.Length - 1 ? args[i + 1].ToLower() : null;
                value = (value?.StartsWith("-") ?? false) ? null : value;

                argDictionary.Add(arg, value);
            }
        }

        return argDictionary;
    }
}