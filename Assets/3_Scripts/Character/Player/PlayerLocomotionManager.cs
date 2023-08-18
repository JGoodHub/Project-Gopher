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
        HorizontalMovement = PlayerInputManager.instance.HorizontalInput;
        VerticalMovement = PlayerInputManager.instance.VerticalInput;
        MoveAmount = PlayerInputManager.instance.MoveAmount;
    }
}