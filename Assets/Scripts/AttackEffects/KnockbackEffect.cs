using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackEffect: AttackEffect
{
    private Vector3 pushDirection;
    private float paralyzeTime;
    public KnockbackEffect(Vector3 pushDirection, float paralyzeTime) {
        this.pushDirection = pushDirection;
        this.paralyzeTime = paralyzeTime;
    }

    public override void Perform(AttackingInfo info)
    {
        info.attacked.StartCoroutine(DoEffect(info));
    }

    private IEnumerator DoEffect(AttackingInfo info) {
        info.attacked.isCanMove = false;

        info.attacked.rigid.velocity = Vector2.zero;
        info.attacked.rigid.AddForce(pushDirection, ForceMode2D.Impulse);
        yield return new WaitForSeconds(paralyzeTime);

        info.attacked.isCanMove = true;
    }
}