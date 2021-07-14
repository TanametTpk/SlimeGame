using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject target;
    public float attackRange;
    [SerializeField] private RPGCharacter character;
    [SerializeField] private bool isAttacking;
    [SerializeField] private float rangeBetweenFriend;
    void Start()
    {
        UpdateTarget();
    }

    void Update()
    {
        if (character.GetHealth() <= 0) {
            Destroy(this.gameObject);
        }

        if (!target || !target.activeSelf) {
            UpdateTarget();
        }

        // if not found target
        if (!target) return;
        if (IsTargetInAttackRange()) {
            character.LookTo(target.transform.position - transform.position);
            Attack();
        }else {
            Move();
        }
    }

    private void UpdateTarget() {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private bool IsTargetInAttackRange() {
        float distance = Vector2.Distance(transform.position, target.transform.position);
        return distance <= attackRange;
    }

    private void Move() {
        if (isAttacking) return;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, rangeBetweenFriend);
        Vector3 sumDirection = Vector3.zero;
        foreach (var friend in colliders)
        {
            sumDirection -= friend.transform.position - transform.position;
        }

        Vector2 direction = target.transform.position - transform.position;
        bool isShouldRun = IsShouldRunAway();
        direction = direction * (isShouldRun ? -1 : 1);
        character.LookTo(direction);
        direction += (Vector2)sumDirection;
        character.Move(direction.normalized);
    }

    private void Attack() {
        if (IsShouldRunAway()) return;
        if (isAttacking) return;
        character.Move(Vector2.zero);
        StartCoroutine(DoAttackAnimation());
    }

    private IEnumerator DoAttackAnimation() {
        isAttacking = true;
        character.skills.UseBy(character);
        yield return new WaitForSeconds(0.3f);
        character.skills.StopBy(character);
        isAttacking = false;
    } 

    private bool IsShouldRunAway() {
        PlayerController player = FindObjectOfType<PlayerController>();
        return player.IsTransforming();
    }
}
