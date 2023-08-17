using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterManager : NetworkBehaviour
{
    // public static CharacterManager instance;
    protected CharacterLocomotionManager characterLocomotionManager;
    [SerializeField] private PlayerNetworkManager _playerNetworkManager;
    public CharacterController characterController;
    public AttributeSet attributeSet;
    

    protected virtual void Awake()
    {
        // if (instance == null)
        // {
        //     instance = this;
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }
        _playerNetworkManager = GetComponent<PlayerNetworkManager>();
        characterLocomotionManager = GetComponent<CharacterLocomotionManager>();
        characterController = GetComponent<CharacterController>();
        attributeSet = GetComponent<AttributeSet>();
    }

    protected virtual void Update()
    {
        UpdateNetworkVariables();
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
