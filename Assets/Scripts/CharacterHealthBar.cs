using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthBar : MonoBehaviour
{
    public UISliderBar sliderBar;
    public RPGCharacter character;

    private void Start() {
        sliderBar.SetMaxValue((int)Mathf.Ceil(character.GetMaxHealth()));
        sliderBar.SetValue((int)Mathf.Ceil(character.GetHealth()));
    }
    void Update()
    {
        UpdateHealth();
    }
    private void UpdateHealth()
    {
        sliderBar.SetValue((int)Mathf.Ceil(character.GetHealth()));
    }
}
