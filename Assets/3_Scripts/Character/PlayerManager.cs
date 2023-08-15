using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private PlayerLocomotionManager playerLocomotionManager;
    public CharacterController characterController;
    
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
        
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        characterController = GetComponent<CharacterController>();

    }

    private void Update()
    {
        playerLocomotionManager.HandleAllMovement();
    }
}
