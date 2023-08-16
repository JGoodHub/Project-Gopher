using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private float roundTimer = 0.0f;
    [SerializeField] private bool isTimerActive = false;
    private const float MaxRoundTimer = 60.0f;

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
        if (isTimerActive)
        {
            roundTimer += Time.deltaTime;
            if (roundTimer >= MaxRoundTimer)
            {
                EndRound();
            }
        }
    }

    private void RoundStartTimer()
    {
        isTimerActive = true;
    }

    private void RoundEndTimer()
    {
        isTimerActive = false;
    }

    private void ResetTimer()
    {
        roundTimer = 0.0f;
    }

    private void EndRound()
    {
        Pitfall.instance.OpenPitfall();
        RoundEndTimer();
        ResetTimer();
    }
    
    
}

