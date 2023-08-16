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
    private AttributeSet _attributeSet;


    private void Start()
    {
        _attributeSet = GetComponent<AttributeSet>();

        AddChain();
        AddChain();
    }

    private void Update()
    {
        _tapResetCooldown -= Time.deltaTime;
        _tapIntervalCooldown -= Time.deltaTime;

        if (_tapResetCooldown <= 0)
            _tapCount = 0;

        if (Input.GetButtonDown("Jump") && _tapIntervalCooldown <= 0 && _attachedChains.Count > 0)
        {
            _tapCount++;
            _tapIntervalCooldown = _tapsMinInterval;
            _tapResetCooldown = _tapsExpiration;
            
            _hammerParticleSystem.Emit(1);
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
    }

    public void RemoveChain()
    {
        if (_attachedChains.Count == 0)
            return;

        Destroy(_attachedChains[0].gameObject);
        _attachedChains.RemoveAt(0);

        if (_attributeSet.AreChainsMaxedOut())
            PickupsController.Singleton.SpawnPickup(Pickup.Type.Ammo, Quaternion.Euler(0, Random.Range(0f, 360f), 0) * Vector3.forward * 1.5f);
        else
            _attributeSet.heldChains++;
    }
}