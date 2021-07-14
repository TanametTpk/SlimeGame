using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : CharacterBase
{
    [SerializeField] private Transform characterDisplayTransform;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Animator animator;
    public Rigidbody2D rigid;
    public bool isEnebledAction = true;
    public bool isCanMove = true;
    protected Vector2 moveDirection;
    public bool isFacingRight = false;

    private void FixedUpdate() {
        if (isCanMove)
            this.rigid.velocity = moveDirection * GetSpeed();
    }

    public virtual void Move(Vector2 direction) {
        if (!isEnebledAction) return;
        moveDirection = direction;
    }

    public Sprite GetSprite() {
        return this.spriteRenderer.sprite;
    }

    public void SetSprite(Sprite sprite) {
        this.spriteRenderer.sprite = sprite;
    }

    public SpriteRenderer GetSpriteRenderer() {
        return this.spriteRenderer;
    }

    public void SetSpriteRenderer(SpriteRenderer spriteRenderer) {
        this.spriteRenderer = spriteRenderer;
    }

    public Animator GetAnimator() {
        return this.animator;
    }

    public void SetAnimator(Animator animator) {
        this.animator = animator;
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector2 theScale = characterDisplayTransform.localScale;
        theScale.x *= -1;
        characterDisplayTransform.localScale = theScale;
    }
}
