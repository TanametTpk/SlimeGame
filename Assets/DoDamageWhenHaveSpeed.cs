using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamageWhenHaveSpeed : WhenHitByScript
{
    public RPGCharacter attacker;
    public Rigidbody2D rb;
    
    public override void PerformTrigger2D(Collider2D other) {
        if (other == null) return;
        if (other.tag.Equals(attacker.tag)) return;

        float damage = rb.mass * (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y)) / 10;
        if (damage < 1) return;

        RPGCharacter character = other.GetComponent<RPGCharacter>();
        attacker
            .Attack(character)
            .WithDamage(damage)
            .WithEffect(new KnockbackEffect((Vector3)rb.velocity, 0.5f))
            .exec();
    }
}
