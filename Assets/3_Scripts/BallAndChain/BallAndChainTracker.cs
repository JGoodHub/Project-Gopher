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

    private int _tapCount;
    private float _tapIntervalCooldown;
    private float _tapResetCooldown;
    
    private List<Transform> _attachedChains = new List<Transform>();
    private CharacterLocomotionManager characterLocomotionManager;
    private AttributeSet _attributeSet;

    private IEnumerator Start()
    {
        // small delay to fix weird initial chain physics (they sank below the ground)
        yield return new WaitForSeconds(0.5f);
        characterLocomotionManager = GetComponent<CharacterLocomotionManager>();
        _attributeSet = GetComponent<AttributeSet>();
        _attributeSet.onHeldChainsChanged += UpdateChains;

        for (int i = 0; i < _attributeSet.heldChains; i++)
        {
            AddChain();
        }
    }

    private void Update()
    {
        _tapResetCooldown -= Time.deltaTime;
        _tapIntervalCooldown -= Time.deltaTime;

        if (_tapResetCooldown <= 0)
            _tapCount = 0;

        if (Input.GetButtonDown("Jump") && _tapIntervalCooldown <= 0 && _attachedChains.Count > 0)
        {
            // for if we ever want to use the spacebar key
            _tapCount++;
            _tapIntervalCooldown = _tapsMinInterval;
            _tapResetCooldown = _tapsExpiration;
        }

        if (_tapCount >= _tapsRequiredToRemove)
        {
            RemoveChain();
            
            _tapCount = 0;

            _tapIntervalCooldown = _tapsMinInterval;
            _tapResetCooldown = _tapsExpiration;
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