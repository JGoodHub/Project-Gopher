using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammoCounterText;

    private void Update()
    {
        // TODO THis will need to change when networking is added
        _ammoCounterText.text = $"{PlayerManager.instance.attributeSet.heldChains}/{PlayerManager.instance.attributeSet.maxHeldChains}";
    }
}