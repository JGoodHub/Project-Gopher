using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;

public class AttributeSet : MonoBehaviour
{
    [SerializeField] private float _baseHealth = 100;
    [SerializeField] private float _maxHealth = 500;

    public int heldChains = 2;
    public int maxHeldChains = 5;

    private int _dashes = 2;
    private int _maxDashes = 2; // maybe power-ups can alter this

    private float _health;

    private void Start()
    {
        _health = _baseHealth;
    }

    private void Update()
    {
        _maxHealth = _baseHealth * heldChains;
    }

    private void Die()
    {
        //TODO: Actual Die function 
        if (!IsAlive())
        {
            Destroy(gameObject);
        }
    }

    private bool IsAlive()
    {
        return _health > 0;
    }

    public float GetHealthPercentage()
    {
        return _health / _maxHealth; // I think progress bars in unity prefer a 0.0 - 1.0 scale if not * 100.0f
    }
    
}