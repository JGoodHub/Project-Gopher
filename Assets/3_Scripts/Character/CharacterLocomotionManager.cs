using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterLocomotionManager : MonoBehaviour
{
    protected CharacterManager characterManager;

    [Header("Movement Values")] // These do not have to be here but I like having a copy to use
    public float HorizontalMovement;
    public float VerticalMovement;
    public float MoveAmount;

    private Vector3 MoveDirection;

    [SerializeField] public float WalkingSpeed = 2.5f;
    [SerializeField] public float RunningSpeed = 5.0f;
    [SerializeField] private float RotationSpeed = 15.0f;

    [SerializeField] private Space _movementScope;
    [SerializeField] private bool _lockY;

    private void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
    }


    public void HandleAllMovement()
    {
        HandleGroundedMovement();
        HandleRotationMovement();
    }

    protected abstract void GetMovementValues();

    private void HandleGroundedMovement()
    {
        // Currently just takes player transform to determine move direction
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
        //MoveDirection.y = 0;

        if (MoveAmount > 0.5f)
        {
            characterManager.characterController.Move(MoveDirection * RunningSpeed * Time.deltaTime);
        }
        else if (MoveAmount <= 0.5f)
        {
            characterManager.characterController.Move(MoveDirection * WalkingSpeed * Time.deltaTime);
        }

        if (_lockY)
        {
           transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }

    private void HandleRotationMovement()
    {
        Vector3 mousePosition = RaycastPlane.QueryPlane();

        characterManager.characterController.transform.LookAt(mousePosition, Vector3.up);
    }
}