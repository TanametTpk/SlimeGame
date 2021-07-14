using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class Skill : ScriptableObject
{
    public string name;
    public string description;
    public Sprite sprite;
    public float manaCost;
    public List<CharacterAction> actions;
    public float cooldown;
    private long prevUsedTime = 0;
    private bool active = false;

    public void Use(RPGCharacter character) {
        if (IsActive() || !CanUseSkill() || character.GetMana() < manaCost) return;
        active = true;
        character.UseMana(manaCost);
        foreach (CharacterAction action in actions)
        {
            action.PerformBy(character);
        }
    }

    public void Stop(RPGCharacter character) {
        if (!IsActive()) return;

        foreach (CharacterAction action in actions)
        {
            action.StopPerformBy(character);            
        }

        prevUsedTime = GetCurrentTime();
        active = false;
    }

    public void Reset(RPGCharacter character)
    {
        foreach (CharacterAction action in actions)
        {
            action.Reset(character);            
        }
        
        prevUsedTime = 0;
        active = false;
    }

    public void UpdateBy(RPGCharacter character)
    {
        foreach (CharacterAction action in actions)
        {
            action.UpdateBy(character);            
        }
    }

    public void FixedUpdateBy(RPGCharacter character)
    {
        foreach (CharacterAction action in actions)
        {
            action.FixedUpdateBy(character);            
        }
    }

    public bool CanUseSkill() {
        return GetCurrentTime() - prevUsedTime > cooldown * 1000;
    }

    public bool IsActive() {
        return active;
    }

    private long GetCurrentTime() {
        return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }
}
