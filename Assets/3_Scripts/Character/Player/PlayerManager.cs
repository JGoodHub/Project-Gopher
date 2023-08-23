using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerManager : CharacterManager
{

    private PlayerLocomotionManager _playerLocomotionManager;

    protected override void Awake()
    {
        base.Awake();

        _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
    }

    private IEnumerator Start()
    {
        if (IsOwner == false)
            yield break;

        yield return new WaitForSeconds(0.2f);

        PlayerCamera.Singleton.SetPlayerTarget(this);
    }

    protected override void Update()
    {
        base.Update();

        _playerLocomotionManager.HandleAllMovement();
    }

}