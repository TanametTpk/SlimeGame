using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField ]private int spinAngle;
    [SerializeField] private float exitAnimationTime;
    [SerializeField] private float exitAttackTime;

    private void Awake() {
        trailRenderer.emitting = false;
    }

    public override void StartAttack() {
        trailRenderer.emitting = true;
    }

    public override void StopAttack() {
        trailRenderer.emitting = false;
    }

    public override void UseBy(RPGCharacter character)
    {
        StartAttack();
        WeaponHolder holder = character.GetComponent<WeaponHolder>();
        if (holder != null) {
            holder.SetToAngle(holder.GetTargetAngle());
            holder.ForceMoveToAngle(spinAngle, exitAttackTime);
        }
        RPGCharacter[] characters = FindCharacterInAttackRange(character.transform.position, character.lookDirection);
        foreach (var targetCharacter in characters)
        {
            if (!targetCharacter.CompareTag(character.tag)) {
                character.Attack(targetCharacter).WithDamage(this.info.damage).exec();
            }
        }

        StopAttackIn(exitAnimationTime);
    }

    private RPGCharacter[] FindCharacterInAttackRange(Vector3 center, Vector3 lookDirection) {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, this.info.attackRange);
        List<RPGCharacter> targets = new List<RPGCharacter>();

        foreach (var collider in colliders)
        {
            RPGCharacter character = collider.GetComponent<RPGCharacter>();
            if (character && IsCharacterInAngle(center, lookDirection, character)) {
                targets.Add(character);
            }
        }

        return targets.ToArray();
    }

    private bool IsCharacterInAngle(Vector3 center, Vector3 lookDirection, RPGCharacter character) {
        Vector3 targetDir = character.transform.position - center;
        float angle = Vector2.Angle(targetDir, lookDirection);
        return angle < 45;
    }

    public override void UpdatePosition(RPGCharacter user, WeaponHolder holder, float distance, float angle) {
        float x = user.transform.position.x + distance * Mathf.Sin(Mathf.PI * 2 * angle / 360);
        float y = user.transform.position.y - distance * Mathf.Cos(Mathf.PI * 2 * angle / 360);
        float z = user.transform.position.z;
        Vector3 targetPos = new Vector3(x, y, z);
        transform.position = targetPos;
        transform.rotation = Quaternion.Euler(0, 0, holder.GetCurrentAngle() + offsetAngle);
    }
}
