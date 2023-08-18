using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerManager : CharacterManager
{

    [SerializeField] private PlayerNetworkManager _playerNetworkManager;
    private PlayerCamera _playerCamera;
    private PlayerLocomotionManager _playerLocomotionManager;

    protected override void Awake()
    {
        base.Awake();
        // if (instance == null)
        // {
        //     instance = this;
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }
        _playerNetworkManager = GetComponent<PlayerNetworkManager>();
        _playerCamera = GetComponentInChildren<PlayerCamera>();
        _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
    }

    private void Start()
    {
        // PlayerCamera.instance.playerManager = this;
        DontDestroyOnLoad(gameObject);
    }

    protected override void Update()
    {
        base.Update();
        _playerLocomotionManager.HandleAllMovement();
        _playerCamera.AttachToPlayerAndFollow();
        UpdateNetworkVariables();
        ThrowChain();
    }

    private void UpdateNetworkVariables()
    {
        if (IsOwner)
        {
            _playerNetworkManager.NetworkPosition.Value = transform.position;
            _playerNetworkManager.NetworkRotation.Value = transform.rotation;
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, _playerNetworkManager.NetworkPosition.Value,
                ref _playerNetworkManager.NetworkPositionVelocity, _playerNetworkManager.NetworkPositionSmoothTime);
        
            transform.rotation = Quaternion.Slerp(transform.rotation, _playerNetworkManager.NetworkRotation.Value,
                _playerNetworkManager.NetworkRotationSmoothTime);
        }
    }   
}
