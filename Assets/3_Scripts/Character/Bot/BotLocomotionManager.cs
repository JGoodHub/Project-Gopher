using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotLocomotionManager : CharacterLocomotionManager
{
    protected override void GetMovementValues()
    {
        HorizontalMovement = 0;
        VerticalMovement = 0;
        MoveAmount = 0;
    }
}