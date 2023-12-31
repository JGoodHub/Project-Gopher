using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class CharacterLocomotionManager : NetworkBehaviour
{
    protected CharacterManager characterManager;

    [Header("Movement Values")] // These do not have to be here but I like having a copy to use
    public float HorizontalMovement;

    public float VerticalMovement;
    public float MoveAmount;
    public AudioSource footstepsSound;
    private Vector3 MoveDirection;

    [SerializeField] public float WalkingSpeed = 2.5f;
    [SerializeField] public float RunningSpeed = 5.0f;
    public bool canRotate = true;

    private const float gravity = -9.81f;
    private const float groundOffset = 0.1f;

    [SerializeField] private Space _movementScope;
    [SerializeField] private bool _lockY;

    protected abstract void Awake();

    public void HandleAllMovement()
    {
        HandleGroundedMovement();
        HandleRotationMovement();
    }

    protected abstract void GetMovementValues();

    private float verticalVelocity = 0f;

    private void HandleGroundedMovement()
    {
        GetMovementValues();
        if (_movementScope == Space.Self)
        {
            MoveDirection = transform.forward * VerticalMovement;
            MoveDirection += transform.right * HorizontalMovement;
        }
        else
        {
            MoveDirection = Vector3.forward * VerticalMovement;
            MoveDirection += Vector3.right * HorizontalMovement;
        }

        MoveDirection.Normalize();
        
        // gravity
        if(characterManager.characterController.isGrounded)
        {
            verticalVelocity = -groundOffset;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 combinedMove = MoveDirection + Vector3.up * verticalVelocity;

        if (MoveAmount > 0.5f)
        {
            characterManager.characterController.Move(combinedMove * RunningSpeed * Time.deltaTime);
            footstepsSound.enabled = true;
        }
        else if (MoveAmount <= 0.5f)
        {
            characterManager.characterController.Move(combinedMove * WalkingSpeed * Time.deltaTime);
            footstepsSound.enabled = false;
        }
    }

    private void HandleRotationMovement()
    {
        if (!Application.isFocused) return;
        if(canRotate) {
            Vector3 mousePosition = RaycastPlane.QueryPlane();

            characterManager.characterController.transform.LookAt(mousePosition, Vector3.up);
        }
    }
}

