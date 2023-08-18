using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammoCounterText;
    [SerializeField] private CharacterManager _characterManager;    

    private void Update()
    {
        // TODO THis will need to change when networking is added
        _ammoCounterText.text = $"{_characterManager.attributeSet.heldChains}/{_characterManager.attributeSet.maxHeldChains}";
    }
}