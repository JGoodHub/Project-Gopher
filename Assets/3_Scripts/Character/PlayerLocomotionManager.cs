using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionManager : MonoBehaviour
{
    private PlayerManager playerManager;

    [Header("Movement Values")] // These do not have to be here but I like having a copy to use
    public float HorizontalMovement;
    public float VerticalMovement;
    public float MoveAmount;

    private Vector3 MoveDirection;

    [SerializeField] private float WalkingSpeed = 2.0f;
    [SerializeField] private float RunningSpeed = 5.0f;
    [SerializeField] private float RotationSpeed = 15.0f;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    

    public void HandleAllMovement()
    {
        HandleGroundedMovement();
    }

    private void GetMovementValues()
    {
        HorizontalMovement = PlayerInputManager.instance.HorizontalInput;
        VerticalMovement = PlayerInputManager.instance.VerticalInput;
        MoveAmount = PlayerInputManager.instance.MoveAmount;
    }

    private void HandleGroundedMovement()
    {
        // Currently just takes player transform to determine move direction
        GetMovementValues();
        MoveDirection = transform.forward * VerticalMovement;
        MoveDirection += transform.right * HorizontalMovement;
        MoveDirection.Normalize();
        MoveDirection.y = 0;
        
        
        if (MoveAmount > 0.5f)
        {
            playerManager.characterController.Move(MoveDirection * RunningSpeed * Time.deltaTime);
        }
        else if (MoveAmount <= 0.5f)
        {
            playerManager.characterController.Move(MoveDirection * WalkingSpeed * Time.deltaTime);
        }
    }
}