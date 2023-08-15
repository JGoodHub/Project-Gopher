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
    public AttributeSet attributeSet;
    
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
        attributeSet = GetComponent<AttributeSet>();

    }

    private void Update()
    {
        playerLocomotionManager.HandleAllMovement();
    }
}
