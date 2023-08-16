using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePitfallTimer : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    private float timer = 60.0f;
    private bool _isTimerActive = false;
    private bool _isPitfallOpen = false;
    


    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        UpdateTimer();
        UpdateText();
        
    }

    private void UpdateText()
    {
        text.text = _isPitfallOpen ? "Pitfall is OPEN" : $"Pitfall Opens in {(int)timer}";
    }
    
    private void UpdateTimer()
    {
        if (!_isTimerActive) return;
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            DisableTimer();
        }
    }

    public void EnableTimer()
    {
        _isTimerActive = true;
    }

    private void DisableTimer()
    { 
        _isPitfallOpen = true;
        _isTimerActive = false;
        timer = 60.0f;
    }
    
    
}
