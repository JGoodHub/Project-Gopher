using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

<<<<<<< HEAD:Assets/3_Scripts/Character/Player/PlayerManager.cs
public class PlayerManager : CharacterManager
{
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
        characterLocomotionManager = GetComponent<CharacterLocomotionManager>();
=======
public class PlayerManager : NetworkBehaviour
{
    // public static PlayerManager instance;
    private PlayerLocomotionManager playerLocomotionManager;
    public CharacterController characterController;
    private PlayerNetworkManager _playerNetworkManager;
    private PlayerCamera _playerCamera;
    public AttributeSet attributeSet;

   
    
    private void Awake()
    {

        // if (instance == null)
        // {
        //     instance = this;
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }
        
        DontDestroyOnLoad(this);
       
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
>>>>>>> adc1eed (Networking Progress):Assets/3_Scripts/Character/PlayerManager.cs
        characterController = GetComponent<CharacterController>();
        _playerNetworkManager = GetComponent<PlayerNetworkManager>();
        _playerCamera = GetComponentInChildren<PlayerCamera>();
        attributeSet = GetComponent<AttributeSet>();
<<<<<<< HEAD:Assets/3_Scripts/Character/Player/PlayerManager.cs
=======
        

    }

    private void Start()
    {
        // PlayerCamera.instance.playerManager = this;
    }

    private void Update()
    {
        if (IsOwner)
        {
            playerLocomotionManager.HandleAllMovement();
            _playerCamera.AttachToPlayerAndFollow();
        }
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
            
            transform.rotation = Quaternion.Slerp(transform.rotation, _playerNetworkManager.NetworkRotation.Value, _playerNetworkManager.NetworkRotationSmoothTime);
        }
>>>>>>> adc1eed (Networking Progress):Assets/3_Scripts/Character/PlayerManager.cs
    }
}
