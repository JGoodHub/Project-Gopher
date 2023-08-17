using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    protected CharacterLocomotionManager characterLocomotionManager;
    public CharacterController characterController;
    public AttributeSet attributeSet;
    
    private void Awake()
    {
        // if (instance == null)
        // {
        //     instance = this;
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }
        characterLocomotionManager = GetComponent<CharacterLocomotionManager>();
        characterController = GetComponent<CharacterController>();
        attributeSet = GetComponent<AttributeSet>();
    }
    private void Update()
    {
        characterLocomotionManager.HandleAllMovement();
    }
}
