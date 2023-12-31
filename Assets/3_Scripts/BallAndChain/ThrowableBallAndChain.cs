using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ThrowableBallAndChain : MonoBehaviour
{
    [SerializeField] private float _startHeight = 0.5f;
    [SerializeField] private float _speed;

    private Vector3 _startPosition;
    private Vector3 _endPosition;

    public void Initialise(Vector3 startPosition, Vector3 endPosition)
    {
        _startPosition = startPosition;
        _endPosition = endPosition;

        float lifetime = (_endPosition - _startPosition).magnitude / _speed;

        transform.position = _startPosition;
        transform.LookAt(_endPosition, Vector3.up);

        DOVirtual.Float(0, 1, lifetime, UpdatePosition)
            .OnComplete(HandleMissedShot)
            .SetEase(Ease.Linear);
    }

    private void UpdatePosition(float t)
    {
        Vector3 newPosition = Vector3.Lerp(_startPosition, _endPosition, t);
        //newPosition.y = Mathf.Cos((t * 1.1f) * (Mathf.PI * 0.5f)) * _startHeight;
        newPosition.y = _startHeight;

        transform.position = newPosition;
    }

    private void HandleMissedShot()
    {
        PickupsManager.Singleton.SpawnPickup(Pickup.Type.Ammo, new Vector3(transform.position.x, 0f, transform.position.z));

        Destroy(gameObject, 0.02f);

        // TODO Handle spawning a networked pickup-able chain item 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OtherPlayers"))
        {
            // TODO Damage the other player and attached a chain
            return;
        }

        if (other.CompareTag("Terrain"))
        {
            HandleMissedShot();
        }
    }
}