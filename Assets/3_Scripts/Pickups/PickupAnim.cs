using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAnim : MonoBehaviour
{
    [SerializeField] private Transform _pickupRoot;

    [SerializeField] private float _bobRate;
    [SerializeField] private float _bobRange;
    [SerializeField] private float _spinRate;

    private float _timeAlive;
    private float _baseY;

    private void Start()
    {
        _baseY = _pickupRoot.position.y;
    }

    private void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = _baseY + (_bobRange * Mathf.Sin(Time.time * _bobRate));
        transform.position = newPosition;

        transform.Rotate(Vector3.up, Time.deltaTime * _spinRate);
    }
}