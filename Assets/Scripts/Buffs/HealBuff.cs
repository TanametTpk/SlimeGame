using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Heal Buff", menuName = "Heal Buff")]
public class HealBuff: BuffBase {
    public float healPoint;

    public override void OnAdd(RPGCharacter character) {
        character.Heal(healPoint);
        character.RestoreMana(character.GetMaxMana());
    }

    public override void OnRemove(RPGCharacter character)
    {
        // do nothing
    }
}