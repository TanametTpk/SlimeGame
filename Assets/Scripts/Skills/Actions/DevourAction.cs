using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attacking Action", menuName = "Devour Action")]
public class DevourAction : CharacterAction
{
    public string[] tags;
    public float transformTime;
    public float pullSpeed;
    public Material[] lineMaterial;
    public float consumeDistance;
    public float hookDistance;
    public GameObject hookerTemplate;
    public float hookSpeed;
    private bool isHooked;
    private HookScript hookScript;
    private bool isFinish = true;

    public override void PerformBy(RPGCharacter character)
    {
        if (!isFinish) return;
        isFinish = false;
        Vector2 hookDirection = character.lookDirection.normalized;
        GameObject hooker = Instantiate(hookerTemplate, (Vector2)character.transform.position + character.lookDirection.normalized, Quaternion.identity);
        hookScript = hooker.GetComponent<HookScript>();
        hookScript.caster = character.transform;
        hookScript.speed = hookSpeed;
        hookScript.returnSpeed = pullSpeed;
        hookScript.range = hookDistance;
        hookScript.stopRange = 0;
        hookScript.tagsToCheck = tags;
        hookScript.direction = hookDirection;
    }
    
    public override void FixedUpdateBy(RPGCharacter character)
    {
        if (hookScript) {
            float distance = Vector2.Distance(character.transform.position, hookScript.transform.position);

            SetActionHookedObject(false);

            if (distance <= consumeDistance) {
                if (hookScript.hookedObject) {
                    ConsumeEnemy(character);
                }
                else {
                    DestoryHook();
                }
            }
        }
    }

    public override void StopPerformBy(RPGCharacter character){
        if (character) return;
        if (isFinish) return;
        isFinish = true;
    }

    private void ConsumeEnemy(RPGCharacter character) {
        PlayerController controller = FindObjectOfType<PlayerController>();
        RPGCharacter enemyCharacter = hookScript.hookedObject.GetComponent<RPGCharacter>();
        SetActionHookedObject(true);

        EnemyAI ai = hookScript.hookedObject.GetComponent<EnemyAI>();
        Destroy(ai);

        MultipleBulletRangedWeapon bow = enemyCharacter.GetComponentInChildren<MultipleBulletRangedWeapon>();
        if (bow) {
            bow.numberOfBullet = 5;
        }

        enemyCharacter.SetMaxHealth(character.GetMaxHealth());
        enemyCharacter.SetHealth(character.GetHealth());
        enemyCharacter.SetSpeed(character.GetSpeed());
        character.isCanMove = true;
        controller.TransformTo(enemyCharacter, transformTime);
        DestoryHook();
    }

    private void SetActionHookedObject(bool isEnebledAction) {
        if (!hookScript.hookedObject) return;
        GameObject hookedObject = hookScript.hookedObject;
        if (hookedObject.tag == "Enemy") {
            RPGCharacter enemy = hookedObject.GetComponent<RPGCharacter>();
            enemy.isEnebledAction = isEnebledAction;
        }
    }

    private void DestoryHook() {
        if (!hookScript) return;
        Destroy(hookScript.gameObject);
        StopPerformBy(null);
    }
}
