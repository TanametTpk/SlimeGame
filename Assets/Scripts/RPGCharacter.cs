using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGCharacter : Character
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private WeaponHolder holder;
    [SerializeField] public SkillSystem skills;
    private int currentSelectSkill;
    [HideInInspector] public Vector2 lookDirection;
    public float forceDash;
    public float timeToDash;
    public float throwingForce;
    public float carryRadius;
    public float carryPosition = 1;
    public float dashCoolDown = 0.8f;
    public float dashTimeRemain = 0;
    public ParticleSystem dustEffect;
    private GameObject holdingItem;

    private void Update() {
        if (skills.canUseSkill != isEnebledAction) {
            skills.canUseSkill = isEnebledAction;
        }

        if (holdingItem) {
            holdingItem.transform.position = transform.position + Vector3.up * carryPosition;
        }
        
        if (dashTimeRemain > 0) {
            dashTimeRemain -= Time.deltaTime;
        }
    }

    public void Dash() {
        if (!isEnebledAction) return;
        if (!isCanMove) return;
        if (dashTimeRemain > 0) return;
        dashTimeRemain = dashCoolDown;
        StartCoroutine(MoveDash(forceDash, timeToDash));
        AudioManager.instance.Play("Dash");
    }

    private void CreateDust() {
        if (!dustEffect) return;
        dustEffect.Play();
    }

    private IEnumerator MoveDash(float force, float timeToMove) {
        isCanMove = false;
        this.rigid.velocity = Vector2.zero;
        this.rigid.AddForce(moveDirection.normalized * force, ForceMode2D.Impulse);
        CreateDust();
        yield return new WaitForSeconds(timeToMove);
        isCanMove = true;
    }

    public void Hold() {
        if (!isEnebledAction) return;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, carryRadius);
        GameObject nearestCarryable = null;
        float minDistance = float.MaxValue;
        foreach (var collider in colliders)
        {
            if (!collider.CompareTag("Carryable")) continue;
            float distance = Vector2.Distance(collider.transform.position, transform.position);

            if (distance < minDistance) {
                minDistance = distance;
                nearestCarryable = collider.gameObject;
            }
        }

        if (!nearestCarryable) return;
        holdingItem = nearestCarryable;
    }

    public void Throw() {
        if (!isEnebledAction) return;
        if (!holdingItem) return;
        GameObject item = holdingItem;
        holdingItem = null;

        Rigidbody2D itemBody = item.GetComponent<Rigidbody2D>();
        Collider2D collider = item.GetComponent<Collider2D>();
        DoDamageWhenHaveSpeed attackScript = item.GetComponent<DoDamageWhenHaveSpeed>();
        if (!itemBody || !collider || !attackScript) return;

        collider.isTrigger = true;
        attackScript.attacker = this;
        itemBody.AddForce(lookDirection.normalized * throwingForce, ForceMode2D.Impulse);
        AudioManager.instance.Play("Throw");
    }

    public override void Move(Vector2 direction)
    {
        if (!isEnebledAction) return;
        this.animator.SetFloat("speed", Mathf.Abs(direction.x + direction.y));
        base.Move(direction);
    }

    public void LookTo(Vector2 lookDirection) {
        if (!isEnebledAction) return;
        if (lookDirection.x > 0 && !isFacingRight) 
        {
            Flip ();
            isFacingRight = true;
        } else if (lookDirection.x < 0 && isFacingRight) 
        {
            Flip ();
            isFacingRight = false;
        }
        this.lookDirection = lookDirection;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        holder.SetRotate(angle, holder.GetSpeed());
    }

    public AttackingExecutor Attack(RPGCharacter character) {
        AttackingExecutor info = new AttackingExecutor();
        info.AttackTo(character).By(this);
        return info;
    }

    public void WasAttack(AttackingInfo info) {
        this.GetAnimator().SetTrigger("Damaged");
        
        foreach (var effect in info.effects)
        {
            effect.Perform(info);
        }
        TakeDamage(info.damage);
        AudioManager.instance.Play("Damaged");
    }

    public void SetWeapon(Weapon weapon) {
        this.weapon = weapon;
        this.holder.SetItem(this.weapon);
    }

    public Weapon GetWeapon() {
        return this.weapon;
    }
}
