using System;
using System.Net;
using Hathora.Cloud.Sdk.Model;
using Unity.Mathematics;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerHandler : NetworkBehaviour
{
    [Serializable]
    public class RoomState
    {
        public int CurrentPlayers;
    }

    private static ServerHandler _singleton;

    public static ServerHandler Singleton => _singleton;

    [SerializeField] private GameObject _playerPrefab;

    private static bool _runningAsServer;
    private RoomState _roomState;

    private Action<ulong> _connectionEstablishedToServerCallback;

    public static bool RunningAsServer => _runningAsServer;

    private void Awake()
    {
        _singleton = this;
        DontDestroyOnLoad(gameObject);
    }

    #region Server code

    public void SetupAsServer()
    {
        _runningAsServer = true;
        _roomState = new RoomState();

        Debug.Log($"[{GetType()}]: Starting application in server mode");
        NetworkManager.StartServer();

        NetworkManager.OnClientConnectedCallback += ClientConnectedToServer;
    }

    private void ClientConnectedToServer(ulong clientID)
    {
        // Update the room state to include the new player
        Debug.Log($"[{GetType()}]: Client connected to server {clientID}");

        SetLobbyStateRequest setLobbyStateRequest = new SetLobbyStateRequest(JsonUtility.ToJson(_roomState));
        
        HathoraService.Singleton.SetLobbyState();
    }

    #endregion

    #region Client code

    public void SetupAsClient(string host, ushort port, Action<ulong> connectionEstablishedToServerCallback)
    {
        UnityTransport unityTransport = NetworkManager.GetComponent<UnityTransport>();

        IPAddress[] hostAddresses = Dns.GetHostAddresses(host);

        if (hostAddresses.Length == 0)
        {
            Debug.LogError($"[{GetType()}]: Failed to resolve host name to ip address");
        }

        unityTransport.SetConnectionData(hostAddresses[0].ToString(), port);

        _connectionEstablishedToServerCallback = connectionEstablishedToServerCallback;
        NetworkManager.OnClientConnectedCallback += ConnectionEstablishedToServer;

        NetworkManager.StartClient();
    }

    private void ConnectionEstablishedToServer(ulong clientID)
    {
        NetworkManager.OnClientConnectedCallback -= ConnectionEstablishedToServer;

        _connectionEstablishedToServerCallback?.Invoke(clientID);
    }

    #endregion

    #region Server RPCs

    [ServerRpc(RequireOwnership = false)]
    public void StartMatchServerRpc()
    {
        Debug.Log("Starting match, Loading arena scene");
        NetworkManager.SceneManager.LoadScene("Arena", LoadSceneMode.Single);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnPlayerPrefabServerRpc(Vector3 position, ServerRpcParams rpcParams = default)
    {
        Debug.Log($"------------------------ Spawning player for client id {rpcParams.Receive.SenderClientId}");
        GameObject playerObject = Instantiate(_playerPrefab, position, Quaternion.identity);
        playerObject.GetComponent<NetworkObject>().SpawnAsPlayerObject(rpcParams.Receive.SenderClientId, true);
    }

    #endregion
}