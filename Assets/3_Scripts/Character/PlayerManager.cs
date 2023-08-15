using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerManager : MonoBehaviour
{
    private PlayerLocomotionManager playerLocomotionManager;
    public CharacterController characterController;
    
    private void Awake()
    {
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        characterController = GetComponent<CharacterController>();

    }

    private void Update()
    {
        playerLocomotionManager.HandleAllMovement();
    }
}
