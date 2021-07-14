using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float maxMana;
    [SerializeField] private float mana;
    [SerializeField] private float speed;

    public float GetSpeed() {
        return speed;
    }

    public void SetSpeed(float speed) {
        this.speed = speed;
    }

    public float GetHealth() {
        return health;
    }

    public float GetMaxHealth() {
        return maxHealth;
    }

    public void SetMaxHealth(float maxHealth) {
        this.maxHealth = maxHealth;
    }

    public void SetHealth(float health) {
        this.health = health;
    }

    public void TakeDamage(float health) {
        this.health -= health;
        if (this.health < 0) this.health = 0;
    }

    public void Heal(float health) {
        this.health += health;
        if (this.health > this.maxHealth) this.health = this.maxHealth;
    }

    public float GetMaxMana() {
        return maxMana;
    }

    public float GetMana() {
        return mana;
    }

    public void SetMaxMana(float maxMana) {
        this.maxMana = maxMana;
    }

    public void UseMana(float point) {
        this.mana -= point;
        if (this.mana < 0) this.mana = 0;
    }

    public void RestoreMana(float point) {
        this.mana += point;
        if (this.mana < this.maxMana) this.mana = this.maxMana;
    }
}
