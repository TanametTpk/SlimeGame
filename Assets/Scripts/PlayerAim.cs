using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private WeaponHolder holder;
    private Vector2 aimPosition;
    private void Update() {
        Aim();
    }

    public void Aim() {
        Vector2 lookDirection = aimPosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        holder.SetRotate(angle, holder.GetSpeed());
    }

    public void OnMouseReceivePosition(InputAction.CallbackContext value) {
        Vector2 rawPosition = value.ReadValue<Vector2>();
        aimPosition = cam.ScreenToWorldPoint(rawPosition);
    }

    public Vector2 getAimPosition() {
        return aimPosition;
    }
}
