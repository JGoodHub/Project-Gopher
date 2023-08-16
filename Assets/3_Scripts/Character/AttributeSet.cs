using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;

public class AttributeSet : MonoBehaviour
{
    [SerializeField] private float BaseHealth = 100;
    [SerializeField] private float MaxHealth = 500; 
    [SerializeField] private float Health;

    [SerializeField] private int HeldChains = 2;
    [SerializeField] private int MaxChains = 5; // Maybe

    private int Dashes = 2;
    private int MaxDashes = 2; // maybe powerups can alter this
    

    private void Start()
    {
        Health = BaseHealth;
    }

    private void Update()
    {
        MaxHealth = BaseHealth * MaxChains;
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
        return Health > 0;
    }

    public float GetHealthPercentage()
    {
        return Health / MaxHealth; // I think progress bars in unity prefer a 0.0 - 1.0 scale if not * 100.0f
    }
    
}
