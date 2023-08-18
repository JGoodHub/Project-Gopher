using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;

    private void Start()
    {
        ServerHandler.Singleton.SpawnPlayerPrefabServerRpc(_playerSpawnPoint.position);
    }
}