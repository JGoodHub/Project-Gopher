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
    private PlayerLocomotionManager _playerLocomotionManager;

    protected override void Awake()
    {
        base.Awake();

        _playerNetworkManager = GetComponent<PlayerNetworkManager>();
        _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);

        PlayerCamera.Singleton.SetPlayerTarget(this);
    }

    protected override void Update()
    {
        base.Update();

        _playerLocomotionManager.HandleAllMovement();

        UpdateNetworkVariables();
    }

    private void UpdateNetworkVariables()
    {
        if (_playerNetworkManager == null || _playerNetworkManager.NetworkPosition == null || _playerNetworkManager.NetworkRotation == null)
            return;

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