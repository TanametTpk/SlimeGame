using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Info", menuName = "Weapon Info")]
public class WeaponInfo : ScriptableObject
{
    public string name;
    public string description;
    public Sprite sprite;
    public float cooldown;
    public float damage;
    public float attackRange;
}
