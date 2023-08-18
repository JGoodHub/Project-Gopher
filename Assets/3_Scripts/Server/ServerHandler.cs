using System;
using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ServerHandler : NetworkBehaviour
{
    private static ServerHandler _singleton;

    public static ServerHandler Singleton => _singleton;

    private static bool _runningAsServer;

    public static bool RunningAsServer => _runningAsServer;

    private Action<ulong> _onConnectedCallback;

    private void Awake()
    {
        _singleton = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetupAsServer()
    {
        Debug.Log($"[{GetType()}]: Starting application in server mode");
        NetworkManager.StartServer();
        _runningAsServer = true;
    }

    public void SetupAsClient(string host, ushort port, Action<ulong> onConnectedCallback)
    {
        UnityTransport unityTransport = NetworkManager.GetComponent<UnityTransport>();

        IPAddress[] hostAddresses = Dns.GetHostAddresses(host);

        if (hostAddresses.Length == 0)
        {
            Debug.LogError($"[{GetType()}]: Failed to resolve host name to ip address");
        }

        unityTransport.SetConnectionData(hostAddresses[0].ToString(), port);

        NetworkManager.OnClientConnectedCallback += ClientConnectedToRoomServer;

        _onConnectedCallback = onConnectedCallback;

        NetworkManager.StartClient();
    }

    private void ClientConnectedToRoomServer(ulong clientID)
    {
        NetworkManager.OnClientConnectedCallback -= ClientConnectedToRoomServer;

        _onConnectedCallback?.Invoke(clientID);
    }


    [ServerRpc(RequireOwnership = false)]
    public void StartMatchServerRpc()
    {
        Debug.Log("Starting match, Loading arena scene");
        NetworkManager.SceneManager.LoadScene("Arena", LoadSceneMode.Single);
    }
}