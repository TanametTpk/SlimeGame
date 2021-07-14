using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private RPGCharacter user;
    [SerializeField] private float speed;
    [SerializeField] private float targetAngle;
    [SerializeField] private float distance = 1.2f;
    [SerializeField] private bool canMove;
    private float diffAngle;
    private float disableTime;
    private Weapon item;
    private bool isClockWire = false;

    private void Start() {
        item = user.GetWeapon();
    }

    private void FixedUpdate() {
        float currentAngle = GetCurrentAngle();
        if (canMove){
            SetToAngle(targetAngle);
        }else if (diffAngle > 0) {
            item.transform.RotateAround(transform.position, new Vector3(0, 0, 1), GetTurnDirection() * speed);
            diffAngle -= speed;
        }

        if (disableTime > 0) {
            disableTime -= Time.deltaTime;
        }else {
            canMove = true;
        }
    }

    private float GetTurnDirection() {
        return isClockWire ? -1 : 1;
    }

    public void SetToAngle(float angle) {
        if (item == null) return;
        item.UpdatePosition(user, this, distance, angle);
    }

    public void SetRotate(float angle, float newSpeed) {
        SetTargetAngle(angle);
        speed = newSpeed;
    }

    public void SetTargetAngle(float angle) {
        targetAngle = angle;
    }

    public float GetTargetAngle() {
        return targetAngle;
    }

    public float GetSpeed() {
        return speed;
    }

    public float GetCurrentAngle() {
        if (item == null) return 0;
        Vector2 lookDirection = item.transform.position - transform.position;
        float currentAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;
        return currentAngle;
    }

    private void OnDrawGizmosSelected() {
        float x = transform.position.x + distance * Mathf.Sin(Mathf.PI * 2 * GetCurrentAngle() / 360);
        float y = transform.position.y - distance * Mathf.Cos(Mathf.PI * 2 * GetCurrentAngle() / 360);
        float z = transform.position.z;
        Vector3 finalPos = new Vector3(x, y, z);
        Debug.DrawLine(transform.position, finalPos, Color.red);

        x = transform.position.x + distance * Mathf.Sin(Mathf.PI * 2 * targetAngle / 360);
        y = transform.position.y - distance * Mathf.Cos(Mathf.PI * 2 * targetAngle / 360);
        z = transform.position.z;
        Vector3 targetPos = new Vector3(x, y, z);
        Debug.DrawLine(transform.position, targetPos, Color.green);
    }

    public void SetSpeed(float speed) {
        this.speed = speed;
    }

    public void ForceMoveToAngle(float angle, float duration) {
        DisableMoveWithDuration(duration);
        SetTargetAngle(angle);
        diffAngle = Mathf.Abs(angle);
    }

    public void DisableMoveWithDuration(float duration) {
        if (!canMove) {
            isClockWire = !isClockWire;
        }
        canMove = false;
        disableTime = duration;
    }

    public void SetItem(Weapon item) {
        this.item = item;
    }
}