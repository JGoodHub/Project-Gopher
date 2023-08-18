using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallAndChainTracker : MonoBehaviour
{
    [SerializeField] private GameObject _ballAndChainPrefab;
    [SerializeField] private int _maxAttachedChains = 5;
    [Space]
    [SerializeField] private float _tapsRequiredToRemove;
    [SerializeField] private float _tapsMinInterval;
    [SerializeField] private float _tapsExpiration;
    [Space]
    [SerializeField] private ParticleSystem _hammerParticleSystem;

    private float stunTimeRemaining;
    private int _tapCount;
    private float _tapIntervalCooldown;
    private float _tapResetCooldown;
    private bool _initialized = false;
    
    private List<Transform> _attachedChains = new List<Transform>();
    private CharacterLocomotionManager characterLocomotionManager;
    private AttributeSet _attributeSet;
    private BallAndChainThrower _ballAndChainThrower;

    private IEnumerator Start()
    {
        // small delay to fix weird initial chain physics (they sank below the ground)
        yield return new WaitForSeconds(0.5f);
        characterLocomotionManager = GetComponent<CharacterLocomotionManager>();
        _attributeSet = GetComponent<AttributeSet>();
        _attributeSet.onHeldChainsChanged += UpdateChains;
        _ballAndChainThrower = GetComponent<BallAndChainThrower>();

        for (int i = 0; i < _attributeSet.heldChains; i++)
        {
            AddChain();
        }

        _initialized = true;
    }

    private void Update()
    {
        if (!_initialized) return;

        _tapResetCooldown -= Time.deltaTime;
        _tapIntervalCooldown -= Time.deltaTime;
        stunTimeRemaining -= Time.deltaTime;

        if (_tapResetCooldown <= 0)
            _tapCount = 0;

        if (Input.GetButtonDown("Jump")) //&& _tapIntervalCooldown <= 0 && _attachedChains.Count > 0)
        {
            _tapCount++;
            _tapIntervalCooldown = _tapsMinInterval;
            _tapResetCooldown = _tapsExpiration;
            stunTimeRemaining -= 0.5f;
        }

        if (_tapCount >= _tapsRequiredToRemove)
        {
            RemoveChain();
            _tapCount = 0;
            _tapIntervalCooldown = _tapsMinInterval;
            _tapResetCooldown = _tapsExpiration;
        }

        if (stunTimeRemaining <= 0 && _attributeSet.isStunned) {
            _attributeSet.isStunned = false;
            characterLocomotionManager.canRotate = true;
            if (_ballAndChainThrower != null && _attributeSet != null && characterLocomotionManager != null) 
            {
                _ballAndChainThrower.ThrowAllChainsRandomly();
            }
        }
    }

    public bool CanAttachChain()
    {
        return _attachedChains.Count < _maxAttachedChains;
    }

    public void AddChain()
    {
        if (_attachedChains.Count >= _maxAttachedChains)
            return;

        Vector3 attachPosition = transform.position;
        attachPosition.y = 0.4f;

        Transform chainTransform = Instantiate(_ballAndChainPrefab, attachPosition, Quaternion.Euler(0, _attachedChains.Count * 144, 0), transform).transform;
        _attachedChains.Add(chainTransform);
        characterLocomotionManager.RunningSpeed -= 1f;
        if (_attachedChains.Count >= _attributeSet.maxHeldChains) {
            _attributeSet.isStunned = true;
            characterLocomotionManager.canRotate = false;
            stunTimeRemaining = 5.0f; 
        } else {
            characterLocomotionManager.canRotate = true;
            _attributeSet.isStunned = false;
        }
    }

    public void RemoveChain()
    {
        if (_attachedChains.Count == 0)
            return;

        Destroy(_attachedChains[0].gameObject);
        _attachedChains.RemoveAt(0);
        characterLocomotionManager.RunningSpeed += 1f;
    }

    private void UpdateChains(int newHeldChains)
    {
        while (_attachedChains.Count < newHeldChains)
        {
            AddChain();
        }
        while (_attachedChains.Count > newHeldChains)
        {
            RemoveChain();
        }
    }
}