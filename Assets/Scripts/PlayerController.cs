using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private RPGCharacter character;
    private float transformDurationRemain;
    private RPGCharacter originalCharacter;
    public Vector2 aimPosition;
    public Cinemachine.CinemachineVirtualCamera vCam;

    private void Start() {
        AudioManager.instance.Play("OriginalMusic");
    }

    private void Update() {
        Aim();
    }

    private void FixedUpdate() {
        if (transformDurationRemain > 0) {
            transformDurationRemain -= Time.fixedDeltaTime;
            if (transformDurationRemain <= 0) {
                TransformBackToOriginalCharacter();
            }
        }
    }

    public void Move(InputAction.CallbackContext context) {
        Vector2 direction = context.ReadValue<Vector2>();
        this.character.Move(direction);
    }

    public void Dash(InputAction.CallbackContext context) {
        if (context.started) 
            this.character.Dash();
    }

    public void PrimaryAction(InputAction.CallbackContext context) {
        if (context.started) {
            this.character.skills.UseBy(this.character);
        }

        if (context.canceled) {
            this.character.skills.StopBy(this.character);
        }
    }

    public void Interact(InputAction.CallbackContext context) {
        if (context.started) {
            this.character.Hold();
        }

        if (context.canceled) {
            this.character.Throw();
        }
    }

    public void ChangeSkill(InputAction.CallbackContext context) {
        this.character.skills.Select(0);
    }

    public void Aim() {
        Vector2 lookDirection = aimPosition - (Vector2)this.character.transform.position;
        this.character.LookTo(lookDirection);
    }

    public void OnMouseReceivePosition(InputAction.CallbackContext value) {
        Vector2 rawPosition = value.ReadValue<Vector2>();
        aimPosition = cam.ScreenToWorldPoint(rawPosition);
    }

    public Vector2 getAimPosition() {
        return aimPosition;
    }

    public void SetCharacter(RPGCharacter character) {
        vCam.Follow = character.transform;
        this.character = character;
        this.character.tag = "Player";
    }

    public void TransformTo(RPGCharacter target, float duration) {
        this.character.Move(new Vector2(0, 0));
        originalCharacter = this.character;
        SetCharacter(target);
        this.transformDurationRemain = duration;
        this.originalCharacter.gameObject.SetActive(false);
        AudioManager.instance.Stop("OriginalMusic");
        AudioManager.instance.Play("BattleMusic");
    }

    public bool IsTransforming() {
        return this.originalCharacter != null;
    }

    public void TransformBackToOriginalCharacter() {
        AudioManager.instance.Stop("BattleMusic");
        AudioManager.instance.Play("OriginalMusic");
        this.originalCharacter.transform.position = this.character.transform.position;
        
        // do some animation

        Destroy(this.character.gameObject);
        SetCharacter(this.originalCharacter);
        this.originalCharacter = null;
        this.character.gameObject.SetActive(true);
    }
}
