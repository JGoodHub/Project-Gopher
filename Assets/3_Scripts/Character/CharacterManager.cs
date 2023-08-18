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
    public GameObject stunEffectPrefab;
    private GameObject currentStunEffect;

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

        // handles stun effect
        if(attributeSet.isStunned && currentStunEffect == null)
        {
            // Add an offset to the Y position so the effect appears above the character
            Vector3 effectPosition = transform.position + new Vector3(0, 2.0f, 0);
            // Instantiate the effect and parent it to this character
            currentStunEffect = Instantiate(stunEffectPrefab, effectPosition, Quaternion.identity, transform);
        }
        else if(!attributeSet.isStunned && currentStunEffect != null)
        {
            // Destroy the effect when the stun ends
            Destroy(currentStunEffect);
            currentStunEffect = null;
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
