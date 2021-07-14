using System.Collections;
using System.Collections.Generic;

public class AttackingExecutor
{
    private AttackingInfo info;
    public AttackingExecutor() {
        this.info = new AttackingInfo();
        info.damage = 0;
    }

    public AttackingExecutor AttackTo(RPGCharacter character) {
        info.attacked = character;
        return this;
    }

    public AttackingExecutor By(RPGCharacter character) {
        info.attacker = character;
        return this;
    }

    public AttackingExecutor WithDamage(float damage) {
        info.damage = damage;
        return this;
    }

    public AttackingExecutor WithEffect(AttackEffect effect) {
        info.effects.Add(effect);
        return this;
    }

    public void exec() {
        if (!info.attacker || !info.attacked) return;
        info.attacked.WasAttack(info);
    }
}
