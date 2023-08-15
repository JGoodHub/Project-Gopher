using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAndChainThrower : MonoBehaviour
{

    [SerializeField] private GameObject _chainPrefab;
    [SerializeField] private float _range;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 target = RaycastPlane.QueryPlane();

            ThrowChain(target);
        }
    }

    public void ThrowChain(Vector3 target)
    {
        ThrowableBallAndChain chain = Instantiate(_chainPrefab).GetComponent<ThrowableBallAndChain>();

        Vector3 startPosition = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 endPosition = (target - startPosition).normalized * _range;
        
        chain.Initialise(startPosition, endPosition);
        //chain.SetInitialPosition(startPosition);
    }

}