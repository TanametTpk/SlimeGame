using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Speed Buff", menuName = "Speed Buff")]
public class SpeedBuff: BuffBase {
    public float speed;

    public override void OnAdd(RPGCharacter character) {
        character.SetSpeed(character.GetSpeed() + speed);
    }

    public override void OnRemove(RPGCharacter character){
        character.SetSpeed(character.GetSpeed() - speed);
    }
}