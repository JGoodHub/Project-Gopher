using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private static InputManager _singleton;
    public static InputManager Singleton => _singleton ??= FindObjectOfType<InputManager>(true);

    private PlayerControls playerControls;

    [Header("Movement Input")]
    [SerializeField] private Vector2 MovementInput;
    public float HorizontalInput;
    public float VerticalInput;
    public float MoveAmount;

    [Header("Camera Input")]
    [SerializeField] private Vector2 CameraInput;
    private float CameraHorizontalInput, CameraVerticalInput;

    private void Start()
    {
        playerControls ??= new PlayerControls();

        playerControls.Movement.Movement.performed += i => MovementInput = i.ReadValue<Vector2>();
        playerControls.Camera.Camera.performed += i => CameraInput = i.ReadValue<Vector2>();

        playerControls.Enable();
    }

    private void Update()
    {
        StoreMovementInput();
        StoreCameraInput();
    }

    private void StoreMovementInput()
    {
        HorizontalInput = MovementInput.x;
        VerticalInput = MovementInput.y;

        MoveAmount = Mathf.Clamp01(Mathf.Abs(HorizontalInput) + Mathf.Abs(VerticalInput));

        // OPTIONAL CLAMPING 
        if (MoveAmount <= 0.5 && MoveAmount > 0)
        {
            MoveAmount = 0.5f;
        }
        else if (MoveAmount > 0.5 && MoveAmount <= 1)
        {
            MoveAmount = 1.0f;
        }
    }

    private void StoreCameraInput()
    {
        CameraHorizontalInput = CameraInput.x;
        CameraVerticalInput = CameraInput.y;
    }

}