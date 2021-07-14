using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleBulletRangedWeapon : RangedWeapon
{
    [SerializeField] public int numberOfBullet = 1;
    [SerializeField] public float angleRange = 30;
    
    public override void UseBy(RPGCharacter character) {
        WeaponHolder holder = character.GetComponent<WeaponHolder>();
        float aimAngle = holder.GetCurrentAngle();
        Vector3 targetPos = transform.position - character.transform.position;
        float stepAngle = angleRange / (numberOfBullet + 1);
        float currentAngle = angleRange / 2 * -1 + stepAngle;

        for (int i = 0; i < numberOfBullet; i++) {
            GameObject bullet = Instantiate(bulletTemplate, transform.position + targetPos.normalized * initDistance, Quaternion.Euler(0, 0, aimAngle));   
            SetupBullet(bullet, character);

            bullet.transform.RotateAround(transform.position, new Vector3(0, 0, 1), currentAngle);

            currentAngle += stepAngle;
        }

        StopAttackIn(this.info.cooldown);
    }

    private void SetupBullet(GameObject bullet, RPGCharacter character) {
        MakingDamageWhenHit damageScript = bullet.GetComponent<MakingDamageWhenHit>();
        damageScript.attacker = character;

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
    }
}
