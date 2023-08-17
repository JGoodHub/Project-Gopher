using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private float _roundTimer = 0.0f;
    [SerializeField] private bool _isTimerActive = false;
    private const float _maxRoundTimer = 60.0f;

    private UpdatePitfallTimer _updatePitfallTimer;
    private void Start()
    {
        _updatePitfallTimer = FindObjectOfType<UpdatePitfallTimer>();
        RoundStartTimer();
        _updatePitfallTimer.EnableTimer();
    }

    private void Update()
    {
        RoundTimer();
    }

    private void RoundTimer()
    {
        if (_isTimerActive)
        {
            _roundTimer += Time.deltaTime;
            if (_roundTimer >= _maxRoundTimer)
            {
                EndRound();
            }
        }
    }

    private void RoundStartTimer()
    {
        _isTimerActive = true;
    }

    private void RoundEndTimer()
    {
        _isTimerActive = false;
    }

    private void ResetTimer()
    {
        _roundTimer = 0.0f;
    }

    private void EndRound()
    {
        Pitfall.instance.OpenPitfall();
        RoundEndTimer();
        ResetTimer();
    }
    
    
}

