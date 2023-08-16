using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pitfall : MonoBehaviour
{

    public static Pitfall instance;
    
    private MeshCollider _meshCollider;
    private MeshRenderer _meshRenderer;

    private bool _pitfallOpen = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _meshCollider = GetComponent<MeshCollider>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void OpenPitfall()
    {
        _meshCollider.enabled = false;
        _meshRenderer.enabled = false;
        _pitfallOpen = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_pitfallOpen) return;
        if (other.CompareTag("Player"))
        {
            //TODO: Add Logic of what happens when going into pitfall
            Debug.Log("works");
        }
    }
}
