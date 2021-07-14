using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] protected GameObject bulletTemplate;
    [SerializeField] protected float initDistance;
    
    public override void UseBy(RPGCharacter character) {
        WeaponHolder holder = character.GetComponent<WeaponHolder>();
        float aimAngle = holder.GetCurrentAngle();
        Vector3 targetPos = transform.position - character.transform.position;

        GameObject bullet = Instantiate(bulletTemplate, transform.position + targetPos.normalized * initDistance, Quaternion.Euler(0, 0, aimAngle));
        MakingDamageWhenHit damageScript = bullet.GetComponent<MakingDamageWhenHit>();
        WhenHitDestoryObject hitScript = bullet.GetComponent<WhenHitDestoryObject>();
        string[] tags = {"Enemy", "Player"};
        damageScript.tags = tags;
        damageScript.attacker = character;

        List<string> hitTags = new List<string>();
        foreach (var defaultTags in hitScript.tags)
        {
            hitTags.Add(defaultTags);
        }

        if (character.CompareTag("Player")) {
            hitTags.Add("Enemy");
        }else {
            hitTags.Add("Player");
        }
        hitScript.tags = hitTags.ToArray();

        StopAttackIn(this.info.cooldown);
    }

    public override void UpdatePosition(RPGCharacter user, WeaponHolder holder, float distance, float angle) {
        float x = user.transform.position.x - distance * Mathf.Sin(Mathf.PI * 2 * angle / 360);
        float y = user.transform.position.y + distance * Mathf.Cos(Mathf.PI * 2 * angle / 360);
        float z = user.transform.position.z;
        Vector3 targetPos = new Vector3(x, y, z);
        transform.position = targetPos;
        transform.rotation = Quaternion.Euler(0, 0, holder.GetCurrentAngle() + offsetAngle);
    }
}
