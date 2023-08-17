using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerManager : CharacterManager
{
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
        characterLocomotionManager = GetComponent<CharacterLocomotionManager>();
        characterController = GetComponent<CharacterController>();
        attributeSet = GetComponent<AttributeSet>();
    }
}
