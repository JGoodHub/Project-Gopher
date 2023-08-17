using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerManager : CharacterManager
{

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
    }

   
}
