using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponInfo info;
    [SerializeField] protected float offsetAngle;
    private float stopAttackTime = 0;

    private void FixedUpdate() {
        if (stopAttackTime > 0) {
            stopAttackTime -= Time.fixedDeltaTime;

            if (stopAttackTime <= 0) {
                StopAttack();
            }
        }
    }

    public virtual void UseBy(RPGCharacter character) {
        // do something
    }

    public virtual void StartAttack() {
        // setup animation
    }

    public virtual void StopAttack() {
        // finish animation
    }

    public virtual void UpdatePosition(RPGCharacter user, WeaponHolder holder, float distance, float angle) {
        // update position
    }

    public virtual void StopAttackIn(float duration) {
        this.stopAttackTime = duration;
    }
}
