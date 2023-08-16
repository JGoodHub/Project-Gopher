using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallAndChainTracker : MonoBehaviour
{

    [SerializeField] private GameObject _ballAndChainPrefab;
    [SerializeField] private float _attachHeight = 0.5f;

    private List<Transform> _attachedChains = new List<Transform>();

    private void Start()
    {
        AddChain();
        AddChain();
    }

    public void AddChain()
    {
        Vector3 attachPosition = transform.position;
        attachPosition.y = 0.5f;

        Transform chainTransform = Instantiate(_ballAndChainPrefab, attachPosition, Quaternion.Euler(0, _attachedChains.Count * 144, 0), transform).transform;
        _attachedChains.Add(chainTransform);
    }

    public void RemoveChain()
    {
        if (_attachedChains.Count == 0)
            return;

        Destroy(_attachedChains[0].gameObject);
    }

}