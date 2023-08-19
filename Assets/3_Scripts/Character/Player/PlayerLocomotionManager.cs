using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{

    protected override void Awake()
    {
        characterManager = GetComponent<PlayerManager>();
    }

    protected override void GetMovementValues()
    {
        if (IsOwner == false)
            return;

        HorizontalMovement = InputManager.Singleton.HorizontalInput;
        VerticalMovement = InputManager.Singleton.VerticalInput;
        MoveAmount = InputManager.Singleton.MoveAmount;
    }

}