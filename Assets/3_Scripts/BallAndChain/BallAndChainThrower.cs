using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAndChainThrower : MonoBehaviour
{
    [SerializeField] private GameObject _chainPrefab;
    [SerializeField] private float _range;

    private AttributeSet _attributeSet;

    private void Start()
    {
        _attributeSet = GetComponent<AttributeSet>();
    }

    private void Update()
    {
        FireChain();
    }

    public void FireChain()
    {
        if (Input.GetButtonDown("Fire1") && (_attributeSet.heldChains > 0) && gameObject.CompareTag("Player") && (_attributeSet.isStunned == false))
        {
            Vector3 target = RaycastPlane.QueryPlane();

            Debug.DrawLine(transform.position, target, Color.green, 5f);

            ThrowChain(target);
        }
    }

    public void ThrowChain(Vector3 target)
    {
        ThrowableBallAndChain chain = Instantiate(_chainPrefab).GetComponent<ThrowableBallAndChain>();

        Vector3 startPosition = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 direction = (target - startPosition).normalized;
        Vector3 endPosition = startPosition + direction * _range;

        RaycastHit hit;
        if (Physics.Raycast(startPosition, direction, out hit, _range))
        {
            if (!hit.collider.gameObject.CompareTag("Player") && !hit.collider.gameObject.CompareTag("OtherPlayers")
                                                              && !hit.collider.gameObject.CompareTag("Grid"))
            {
                endPosition = hit.point;
            }
        }

        Debug.DrawLine(startPosition, endPosition, Color.red, 5f);

        chain.Initialise(startPosition, endPosition);

        _attributeSet.heldChains--;
    }

    public void ThrowAllChainsRandomly()
    {
        while (_attributeSet.heldChains > 0)
        {
            Vector3 randomDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized;
            Vector3 target = transform.position + randomDirection * _range;

            ThrowChain(target);
        }
    }
}