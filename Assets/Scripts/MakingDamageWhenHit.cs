using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakingDamageWhenHit : WhenHitByScript
{
    public RPGCharacter attacker;
    public float damage = 10;
    public override void PerformTrigger2D(Collider2D other) {
        if (other == null) return;
        if (other.tag.Equals(attacker.tag)) return;
        RPGCharacter character = other.GetComponent<RPGCharacter>();
        attacker.Attack(character).WithDamage(damage).exec();
    }
}
