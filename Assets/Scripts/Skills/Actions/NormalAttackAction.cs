using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attacking Action", menuName = "Normal Attack Action")]
public class NormalAttackAction : CharacterAction
{
    public LayerMask[] layerMask;
    public override void PerformBy(RPGCharacter character)
    {
        Weapon weapon = character.GetWeapon();
        weapon.UseBy(character);
    }
}
