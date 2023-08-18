using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterManager : NetworkBehaviour
{
    protected CharacterLocomotionManager characterLocomotionManager;
    public CharacterController characterController;
    public AttributeSet attributeSet;
    protected BallAndChainThrower _ballAndChainThrower;

    protected virtual void Awake()
    {
        characterLocomotionManager = GetComponent<CharacterLocomotionManager>();
        characterController = GetComponent<CharacterController>();
        attributeSet = GetComponent<AttributeSet>();
        _ballAndChainThrower = GetComponent<BallAndChainThrower>();
    }

    protected virtual void Update()
    {
        // ThrowChain();
        if(attributeSet.isStunned) {
            //handle stun
        }
    }

    // protected void ThrowChain()
    // {
    //     if (IsOwner && Input.GetButtonDown("Fire1"))
    //     {
    //         Debug.Log("throwing");
    //         _ballAndChainThrower.FireChain();
    //     }
    // }    
}
