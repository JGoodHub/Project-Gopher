using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GopherAnimController : MonoBehaviour
{
    
    [SerializeField] private Animator _animator;

    private Vector3 _lastFramePosition = Vector3.zero;
    
    private void Start()
    {
        _lastFramePosition = transform.position;
    }

    private void Update()
    {
        Vector3 currentFramePosition = transform.position;
        Vector3 velocity = (currentFramePosition - _lastFramePosition) / Time.deltaTime;
        
        Vector3 relativeVelocity = Quaternion.Euler(0f, -transform.rotation.eulerAngles.y, 0) * velocity;
        
        relativeVelocity.Normalize();

        _animator.SetFloat("X", relativeVelocity.x);
        _animator.SetFloat("Z", relativeVelocity.z);

        _lastFramePosition = transform.position;
    }
}